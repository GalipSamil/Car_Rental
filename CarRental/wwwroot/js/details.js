document.addEventListener('DOMContentLoaded', function () {
    const carList = document.getElementById('carList');

    for (let i = 1; i <= 17; i++) {
        const carCard = document.createElement('div');
        carCard.className = 'col-md-6 mb-4';
        carCard.innerHTML = `
            <div class="card h-100">
                <img src="../images/car${i.toString().padStart(2, '0')}.png" class="card-img-top" alt="Car ${i}">
                <div class="card-body">
                    <h5 class="card-title">Brand ${i % 3 + 1} - Model ${i}</h5>
                    <p class="card-text">
                        <strong>Type:</strong> Type ${i % 3 + 1}<br>
                        <strong>Transmission:</strong> ${i % 2 === 0 ? 'Automatic' : 'Manual'}<br>
                        <strong>Fuel:</strong> ${['Petrol', 'Diesel', 'Electric'][i % 3]}<br>
                        <strong>Mileage:</strong> ${Math.floor(Math.random() * 100000)} km<br>
                        <strong>Daily Price:</strong> $${Math.floor(Math.random() * 100) + 30}<br>
                        <strong>Licence Plate:</strong> ABC-${i.toString().padStart(3, '0')}
                    </p>
                    <a href="#" class="btn btn-primary">Book Now</a>
                </div>
            </div>
        `;
        carList.appendChild(carCard);
    }
});
