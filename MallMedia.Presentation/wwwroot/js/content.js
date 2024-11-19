// content.js

import { baseUrl } from '/js/config.js';

import { checkUserAuthentication } from '/js/auth.js';  // Đảm bảo đường dẫn đúng

window.onload = async function () {
    try {
        // Gọi hàm checkUserAuthentication và lấy thông tin currentUser
        const currentUser = await checkUserAuthentication("Admin");
    } catch (error) {
        console.error('Lỗi khi xác thực:', error);
    }
};

//loading data content
document.addEventListener('DOMContentLoaded', function () {
    // Load contents on page load
    loadContent(1);

    // Handle pagination click events
    document.getElementById('pagination').addEventListener('click', function (e) {
        if (e.target && e.target.classList.contains('page-link')) {
            e.preventDefault();
            const pageNumber = parseInt(e.target.getAttribute('data-page'));
            loadContent(pageNumber);
        }
    });
});

// Function to load content and update the table and pagination
function loadContent(pageNumber) {
    const pageSize = 10; // Define how many items per page you want

    const token = localStorage.getItem('authToken');  // Assuming token is stored in localStorage

    if (!token) {
        console.error('No authentication token found.');
        return;
    }

    // Prepare API request parameters
    const url = baseUrl + `/api/content?PageNumber=${pageNumber}&PageSize=${pageSize}`;
    const headers = {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'  // This can be added if you're expecting JSON data
    };

    // Make the AJAX call to the API
    fetch(url, {
        method: 'GET',  // GET request
        headers: headers // Include the headers in the request
    })
        .then(response => response.json())
        .then(data => {
            // Update the table with fetched content
            updateTable(data.items, pageNumber);

            // Update pagination
            updatePagination(data.totalPages, pageNumber);
        })
        .catch(error => {
            console.error('Error fetching content:', error);
        });
}

// Function to update the content table with new data
function updateTable(contents, pageNumber) {
    const tableBody = document.querySelector('#contentTable tbody');
    tableBody.innerHTML = ''; // Clear existing rows

    contents.forEach((content, index) => {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${(pageNumber - 1) * 10 + (index + 1)}</td>
            <td>${content.title}</td>
            <td>${content.description}</td>
            <td>${content.category.name}</td>
            <td>${new Date(content.createdAt).toISOString().split('T')[0]}</td>
           
        `;
        tableBody.appendChild(row);
    });
}
//<td>
//    <a href="#" class="btn btn-primary btn-sm">Edit</a>
//    <a href="#" class="btn btn-danger btn-sm">Delete</a>
//</td>
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
