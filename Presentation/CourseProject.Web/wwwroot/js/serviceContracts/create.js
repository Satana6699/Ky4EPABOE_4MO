function addEmptyRow() {
    const table = document.querySelector("#table-container table tbody");

    // Создаём новую строку
    const newRow = document.createElement("tr");
    newRow.dataset.id = "new"; // Временный ID для новой строки

    newRow.innerHTML = `
        <td data-field="disease" data-disease-id="0"></td>
        <td data-field="symptom" data-symptom-id="0"></td>
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
        if (cell.dataset.field === "disease" || cell.dataset.field === "symptom") {
            cell.addEventListener('click', () => openSelectModal(cell));
        }
    });

    // Вставляем новую строку в начало таблицы
    table.prepend(newRow);
}
async function saveNewRow(saveButton) {
    const row = saveButton.closest("tr");
    const cells = Array.from(row.querySelectorAll('td')).filter(cell => !cell.classList.contains('actions'));

    // Собираем данные из строки
    const newDiseaseSymptom = {
        diseaseId: cells[0].dataset.diseaseId,
        symptomId: cells[1].dataset.symptomId
    };

    // Проверяем заполненность поля
    if (!newDiseaseSymptom.diseaseId && !newDiseaseSymptom.symptomId) {
        alert("Не все поля заполнены");
        return;
    }

    try {
        // Отправляем данные на сервер
        const response = await axios.post(apiBaseUrl, newDiseaseSymptom, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`,
            },
        });

        if (response.status === 201) {
            alert("Symptom created successfully!");

            //// Обновляем строку с новыми данными
            //row.dataset.id = response.data.id; // Устанавливаем ID, полученный от сервера
            //row.innerHTML = `
            //<td data-field="disease" data-disease-id="${response.dataset.id}">${item.disease.name}</td>
            //<td data-field="symptom" data-symptom-id="${response.dataset.id}">${item.symptom.name}</td>
            //    <td style="padding: 8px;">
            //        <a href="javascript:void(0);" onclick="editRow(this)" title="Edit">
            //            <i class="bi bi-pencil-fill"></i>
            //        </a>
            //        <a href="javascript:void(0);" onclick="info(this)" title="Delete Item">
            //            <i class="bi bi-eye-fill"></i>
            //        </a>
            //    </td>
            //`;

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