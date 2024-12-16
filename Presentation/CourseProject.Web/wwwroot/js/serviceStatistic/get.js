const apiBaseUrl = "/api/serviceStatistics";
let currentPage = 1; 
const itemsPerPage = 10; 

async function loadData(page = 1) {
    try {
        const nameFilter = document.getElementById("filter-name").value || "";
        const token = localStorage.getItem('token');

        const response = await axios.get(`${apiBaseUrl}`, {
            params: {
                page: page,
                pageSize: itemsPerPage,
                name: nameFilter,
            },
            headers: {
                Authorization: `Bearer ${token}`,
            },
        });

        // Создание переменных для таблицы
        const itemsLength = response.data.items.length;
        const totalCount = response.data.totalCount;
        const tableTitle = "Сервисные контракты";
        const tableHead = `
                <tr>
                <th>Имя тарифного плана</th>
                <th>Длительность звонков</th>
                <th>Колличество СМС</th>
                <th>Колличество ММС</th>
                <th>Объем передачи данных</th>
                <th>Действия</th>
            </tr>
        `;
        const tableBody = response.data.items.map(item => `
        <tr data-id="${item.id}">
            <td data-field="serviceContract" data-service-contract-id="${item.serviceContract.id}">${item.serviceContract.tariffPlanName}</td>
            <td contenteditable="false" oninput="validateInput(this)">${item.callDuration}</td>
            <td contenteditable="false" oninput="validateInput(this)">${item.smsCount}</td>
            <td contenteditable="false" oninput="validateInput(this)">${item.mmsCount}</td>
            <td contenteditable="false" oninput="validateInput(this)">${item.dataTransferAmount}</td>
            <td class="actions">
                <a href="javascript:void(0);" onclick="editRow(this)" title="Edit">
                    <i class="bi bi-pencil-fill"></i>
                </a>
                <a href="javascript:void(0);" onclick="info(this)" title="Delete Item">
                    <i class="bi bi-eye-fill"></i>
                </a>
            </td>
        </tr>
    `).join('');

        // Создание таблицы
        createTable(itemsLength, totalCount, page, tableTitle, tableHead, tableBody);
    } catch (error) {
        ERROR(error);
    }
}

function formatISODate(isoDate) {
    const date = new Date(isoDate);

    // Опции для форматирования
    const options = {
        day: 'numeric',
        month: 'long',
        year: 'numeric',
        hour: '2-digit',
        minute: '2-digit',
    };

    // Форматирование даты
    return date.toLocaleString('ru-RU', options);
}

function validateInput(cell) {
    const value = cell.textContent;
    if (!/^\d*\.?\d*$/.test(value)) {
        cell.textContent = value.replace(/[^0-9.]/g, '');
    }
}


// Инициализация
loadData();