document.addEventListener("DOMContentLoaded", function () {
    const profilForm = document.getElementById("profilForm");
    const fullNameInput = document.getElementById("fullName");
    const emailInput = document.getElementById("email");
    const licenseInput = document.getElementById("license");
    
    
    const userData = {
        fullname="Galip Duger",
        email="dugergalip@gmail.com",
        license: 5
    };

    

    function loadUserData() {
        fullNameInput.value = userData.fullname;
        emailInput.value = userData.email;
        licenseInput.value = userData.license;
    }

   
    function saveUserData() {
        userData.fullname = fullNameInput.value;
        userData.email = emailInput.value;
        userData.license = licenseInput.value;
    }

    loadUserData();

    profilForm.addEventListener("submit", function (event) {
        event.preventDefault(); // Prevent the form submitting the traditional way
        saveUserData();
    });
});