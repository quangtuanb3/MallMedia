// auth.js
export function checkUserAuthentication() {
    return new Promise((resolve, reject) => {
        var currentUser = null;

        // Lấy token từ localStorage
        var token = localStorage.getItem('authToken');

        if (!token) {
            // Nếu không có token, chuyển hướng đến trang đăng nhập
            window.location.href = '/Auth/Login';
            reject('No auth token found');
            return;
        }

        // Nếu có token, gọi API để lấy thông tin người dùng
        fetch('https://localhost:7199/api/identity/currentUser', {
            method: 'GET',
            headers: {
                'Authorization': 'Bearer ' + token
            }
        })
        .then(response => {
            if (!response.ok) {
                // Nếu có lỗi với response (ví dụ: token không hợp lệ), chuyển hướng tới đăng nhập
                window.location.href = '/Auth/Login';
                reject('Failed to authenticate');
                return;
            }
            return response.json();
        })
        .then(data => {
            currentUser = data;

            if (data.roles && !data.roles.includes('Admin')) {
                // Nếu không phải admin, chuyển hướng tới trang AccessDenied
                window.location.href = '/Auth/AccessDenied';
                reject('Access denied');
                return;
            }
            resolve(currentUser);  // Trả về currentUser khi xác thực thành công
        })
        .catch(error => {
            console.error('Lỗi khi lấy thông tin người dùng:', error);
            window.location.href = '/Auth/Login';  // Nếu có lỗi, chuyển hướng về trang login
            reject(error);
        });
    });
}
