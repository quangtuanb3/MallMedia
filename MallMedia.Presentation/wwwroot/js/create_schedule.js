import { baseUrl } from '/js/config.js';

const token = localStorage.getItem('authToken');
const contentMediaEndpoint = baseUrl + "/api/content/{contentId}/medias";
const contentIdEle = document.getElementById("contentId");
const urlCreateSchedule = baseUrl + '/api/Schedule';
const urlFloor_Depaterment = baseUrl + '/api/floor-department';
const url_content = baseUrl + '/api/Content?PageNumber=1&PageSize=100';

$(document).ready(function () {
    // Fetch content and populate the dropdown
    async function fetchContent() {
        try {
            const response = await fetch(url_content, {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${token}` // Include the authorization token
                }
            });

            if (response.ok) {
                const contentData = await response.json();

                // Check if the 'items' property is an array
                if (Array.isArray(contentData.items)) {
                    // Clear the existing content in the dropdown
                    const contentIdEle = $('#contentId');
                    contentIdEle.empty();

                    // Add the default "Select Content" option
                    contentIdEle.append('<option selected>Select Content</option>');

                    // Loop through the content items and add each item as an option
                    contentData.items.forEach(content => {
                        const option = `<option value="${content.id}">[${content.category?.name || 'Unknown Category'}] - ${content.title}</option>`;
                        contentIdEle.append(option);
                    });
                } else {
                    console.error('Expected "items" to be an array, but received:', contentData.items);
                }
            } else {
                console.error('Error fetching content:', response.statusText);
            }
        } catch (error) {
            console.error('Fetch error:', error);
        }
    }

    // Call the function to fetch content on page load
    fetchContent();

    // Event listener for contentId change (if needed)
    $('#contentId').on('change', function () {
        const contentId = $(this).val();
        console.log('Selected contentId:', contentId);
        // You can perform other actions here based on selected content
    });
});

$(document).ready(function () {

    contentIdEle.onchange = async function () {
        const contentId = contentIdEle.value; // Get the selected contentId
        const endpoint = contentMediaEndpoint.replace("{contentId}", contentId);

        try {
            const response = await fetch(endpoint, {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${token}` // Add the Authorization header with the token
                }
            });

            if (response.ok) {
                const mediaData = await response.json();
                console.log("Media Data:", mediaData);
                if (mediaData.length == 1) {
                    console.log(mediaData[0].resolution)
                    const [width, height] = mediaData[0].resolution.split('x').map(Number);
                    if (width < height) {
                        document.getElementById("deviceTypeLED").checked = false;
                        document.getElementById("deviceTypeDigitalPoster").checked = false;
                        document.getElementById("deviceTypeLED").disabled = true;
                        document.getElementById("deviceTypeDigitalPoster").disabled = true;
                        document.getElementById("deviceTypeVerticalLCD").disabled = false;
                    } else {
                        document.getElementById("deviceTypeLED").disabled = false;
                        document.getElementById("deviceTypeDigitalPoster").disabled = false;
                        document.getElementById("deviceTypeVerticalLCD").checked = false;
                        document.getElementById("deviceTypeVerticalLCD").disabled = true;
                    }
                } else {
                    document.getElementById("deviceTypeLED").disabled = false;
                    document.getElementById("deviceTypeDigitalPoster").disabled = false;
                    document.getElementById("deviceTypeVerticalLCD").disabled = false;
                }
            } else {
                console.error("Error fetching media data:", response.statusText);
            }
        } catch (error) {
            console.error("Fetch error:", error);
        }
    };

    function fetchFloorsAndDepartments() {
        const selectedDeviceTypes = [];
        $('input[type="checkbox"]:checked').each(function () {
            selectedDeviceTypes.push($(this).val());
        });

        if (selectedDeviceTypes.length === 0) {
            $('#labelSelectFloor').css('display', 'none');
            $('#floorCheckboxes').empty();
            $('#labelSelectDepartment').css('display', 'none');
            $('#departmentCheckboxes').empty();
            console.log('No device types selected.');
            return;
        }
        $.ajax({
            url: urlFloor_Depaterment,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ deviceType: selectedDeviceTypes }),
            headers: {
                'Authorization': 'Bearer ' + token
            },
            success: function (response) {
                $('#labelSelectFloor').css('display', 'block');
                const floorCheckboxes = $('#floorCheckboxes');
                floorCheckboxes.empty();
                response.floors.forEach(function (floor) {
                    floorCheckboxes.append('<div class="form-check">' +
                        '<input class="form-check-input" type="checkbox" value="' + floor.floor + '" id="floor-' + floor.floor + '">' +
                        '<label class="form-check-label" for="floor-' + floor.floor + '">' + floor.deviceType + ' - Floor ' + floor.floor + '</label>' +
                        '</div>');
                });

                $('#labelSelectDepartment').css('display', 'block');
                const departmentCheckboxes = $('#departmentCheckboxes');
                departmentCheckboxes.empty();
                response.departments.forEach(function (department) {
                    departmentCheckboxes.append('<div class="form-check">' +
                        '<input class="form-check-input" type="checkbox" value="' + department.department + '" id="department-' + department.department + '">' +
                        '<label class="form-check-label" for="department-' + department.department + '">' + department.deviceType + ' - ' + department.department + '</label>' +
                        '</div>');
                });

                console.log('Selected Device Types:', selectedDeviceTypes);
                console.log('API Response:', response);
            },
            error: function (xhr, status, error) {
                alert('Error fetching floor and department data.');
            }
        });
    }

    $('input[type="checkbox"]').on('change', fetchFloorsAndDepartments);
    $('#contentId').on('change', fetchFloorsAndDepartments);

    $('#floorCheckboxes').on('change', 'input[type="checkbox"]', function () {
        var selectedFloors = [];
        $('#floorCheckboxes input[type="checkbox"]:checked').each(function () {
            selectedFloors.push($(this).val());
        });

        if (selectedFloors.length > 0) {
            $('#departmentCheckboxes input[type="checkbox"]').prop('disabled', true);
        } else {
            $('#departmentCheckboxes input[type="checkbox"]').prop('disabled', false);
        }
    });

    $('#departmentCheckboxes').on('change', 'input[type="checkbox"]', function () {
        var selectedDepartments = [];
        $('#departmentCheckboxes input[type="checkbox"]:checked').each(function () {
            selectedDepartments.push($(this).val());
        });

        if (selectedDepartments.length > 0) {
            $('#floorCheckboxes input[type="checkbox"]').prop('disabled', true);
        } else {
            $('#floorCheckboxes input[type="checkbox"]').prop('disabled', false);
        }
    });

    $('#filterForm').submit(function (e) {
        e.preventDefault();
        clearErrors();
        $('button[type="submit"]').prop('disabled', false); // Disable the submit button

        var selectedDeviceTypes = [];
        $('#deviceTypeCheckBox input[type="checkbox"]:checked').each(function () {
            selectedDeviceTypes.push($(this).val());
        });

        var selectedFloors = [];
        $('#floorCheckboxes input[type="checkbox"]:checked').each(function () {
            selectedFloors.push($(this).val());
        });

        var selectedDepartments = [];
        $('#departmentCheckboxes input[type="checkbox"]:checked').each(function () {
            selectedDepartments.push($(this).val());
        });

        var startDate = $('#startDate').val();
        var endDate = $('#endDate').val();
        var contentId = $('#contentId').val();
        var isValid = true;

        if (!startDate) {
            document.getElementById("StartDateError").innerText = "StartDate is required.";
            isValid = false;
        }
        if (new Date(startDate) < new Date().setHours(0, 0, 0, 0)) {
            const currentDate = new Date().toISOString().split('T')[0]; // Get the current date in YYYY-MM-DD format
            document.getElementById("StartDateError").innerText = `StartDate has to be equal to or after ${currentDate}`;
            isValid = false;
        }
        if (!endDate) {
            document.getElementById("EndDateError").innerText = "EndDate is required.";
            isValid = false;
        }
        if (startDate > endDate) {
            document.getElementById("StartDateError").innerText = "StartDate must be before EndDate.";
            isValid = false;
        }
        var conId = parseInt(contentId, 10);
        if (isNaN(conId) || conId <= 0) {
            document.getElementById("ContentIdError").innerText = "Please select a valid content.";
            isValid = false;
        }

        if (selectedDeviceTypes.length === 0) {
            document.getElementById("DeviceTypeError").innerText = "Please select at least one device type.";
            isValid = false;
        }

        if (selectedFloors.length === 0 && selectedDepartments.length === 0) {
            if (selectedDeviceTypes.length !== 0) {
                document.getElementById("Floor_DepartmentsError").innerText = "Please select at least one floor or department.";
            }
            isValid = false;
        }
        if (!isValid) {
            return;
        }
        let floors1 = selectedFloors.length > 0 ? selectedFloors : null;
        let departments1 = selectedDepartments.length > 0 ? selectedDepartments : null;

        var scheduleData = {
            StartDate: startDate,
            EndDate: endDate,
            ContentId: contentId,
            DeviceType: selectedDeviceTypes,
            Floors: floors1,
            Departments: departments1,
            Status: "SCHEDULED"
        };

        $.ajax({
            url: urlCreateSchedule,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(scheduleData),
            headers: {
                'Authorization': 'Bearer ' + token
            },
            success: function (response) {
                console.log(response.json);
                alert('Schedule created successfully!');
                window.location.href = '/Admin/Schedule/Index';
            },
            error: function (xhr, status, error) {
                alert('Error creating schedule.');
            }
        });
    });
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
