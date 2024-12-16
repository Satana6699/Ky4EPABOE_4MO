function editRow(editButton) {
    const row = editButton.closest('tr');
    const cells = Array.from(row.querySelectorAll('td')).filter(cell => !cell.classList.contains('actions'));
    const isEditing = row.classList.contains('editing');


    if (isEditing) {
        // Сохранение изменений
        const id = row.dataset.id;
        const updatedData = {
            id: id,
            serviceContractId: cells[0].dataset.serviceContractId,
            callDuration: cells[1].innerText.trim(),
            smsCount: cells[2].innerText.trim(),
            mmsCount: cells[3].innerText.trim(),
            dataTransferAmount: cells[4].innerText.trim(),
        };

        saveChanges(id, updatedData, row);
    } else {
        // Начало редактирования
        row.classList.add('editing');
        row.dataset.originalData = JSON.stringify(cells.map(cell => cell.innerText.trim()));

        cells.forEach(cell => {
            if (cell.dataset.field === "serviceContract") {
                cell.addEventListener('click', () => openSelectModal(cell));
            }
        });
        cells.forEach(cell => cell.setAttribute('contenteditable', 'true'));

        editButton.innerHTML = '<i class="bi bi-check-circle-fill"></i>';
        editButton.title = "Save";

        const cancelButton = document.createElement('a');
        cancelButton.innerHTML = '<i class="bi bi-x-circle-fill"></i>';
        cancelButton.title = "Cancel";
        cancelButton.className = "cancel-button";
        cancelButton.onclick = () => cancelEditingDiseaseSymptom(row);
        row.querySelector('td.actions').appendChild(cancelButton);
    }
}

async function saveChanges(id, updatedData, row) {
    try {
        const response = await axios.put(`${apiBaseUrl}/${id}`, updatedData, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`,
            },
        });

        row.classList.remove('editing');
        const cells = Array.from(row.querySelectorAll('td')).filter(cell => !cell.classList.contains('actions'));

        location.reload();

        cells.forEach(cell => {
            cell.removeEventListener('click', openSelectModal); 
            cell.classList.remove('editable'); 
        });

        // Отключаем кнопки редактирования
        const editButton = row.querySelector('a[title="Save"]');
        editButton.innerHTML = '<i class="bi bi-pencil-fill"></i>';
        editButton.title = "Edit";

        const cancelButton = row.querySelector('.cancel-button');
        if (cancelButton) cancelButton.remove();

    } catch (error) {
        console.error("Error saving changes:", error);
        alert("Failed to save changes. Please try again.");
    }
}
function cancelEditingDiseaseSymptom(row) {
    const cells = Array.from(row.querySelectorAll('td')).filter(cell => !cell.classList.contains('actions'));
    const originalData = JSON.parse(row.dataset.originalData);

    // Возвращаем исходные значения
    cells.forEach((cell, index) => {
        cell.innerText = originalData[index];
    });

    row.classList.remove('editing');

    const editButton = row.querySelector('a[title="Save"]');
    if (editButton) {
        editButton.innerHTML = '<i class="bi bi-pencil-fill"></i>';
        editButton.title = "Edit";
    }

    const cancelButton = row.querySelector('.cancel-button');
    if (cancelButton) cancelButton.remove();

    cells.forEach(cell => {
        cell.removeEventListener('click', openSelectModal); 
    });
    cancelEditing(row);
}
function openSelectModal(cell) {
    const type = cell.dataset.field === "serviceContract" ? 'serviceContract' : 'serviceContract';

    const existingModal = document.querySelector('.modal-list');
    if (existingModal) {
        existingModal.remove();
    }

    // Создаем новое модальное окно
    const modal = document.createElement('div');
    modal.classList.add('modal-list');
    modal.innerHTML = `
        <div class="modal-list-content">
            <div class="modal-list-header">
                <span class="close">&times;</span>
                <h2>Select ${type}</h2>
            </div>
            <div class="modal-list-body">
                <table id="select-table">
                    <!-- Данные для выбора загружаются динамически -->
                </table>
            </div>
        </div>
    `;
    document.body.appendChild(modal);

    modal.querySelector('.close').onclick = () => modal.remove();

    // Позиционируем модальное окно под ячейкой
    const cellRect = cell.getBoundingClientRect();
    modal.style.left = `${cellRect.left}px`;
    modal.style.top = `${cellRect.bottom + window.scrollY}px`; // Учитываем прокрутку страницы

    loadSelectData(type, cell);
}


async function loadSelectData(type, cell) {
    try {
        const response = await axios.get(`${apiBaseUrl}/${type}s`, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`,
            },
        });

        const table = document.getElementById('select-table');
        table.innerHTML = '';

        response.data.forEach(item => {
            const row = document.createElement('tr');
            row.dataset.id = item.id;
            row.innerHTML = `<td>${item.tariffPlanName}</td>`;
            row.onclick = () => selectItem(item, cell, type);
            table.appendChild(row);
        });

    } catch (error) {
        console.error("Error loading select data:", error);
        alert("Failed to load data. Please try again.");
    }
}
function selectItem(item, cell, type) {
    if (type === 'serviceContract') {
        cell.dataset.serviceContractId = item.id;
        cell.innerText = item.tariffPlanName;
    } 

    const modal = document.querySelector('.modal-list');
    if (modal) modal.remove();
}

function handleDateChange(event) {
    cell.setAttribute('date-str', Date(event.target.value).toISOString());
}