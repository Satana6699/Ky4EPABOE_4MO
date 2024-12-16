function addEmptyRow() {
    const table = document.querySelector("#table-container table tbody");

    // Создаём новую строку
    const newRow = document.createElement("tr");
    newRow.dataset.id = "new"; // Временный ID для новой строки
    let dateStr = new Date().toISOString();
    newRow.innerHTML = `
        <td data-field="serviceContract" data-service-contract-id="0"></td>
        <td contenteditable="false" oninput="validateInput(this)"></td>
        <td contenteditable="false" oninput="validateInput(this)"></td>
        <td contenteditable="false" oninput="validateInput(this)"></td>
        <td contenteditable="false" oninput="validateInput(this)"></td>
        <td style="padding: 8px;">
            <a href="javascript:void(0);" onclick="saveNewRow(this)" title="Save">
                <i class="bi bi-check-circle-fill"></i>
            </a>
            <a href="javascript:void(0);" onclick="cancelNewRow(this)" title="Cancel">
                <i class="bi bi-x-circle-fill"></i>
            </a>
        </td>
    `;

    
    const cells = Array.from(newRow.querySelectorAll('td')).filter(cell => !cell.classList.contains('actions'));
    newRow.classList.add('editing');
    newRow.dataset.originalData = JSON.stringify(cells.map(cell => cell.innerText.trim()));

    cells.forEach(cell => {
        if (cell.dataset.field === "serviceContract") {
            cell.addEventListener('click', () => openSelectModal(cell));
        }
    });
    cells.forEach(cell => cell.setAttribute('contenteditable', 'true')); // Только данные можно редактировать

    // Вставляем новую строку в начало таблицы
    table.prepend(newRow);

    const datetimeInput = document.querySelector('#datetime');
    datetimeInput.addEventListener('change', (event) => {
        cells[4].setAttribute('date-str', new Date(event.target.value).toISOString());
    });

}
async function saveNewRow(saveButton) {
    const row = saveButton.closest("tr");
    const cells = Array.from(row.querySelectorAll('td')).filter(cell => !cell.classList.contains('actions'));

    // Собираем данные из строки
    const updatedData = {
        serviceContractId: cells[0].dataset.serviceContractId,
        callDuration: cells[1].innerText.trim(),
        smsCount: cells[2].innerText.trim(),
        mmsCount: cells[3].innerText.trim(),
        dataTransferAmount: cells[4].innerText.trim(),
    };

    // Проверяем заполненность поля
    if (!updatedData && !updatedData.Id) {
        alert("Не все поля заполнены");
        return;
    }

    try {
        // Отправляем данные на сервер
        const response = await axios.post(apiBaseUrl, updatedData, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`,
            },
        });

        if (response.status === 201) {
            alert("Данные созданые успешно!");
            location.reload();
        }
        else {
            throw new Error("Ошбика создания данных");
        }
    } catch (error) {
        console.error("Ошбика создание данных:", error);
        alert("Ошбика при создании данных. Потворите попытку позже");

        // Удаляем строку при ошибке
        row.remove();
    }
}

function cancelNewRow(cancelButton) {
    const row = cancelButton.closest("tr");
    row.remove(); // Удаляем строку
}