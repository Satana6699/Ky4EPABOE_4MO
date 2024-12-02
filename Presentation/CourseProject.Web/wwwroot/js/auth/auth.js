const token = localStorage.getItem('token');

if (token !== null) {
    // Показываем кнопку выхода
    displayLogoutButton();
} else {
    // Показываем форму регистрации/входа
    displayAuthForm();
}

function displayAuthForm() {
    const authContainer = document.querySelector('.auth-container');
    authContainer.innerHTML = `
        <form id="authForm">
            <input type="text" id="username" placeholder="Username" required />
            <input type="password" id="password" placeholder="Password" required />
            <button type="submit">Login</button>
            <p id="errorMsg" style="display: none; color: red;"></p>
        </form>
        <button id="registerBtn">Register</button>
    `;

    document.getElementById('authForm').addEventListener('submit', async function (e) {
        e.preventDefault();
        const userName = document.getElementById('username').value;
        const password = document.getElementById('password').value;
        const errorMsg = document.getElementById('errorMsg');
        
        // Clear previous error message
        errorMsg.style.display = 'none';

        try {
            const response = await axios.get('/api/auth/login', {
                params:
                {
                    userName: userName,
                    password: password,
                },
            });

            if (typeof(response.ok) !== "undefined" && !response.ok) {
                throw new Error('Invalid credentials');
            }

            const data = await response.data;
            // Store token in localStorage
            localStorage.setItem('token', data.token);
            localStorage.setItem('role', data.role);

            const accountLink = document.querySelector('a[href="/Home/Auth"]');

            // Проверяем, что ссылка найдена
            if (accountLink) {
                // Меняем текст содержимого
                accountLink.textContent = data.userName;
                localStorage.setItem('accountLinkText', data.userName);
            }

            displayLogoutButton();

            // Перезагрузка страницы
            location.reload();

        } catch (error) {
            errorMsg.style.display = 'block';
            errorMsg.textContent = error.message;
        }
    });

    document.getElementById('registerBtn').addEventListener('click', () => {
        window.location.href = '/Home/Reg'; // Перенаправление на страницу регистрации
    });
}

function displayLogoutButton() {
    const authContainer = document.querySelector('.auth-container');

    // Clear the current form
    authContainer.innerHTML = '';

    // Create Logout button
    const logoutButton = document.createElement('button');
    logoutButton.textContent = 'Logout';
    logoutButton.style.backgroundColor = 'red';
    logoutButton.style.color = 'white';
    logoutButton.style.padding = '10px';
    logoutButton.style.border = 'none';
    logoutButton.style.borderRadius = '4px';
    logoutButton.style.cursor = 'pointer';
    logoutButton.style.fontSize = '16px';

    // Add Logout functionality
    logoutButton.addEventListener('click', () => {
        // Remove token from localStorage
        localStorage.removeItem('token');
        localStorage.removeItem('role');
        localStorage.removeItem('accountLinkText');
        // Redirect to login page
        window.location.href = '/Home/Index';
    });

    // Append Logout button to the container
    authContainer.appendChild(logoutButton);
}
