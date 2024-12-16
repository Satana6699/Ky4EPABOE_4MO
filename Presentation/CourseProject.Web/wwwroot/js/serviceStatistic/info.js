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
        <p><strong>Имя тарифного плана:</strong> ${item.data.serviceContract.tariffPlanName}</p>
        <p><strong>Длительность звонков:</strong> ${item.data.CallDuration}</p>
        <p><strong>Колличество СМС:</strong> ${item.data.data.SmsCount}</p>
        <p><strong>Колличество ММС:</strong> ${item.data.MmsCount}</p>
        <p><strong>Объем передачи данных:</strong> ${item.data.DataTransferAmount}</p>
        <button onclick=\"closeModal()\">Close</button>
        <button onclick=\"deleteRow('${row.dataset.id}')\">Delete</button>
    `;

    modal.style.display = "block";
}