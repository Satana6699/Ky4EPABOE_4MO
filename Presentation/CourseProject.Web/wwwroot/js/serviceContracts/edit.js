function editRow(editButton) {
    const row = editButton.closest('tr');
    const cells = Array.from(row.querySelectorAll('td')).filter(cell => !cell.classList.contains('actions'));
    const isEditing = row.classList.contains('editing');

    if (isEditing) {
        // Сохранение изменений
        const id = row.dataset.id;
        const updatedData = {
            id: id,
            diseaseId: cells[0].dataset.diseaseId, // Это ID болезни
            symptomId: cells[1].dataset.symptomId // Это ID симптома
        };

        saveChanges(id, updatedData, row);
    } else {
        // Начало редактирования
        row.classList.add('editing');
        row.dataset.originalData = JSON.stringify(cells.map(cell => cell.innerText.trim()));

        cells.forEach(cell => {
            if (cell.dataset.field === "disease" || cell.dataset.field === "symptom") {
                cell.addEventListener('click', () => openSelectModal(cell));
            }
        });

        editButton.innerHTML = '<i class="bi bi-check-circle-fill"></i>';
        editButton.title = "Save";

        // Добавляем кнопку отмены
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

        // Обновляем данные в строке
        cells[0].innerText = response.data.disease.name;
        cells[1].innerText = response.data.symptom.name;
        // Отключаем возможность редактирования (клик по ячейкам)
        cells.forEach(cell => {
            cell.removeEventListener('click', openSelectModal);  // Убираем обработчик события
            cell.classList.remove('editable');  // Можно добавить стиль, если нужно
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

    // Отключаем обработчики кликов, если редактирование отменено
    cells.forEach(cell => {
        cell.removeEventListener('click', openSelectModal);  // Убираем обработчик события
    });
}
function openSelectModal(cell) {
    const type = cell.dataset.field === "disease" ? 'disease' : 'symptom';

    // Удаляем все открытые модальные окна, если они есть
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
            row.innerHTML = `<td>${item.name}</td>`;
            row.onclick = () => selectItem(item, cell, type);
            table.appendChild(row);
        });

    } catch (error) {
        console.error("Error loading select data:", error);
        alert("Failed to load data. Please try again.");
    }
}
function selectItem(item, cell, type) {
    if (type === 'disease') {
        cell.dataset.diseaseId = item.id;
        cell.innerText = item.name;
    } else if (type === 'symptom') {
        cell.dataset.symptomId = item.id;
        cell.innerText = item.name;
    }

    const modal = document.querySelector('.modal-list');
    if (modal) modal.remove();
}