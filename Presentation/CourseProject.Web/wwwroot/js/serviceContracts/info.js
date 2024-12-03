async function info(deleteButton) {
    const row = deleteButton.closest('tr');
    const item = await axios.get(apiBaseUrl + "/" + row.dataset.id, {
        headers:
        {
            Authorization: `Bearer ${localStorage.getItem('token')}`,
        },
    });
    const modal = document.getElementById("modal");
    const modalContent = modal.querySelector(".modal-info-content");

    modalContent.innerHTML = `
        <h3>Детальная информация</h3>
        <p><strong>Название тарифа:</strong> ${item.data.tariffPlanName}</p>
        <p><strong>Имя сотрудника:</strong> ${item.data.employee.fullName}</p>
        <p><strong>Имя абонента:</strong> ${item.data.subscriber.fullName}</p>
        <p><strong>Номер телефона:</strong> ${item.data.phoneNumber}</p>
        <p><strong>Дата контракта:</strong> ${item.data.contractDate}</p>
        <button onclick=\"closeModal()\">Close</button>
        <button onclick=\"deleteRow('${row.dataset.id}')\">Delete</button>
    `;

    modal.style.display = "block";
}