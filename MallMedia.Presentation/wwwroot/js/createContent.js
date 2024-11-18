import { baseUrl } from '/js/config.js';
const authToken = localStorage.getItem('authToken');
const tolerance = 0.01;

function isMultipleOf(value, resolution, factor) {
    const ratio = value / resolution;
    return Math.abs(ratio - factor) < tolerance; // Check if the ratio is close to the desired factor
}

function isIntegerMultiple(value, resolution) {
    const ratio = value / resolution;
    return Math.abs(ratio - Math.round(ratio)) < tolerance; // Check if the ratio is close to an integer
}

document.addEventListener('DOMContentLoaded', async function () {
    const categoryDropdown = new Choices('#categoryDropdown', {
        searchEnabled: true,
        itemSelectText: '',
        placeholder: true,
        placeholderValue: 'Select Category',
    });

    try {
        const currentUserEndpoint = baseUrl + "/api/identity/currentUser";
        const response = await fetch(currentUserEndpoint, {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${authToken}`
            }
        });

        if (!response.ok) {
            throw new Error('Failed to fetch current user');
        }

        const currentUser = await response.json();

        const userIdInput = document.getElementById('userId');
        const usernameInput = document.getElementById('username');
        if (userIdInput && currentUser && currentUser.id) {
            userIdInput.value = currentUser.id;
            usernameInput.value = currentUser.username;
        }
    } catch (error) {
        console.error('Error fetching user data:', error);
    }
});

const chunkSize = 1024 * 1024;
const uploadEndpoint = baseUrl + '/api/content/upload-media';
const createContentEndpoint = baseUrl + '/api/content';
const form = document.getElementById('contentForm');
const submitBtn = document.getElementById('submitBtn');
const videoInput = document.getElementById('videoInput');
const videoValidationMessage = document.getElementById('videoError');

async function validateVideos(files) {
    const validVideos = [];
    let found1080x1920 = false;
    let found1920x1080 = false;

    for (const file of files) {
        const video = document.createElement('video');
        video.src = URL.createObjectURL(file);

        await new Promise((resolve) => {
            video.onloadedmetadata = function () {
                if (file.type === 'video/mp4' &&
                    file.size <= 200 * 1024 * 1024 &&
                    Math.floor(video.duration) <= 30) {

                    const metadata = {
                        fileName: file.name,
                        fileType: file.type,
                        fileSize: file.size,
                        duration: Math.floor(video.duration),
                        resolution: `${video.videoWidth}x${video.videoHeight}`
                    };

                    if (!found1920x1080 && isIntegerMultiple(video.videoWidth, 1920) && isIntegerMultiple(video.videoHeight, 1080)) {
                        validVideos.push({ file, metadata });
                        found1920x1080 = true;
                    } else if (!found1080x1920 && isIntegerMultiple(video.videoWidth, 1080) && isIntegerMultiple(video.videoHeight, 1920)) {
                        validVideos.push({ file, metadata });
                        found1080x1920 = true;
                    }
                }
                resolve();
            };
        });
    }

    if (validVideos.length === 0) {
        document.getElementById("videoError").innerText = "Please upload at least one valid video (1920x1080 or 1080x1920 and less than 30s).";
    } else if (validVideos.length !== 1 && validVideos.length !== 2) {
        document.getElementById("videoError").innerText = "Please upload exactly one or two valid videos.";
    }

    return validVideos;
}

async function uploadFileInChunks(file, metadata, mediaDtos) {
    let start = 0;
    let chunkIndex = 0;
    const totalChunks = Math.ceil(file.size / chunkSize);
    while (start < file.size) {
        const chunk = file.slice(start, start + chunkSize);
        const formData = new FormData();
        formData.append("chunk", chunk);
        formData.append("fileName", file.name);
        formData.append("chunkIndex", chunkIndex);
        formData.append("totalChunks", totalChunks);
        formData.append("fileType", file.type);
        formData.append("fileSize", file.size);
        formData.append("duration", metadata.duration);
        formData.append("resolution", metadata.resolution);

        const response = await fetch(uploadEndpoint, {
            method: 'POST',
            body: formData,
            headers: {
                'Authorization': `Bearer ${authToken}`
            }
        });
        if (response.ok) {
            const data = await response.json();
            if (Array.isArray(data) && data.length > 0) {
                mediaDtos.push(data[0]);
            }
        }
        if (!response.ok) {
            throw new Error('Failed to upload chunk ' + chunkIndex);
        }

        start += chunkSize;
        chunkIndex++;
    }
    return mediaDtos;
}

submitBtn.addEventListener('click', async (e) => {
    e.preventDefault();

    document.querySelectorAll(".error").forEach(e => e.innerText = "");

    let isValid = true;

    const title = document.getElementById("title").value.trim();
    if (title.length < 5) {
        document.getElementById("titleError").innerText = "Title must be at least 5 characters.";
        isValid = false;
    }

    const description = document.getElementById("description").value.trim();
    if (description.length < 10) {
        document.getElementById("descriptionError").innerText = "Description must be at least 10 characters.";
        isValid = false;
    }

    const categoryId = parseInt(document.getElementById("categoryDropdown").value, 10);
    if (isNaN(categoryId) || categoryId <= 0) {
        document.getElementById("categoryError").innerText = "Please select a valid category.";
        isValid = false;
    }

    const userId = document.getElementById("userId").value.trim();
    if (!userId) {
        document.getElementById("userIdError").innerText = "UserId is required.";
        isValid = false;
    }

    const files = videoInput.files;
    const validVideos = await validateVideos(files);
    let mediaDtos = [];

    if (validVideos.length !== 2 && validVideos.length !== 1) {
        isValid = false;
    }

    if (!isValid) {
        return;
    }

    try {
        for (const { file, metadata } of validVideos) {
            await uploadFileInChunks(file, metadata, mediaDtos);
        }

        const formData = new FormData();
        formData.append("Title", document.getElementById("title").value);
        formData.append("CategoryId", document.getElementById("categoryDropdown").value);
        formData.append("Description", document.getElementById("description").value);
        formData.append("UserId", document.getElementById("userId").value);
        formData.append("MediaDtos", JSON.stringify(mediaDtos));

        const response = await fetch(createContentEndpoint, {
            method: 'POST',
            body: formData,
            headers: {
                'Authorization': `Bearer ${authToken}`
            }
        });

        if (response.ok) {
            videoValidationMessage.textContent = "";
            alert('Creating content successfully');
            window.location.href = "/admin/content/create";
        } else {
            alert('Error creating content.');
        }
    } catch (error) {
        alert('Create content failed: ' + error.message);
    }
});
