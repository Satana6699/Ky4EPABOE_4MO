function addEmptyRow() {
    const table = document.querySelector("#table-container table tbody");

    // Создаём новую строку
    const newRow = document.createElement("tr");
    newRow.dataset.id = "new"; // Временный ID для новой строки
    let dateStr = new Date().toISOString();
    newRow.innerHTML = `
        <td contenteditable="false"></td>
            <td data-field="employee" data-employee-id="0"></td>
            <td data-field="subscriber" data-subscriber-id="0"></td>
            <td contenteditable="false"></td>
            <td contenteditable="false" date-str="0">
                <input type ="datetime-local" id="datetime" name="datetime" value="${dateStr}"/>
            </td>
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
        if (cell.dataset.field === "employee" || cell.dataset.field === "subscriber") {
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
    const dateStr = cells[4].getAttribute('date-str');

    // Собираем данные из строки
    const id = row.dataset.id;
    const newdate = new Date(dateStr);
    const updatedData = {
        id: id,
        tariffPlanName: cells[0].innerText.trim(),
        employeeId: cells[1].dataset.employeeId,
        subscriberId: cells[2].dataset.subscriberId,
        phoneNumber: cells[3].innerText.trim(),
        contractDate: newdate,
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