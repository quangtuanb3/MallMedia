import { baseUrl } from '/js/config.js';
import { showToast } from '/js/auth.js';  // Đảm bảo đường dẫn đúng
document.addEventListener("DOMContentLoaded", function () {
    const token = localStorage.getItem('authToken');
    const urlLocation = baseUrl + '/api/locations';
    const urlCreateDevice = baseUrl + '/api/devices';

    // Fetch locations on page load
    fetch(urlLocation, {
        method: "GET",
        headers: {
            "Authorization": `Bearer ${token}`
        }
    })
    .then(response => response.json())
    .then(locations => {
        const locationSelect = document.getElementById("locationId");
        locations.forEach(location => {
            const option = document.createElement("option");
            option.value = location.id;
            option.textContent = `Tầng: ${location.floor} - ${location.department}`;
            locationSelect.appendChild(option);
        });
    })
    .catch(error => {
        console.error("Error fetching locations:", error);
        displayError("general", "Failed to load locations. Please try again.");
    });

    const createDeviceForm = document.getElementById("createDeviceForm");

    if (createDeviceForm) {
        createDeviceForm.addEventListener("submit", function (event) {
            event.preventDefault();
            clearErrors();
            var isValid = true;
            const deviceType = document.getElementById("deviceType").value;
            if (!deviceType) {
                document.getElementById("DeviceTypeError").innerText = "Please select a valid device type.";
                isValid = false;
            }
            const locationId = parseInt(document.getElementById("locationId").value, 10);
            if (isNaN(locationId) || locationId <= 0) {
                document.getElementById("LocationError").innerText = "Please select a valid location.";
                isValid = false;
            }
            if (!isValid) {
                return;
            }
            const formData = {
                deviceName: document.getElementById("deviceName").value,
                deviceType: document.getElementById("deviceType").value,
                size: document.getElementById("size").value,
                resolution: document.getElementById("resolution").value,
                locationId: document.getElementById("locationId").value
            };

            fetch(urlCreateDevice, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${token}`
                },
                body: JSON.stringify(formData)
                })
                .then(async (response) => {
                    if (response.ok) {
                        alert('Device created successfully!');
                        window.location.href = '/Admin/Device/Create';
                    } else {
                        const errorData = await response.json();
                        if (errorData.errors) {
                            handleValidationErrors(errorData.errors);
                        } else {
                            showToast("Create new device failed!(Device name is existed!)", "An unknown error occurred. Please try again.");
                        }
                    }
                })
                .catch(error => {
                    console.error("Error creating device:", error);
                    showToast("Create new device failed! (Device name is existed!)", "Error occurred while creating the device.");
                });
        });
    } else {
        console.error("Create device form not found.");
        showToast("Create device form not found.")
    }

    function clearErrors() {
        document.querySelectorAll(".error-message").forEach(span => {
            span.textContent = "";
        });
    }

    function handleValidationErrors(errors) {
        for (const [field, messages] of Object.entries(errors)) {
            const errorSpan = document.getElementById(`${field}Error`);
            if (errorSpan) {
                errorSpan.textContent = messages.join(", ");
            }
        }
    }
});
