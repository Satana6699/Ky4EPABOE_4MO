function addEmptyRow() {
    const table = document.querySelector("#table-container table tbody");

    // Создаём новую строку
    const newRow = document.createElement("tr");
    newRow.dataset.id = "new"; // Временный ID для новой строки

    newRow.innerHTML = `
        <td style="padding: 8px;" contenteditable="true"></td>
        <td style="padding: 8px;" contenteditable="true" oninput="validateDecimalInput(this)"></td>
        <td style="padding: 8px;" contenteditable="true" oninput="validateDecimalInput(this)"></td>
        <td style="padding: 8px;" contenteditable="true" oninput="validateDecimalInput(this)"></td>
        <td style="padding: 8px;" contenteditable="true" oninput="validateDecimalInput(this)"></td>
        <td style="padding: 8px;" contenteditable="true"></td>
        <td style="padding: 8px;" contenteditable="true" oninput="validateDecimalInput(this)"></td>
        <td style="padding: 8px;" contenteditable="true" oninput="validateDecimalInput(this)"></td>
        <td style="padding: 8px;" contenteditable="true" oninput="validateDecimalInput(this)"></td>
        <td style="padding: 8px;">
            <a href="javascript:void(0);" onclick="saveNewRow(this)" title="Save">
                <i class="bi bi-check-circle-fill"></i>
            </a>
            <a href="javascript:void(0);" onclick="cancelNewRow(this)" title="Cancel">
                <i class="bi bi-x-circle-fill"></i>
            </a>
        </td>
    `;

    // Вставляем новую строку в начало таблицы
    table.prepend(newRow);
}
async function saveNewRow(saveButton) {
    const row = saveButton.closest("tr");
    const cells = row.querySelectorAll("td[contenteditable]");

    const updatedData = {
        name: cells[0].innerText.trim(),
        subscriptionFee: cells[1].innerText.trim(),
        localCallCost: cells[2].innerText.trim(),
        longDistanceCallCost: cells[3].innerText.trim(),
        internationalCallCost: cells[4].innerText.trim(),
        billingType: cells[5].innerText.trim(),
        smsCost: cells[6].innerText.trim(),
        mmsCost: cells[7].innerText.trim(),
        dataTransferCost: cells[8].innerText.trim(),
    };

    // Проверяем заполненность поля
    if (!updatedData.name && !updatedData.subscriptionFee && !updatedData.localCallCost) {
        alert("Не все поля заполнены");
        return;
    }

    try {
        // Отправляем данные на сервер
        const response = await axios.post(apiBaseUrl, updatedData, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token') }`,
            },
        });

        if (response.status === 201) {
            alert("Данные созданы успешно!");

        location.reload();

        } else {
            throw new Error("Failed to create Symptom.");
        }
    } catch (error) {
        console.error("Error creating Symptom:", error);
        alert("Failed to create Symptom. Please try again.");

        // Удаляем строку при ошибке
        row.remove();
    }
}

function cancelNewRow(cancelButton) {
    const row = cancelButton.closest("tr");
    row.remove(); // Удаляем строку
}
