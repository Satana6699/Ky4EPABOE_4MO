const apiBaseUrl = "/api/tariffPlans";
let currentPage = 1; // Текущая страница
const itemsPerPage = 8; // Количество записей на странице

async function loadData(page = 1) {
    try {
        const nameFilter = document.getElementById("filter-name").value || "";
        const token = localStorage.getItem('token');
        const response = await axios.get(`${apiBaseUrl}`, {
            params: {
                page,
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
        const tableTitle = "Тарифные планы";
        const tableHead = `
            <tr>
                <th>Имя</th>
                <th>Абонентская плата</th>
                <th>Цена местного звонка</th>
                <th>Цена междугороднего звонка</th>
                <th>Цена международного звонка</th>
                <th>Тип счета</th>
                <th>Цена смс</th>
                <th>Цена ммс</th>
                <th>Цена за передачу данных</th>
                <th>Действия</th>
            </tr>
        `;
        const tableBody = response.data.items.map(item => `
            <tr data-id="${item.id}">
                <td contenteditable="false">${item.name}</td>
                <td contenteditable="false" oninput="validateDecimalInput(this)">${item.subscriptionFee}</td>
                <td contenteditable="false" oninput="validateDecimalInput(this)">${item.localCallCost}</td>
                <td contenteditable="false" oninput="validateDecimalInput(this)">${item.longDistanceCallCost}</td>
                <td contenteditable="false" oninput="validateDecimalInput(this)">${item.internationalCallCost}</td>
                <td contenteditable="false">${item.billingType}</td>
                <td contenteditable="false" oninput="validateDecimalInput(this)">${item.smsCost}</td>
                <td contenteditable="false" oninput="validateDecimalInput(this)">${item.mmsCost}</td>
                <td contenteditable="false" oninput="validateDecimalInput(this)">${item.dataTransferCost}</td>
                <td class="actions">
                    <a class="edit-buttons" href="javascript:void(0);" onclick="editRow(this)" title="Edit">
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

function validateDecimalInput(cell) {
    const value = cell.innerText;
    const isValid = /^\d*\.?\d*$/.test(value);
    if (!isValid) {
        cell.innerText = '';
        alert('Пожалуйста, введите корректное число.');
    }
}

// Инициализация
loadData();