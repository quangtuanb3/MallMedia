﻿// devices.js
import { baseUrl } from '/js/config.js';  // Nhập baseUrl từ tệp config.js

import { checkUserAuthentication } from '/js/auth.js';  // Đảm bảo đường dẫn đúng

window.onload = async function () {
    try {
        // Gọi hàm checkUserAuthentication và lấy thông tin currentUser
        const currentUser = await checkUserAuthentication("Admin");
    } catch (error) {
        console.error('Lỗi khi xác thực:', error);
    }
};
// Load devices
document.addEventListener('DOMContentLoaded', function () {
    loadDevices(1);

    // Handle pagination click events
    document.getElementById('pagination').addEventListener('click', function (e) {
        if (e.target && e.target.classList.contains('page-link')) {
            e.preventDefault();
            const pageNumber = parseInt(e.target.getAttribute('data-page'));
            loadDevices(pageNumber);
        }
    });
});

function loadDevices(pageNumber) {
    const pageSize = 10;  // Define how many items per page you want
   

    // Retrieve the token (example from localStorage, replace as necessary)
    const token = localStorage.getItem('authToken');  // Assuming token is stored in localStorage

    if (!token) {
        console.error('No authentication token found.');
        return;
    }

    // Prepare API request parameters
    const url = baseUrl + `/api/Schedule?PageNumber=${pageNumber}&PageSize=${pageSize}`;

    // Set up request headers, including Authorization
    const headers = {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'  // This can be added if you're expecting JSON data
    };

    // Make the AJAX call to the API
    fetch(url, {
        method: 'GET',  // GET request
        headers: headers // Include the headers in the request
    })
        .then(response => {
            if (!response.ok) {
                throw new Error(`Network response was not ok: ${response.statusText}`);
            }
            return response.json();
        })
        .then(data => {
            // Check if data.items exists and is an array
            if (Array.isArray(data.items)) {
                // Update the table
                updateTable(data.items, pageNumber);

                // Update pagination
                updatePagination(data.totalPages, pageNumber);
            } else {
                console.error('Invalid response format. "items" not found.');
            }
        })
        .catch(error => {
            console.error('Error fetching devices:', error);
        });
}

// Function to update the table with new device data
function updateTable(devices, pageNumber) {
    const tableBody = document.querySelector('#scheduleTable tbody');
    tableBody.innerHTML = ''; // Clear existing rows

    devices.forEach((schedule, index) => {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${(pageNumber - 1) * 10 + (index + 1)}</td>
            <td>${schedule.devicedto.deviceName}</td>
            <td>${new Date(schedule.startDate).toISOString().split('T')[0]}</td>
            <td>${new Date(schedule.endDate).toISOString().split('T')[0]}</td>
        `;
        tableBody.appendChild(row);
    });
}

// Function to update the pagination controls
function updatePagination(totalPages, currentPage) {
    const paginationDiv = document.getElementById('pagination');
    paginationDiv.innerHTML = ''; // Clear existing pagination

    // Previous page and first page links
    if (currentPage > 1) {
        paginationDiv.innerHTML += `
            <ul class="pagination">
                <li class="page-item">
                    <a class="page-link" href="#" data-page="1">First</a>
                </li>
                <li class="page-item">
                    <a class="page-link" href="#" data-page="${currentPage - 1}">Previous</a>
                </li>
            </ul>
        `;
    }

    // Page number links
    for (let i = 1; i <= totalPages; i++) {
        paginationDiv.innerHTML += `
            <ul class="pagination">
                <li class="page-item ${i === currentPage ? 'active' : ''}">
                    <a class="page-link" href="#" data-page="${i}">${i}</a>
                </li>
            </ul>
        `;
    }

    // Next page and last page links
    if (currentPage < totalPages) {
        paginationDiv.innerHTML += `
            <ul class="pagination">
                <li class="page-item">
                    <a class="page-link" href="#" data-page="${currentPage + 1}">Next</a>
                </li>
                <li class="page-item">
                    <a class="page-link" href="#" data-page="${totalPages}">Last</a>
                </li>
            </ul>
        `;
    }
}
