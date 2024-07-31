document.addEventListener('DOMContentLoaded', function () {
    const registerForm = document.getElementById('registerForm');

    registerForm.addEventListener('submit', function (event) {
        event.preventDefault(); 

        const fullname = document.getElementById('fullname').value;
        const email = document.getElementById('email').value;
        const username = document.getElementById('username').value;
        const password = document.getElementById('password').value;

        
        const users = JSON.parse(localStorage.getItem("users")) || [];

       
        const userExists = users.some(user => user.username === username);
        if (userExists) {
            alert("Username already taken.Please choose new one");
        }   else {
                
                users.push({ fullname, email, username, password });
                
                localStorage.setItem("users", JSON.stringify(users));

                alert("Registration succesful!");
                window.location.href = "login.html";
            }       
    });
});
