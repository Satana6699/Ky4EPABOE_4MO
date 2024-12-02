const apiBaseUrl = "/api/serviceContracts"; // Базовый URL для новой таблицы
let currentPage = 1; // Текущая страница
const itemsPerPage = 10; // Количество записей на странице

async function loadData(page = 1) {
    try {
        const diseaseFilter = document.getElementById("filter-disease").value || "";
        const symptomFilter = document.getElementById("filter-symptom").value || "";

        const response = await axios.get(`${apiBaseUrl}`, {
            params: {
                page: page,
                pageSize: itemsPerPage,
                nameDisease: diseaseFilter,
                nameSymptom: symptomFilter,
            },
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`,
            },
        });

        // Создание переменных для таблицы
        const itemsLength = response.data.items.length;
        const totalCount = response.data.totalCount;
        const tableTitle = "Болезни и симптомы";
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
            <td data-field="symptom" data-symptom-id="${item.symptom.id}">${item.tariffPlanName}</td>
            <td data-field="disease" data-disease-id="${item.disease.id}">${item.employee.fullName}</td>
            <td data-field="symptom" data-symptom-id="${item.symptom.id}">${item.subscriber.fullName}</td>
            <td data-field="symptom" data-symptom-id="${item.symptom.id}">${item.phoneNumber}</td>
            <td data-field="symptom" data-symptom-id="${item.symptom.id}">${item.contractDate}</td>
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

// Инициализация
loadData();