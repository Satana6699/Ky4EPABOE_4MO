document.getElementById('registerForm').addEventListener('submit', async function (e) {
    e.preventDefault();

    const userName = document.getElementById('registerUsername').value;
    const fullName = document.getElementById('registerFullName').value;
    const password = document.getElementById('registerPassword').value;
    const errorMsg = document.getElementById('registerErrorMsg');
    const newUser = {
        userName: userName,
        fullName: fullName,
        password: password,
        role: "",
        passwordTime: new Date(),
    };

    // Clear previous error message
    errorMsg.style.display = 'none';

    try {
        const response = await axios.post('/api/auth/register', newUser);

        if (typeof response.ok !== "undefined" && !response.ok) {
            throw new Error('Ошибка регистрации');
        }

        alert('Регистрация прошла успешно, теперь можете войти в аккаунт');
    } catch (error) {
        console.error("Error fetching symptoms:", error);

        errorMsg.style.display = 'block';
        errorMsg.textContent = error.message;
    }
});
