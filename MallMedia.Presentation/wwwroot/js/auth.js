// auth.js
export function checkUserAuthentication(requiredRole = null) {
    return new Promise((resolve, reject) => {
        // Get the token from localStorage
        const token = localStorage.getItem('authToken');

        if (!token) {
            // Redirect to login if no token
            window.location.href = '/Auth/Login';
            reject('No auth token found');
            return;
        }

        // Call the API to fetch current user information
        fetch('https://localhost:7199/api/identity/currentUser', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`,
            },
        })
            .then(response => {
                if (!response.ok) {
                    // Redirect to login if the response is not okay
                    window.location.href = '/Auth/Login';
                    reject('Failed to authenticate');
                    return;
                }
                return response.json();
            })
            .then(data => {
                // Check if the required role is provided and validate
                if (requiredRole && (!data.roles || !data.roles.includes(requiredRole))) {
                    // Redirect to AccessDenied if the user lacks the required role
                    window.location.href = '/Auth/AccessDenied';
                    reject(`Access denied for role: ${requiredRole}`);
                    return;
                }

                // Resolve with user data if authentication and role check pass
                resolve(data);
            })
            .catch(error => {
                console.error('Error fetching user information:', error);
                window.location.href = '/Auth/Login'; // Redirect to login on error
                reject(error);
            });
    });
}
