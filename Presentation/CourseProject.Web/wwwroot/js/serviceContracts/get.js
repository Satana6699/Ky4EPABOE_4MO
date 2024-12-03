const apiBaseUrl = "/api/serviceContracts"; // Базовый URL для новой таблицы
let currentPage = 1; // Текущая страница
const itemsPerPage = 10; // Количество записей на странице

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
                <th>Название тарифа</th>
                <th>Имя сотрудника</th>
                <th>Имя абонента</th>
                <th>Номер телефона</th>
                <th>Дата контракта</th>
                <th>Действия</th>
            </tr>
        `;
        const tableBody = response.data.items.map(item => `
        <tr data-id="${item.id}">
            <td contenteditable="false">${item.tariffPlanName}</td>
            <td data-field="employee" data-employee-id="${item.employee.id}">${item.employee.fullName}</td>
            <td data-field="subscriber" data-subscriber-id="${item.subscriber.id}">${item.subscriber.fullName}</td>
            <td contenteditable="false">${item.phoneNumber}</td>
            <td contenteditable="false" date-str="${item.contractDate}">${formatISODate(item.contractDate)}</td>
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

// Инициализация
loadData();