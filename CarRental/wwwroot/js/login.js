document.addEventListener('DOMContentLoaded', function () {
    const loginForm = document.getElementById('loginForm');

    loginForm.addEventListener('submit', function (event) {
        event.preventDefault(); // Prevent form from submitting traditionally

        const username = document.getElementById('username').value;
        const password = document.getElementById('password').value;

        // Get users from localStorage
        const users = JSON.parse(localStorage.getItem('users')) || [];

        // Check if the user exists and the password is correct
        const user = users.find(user => user.username === username && user.password === password);

        if (user) {
            alert('Login successful!');
            window.location.href = 'profile.html'; // Redirect to profile page
        } else {
            alert('Invalid username or password.');
        }
    });
});
