const apiBaseUrl = "/api/employees";
let currentPage = 1;
const itemsPerPage = 10;

function createTable(items, totalItems, currentPage) {
    const container = document.getElementById("table-container");
    container.innerHTML = "";

    if (items.length === 0) {
        container.innerHTML = `В таблице нет данных`;
        return;
    }

    const table = document.createElement("table");
    const caption = document.createElement("caption");
    caption.innerHTML = `
        Сотрудники List
            <a href="javascript:void(0);" onclick="addEmptyRow()" title="Add Item">
            Добавить
        </a>`;
    table.appendChild(caption);

    const thead = document.createElement("thead");
    thead.innerHTML = `
        <tr>
            <th>ФИО</th>
            <th>Должность</th>
            <th>Образование</th>
            <th>Редактирование</th>
        </tr>
    `;
    table.appendChild(thead);

    const tbody = document.createElement("tbody");
    tbody.innerHTML = items.map(item => `
        <tr data-id="${item.id}">
            <td contenteditable="false">${item.fullName}</td>
            <td contenteditable="false">${item.position}</td>
            <td contenteditable="false">${item.education}</td>
            <td class="actions">
                <a href="javascript:void(0);" onclick="edit(this)" title="Edit">
                    <i class="bi bi-pencil-fill"></i>
                </a>
                <a href="javascript:void(0);" onclick="info(this)" title="Delete Item">
                    <i class="bi bi-eye-fill"></i>
                </a>
            </td>
        </tr>
    `).join('');

    table.appendChild(tbody);
    container.appendChild(table);

    renderPagination(totalItems, currentPage);
}

function renderPagination(totalItems, currentPage) {
    const container = document.getElementById("table-container");
    const totalPages = Math.ceil(totalItems / itemsPerPage);

    const paginationDiv = document.createElement("div");
    paginationDiv.style.textAlign = "center";
    paginationDiv.style.marginTop = "10px";

    const prevButton = document.createElement("button");
    prevButton.innerText = "←";
    prevButton.style.margin = "0 5px";
    prevButton.style.padding = "5px 10px";
    prevButton.style.background = currentPage > 1 ? "#3498DB" : "#ccc";
    prevButton.style.color = currentPage > 1 ? "#fff" : "#666";
    prevButton.style.border = "1px solid #ccc";
    prevButton.style.borderRadius = "4px";
    prevButton.style.cursor = currentPage > 1 ? "pointer" : "not-allowed";
    prevButton.onclick = () => {
        if (currentPage > 1) loadData(currentPage - 1);
    };

    paginationDiv.appendChild(prevButton);

    const currentPageText = document.createElement("span");
    currentPageText.innerText = `Page ${currentPage} of ${totalPages}`;
    currentPageText.style.margin = "0 10px";
    paginationDiv.appendChild(currentPageText);

    const pageInput = document.createElement("input");
    pageInput.type = "number";
    pageInput.min = 1;
    pageInput.max = totalPages;
    pageInput.value = currentPage;
    pageInput.style.width = "50px";
    pageInput.style.margin = "0 5px";
    pageInput.onchange = () => {
        const inputPage = parseInt(pageInput.value, 10);
        if (!isNaN(inputPage)) {
            if (inputPage < 1) loadData(1);
            else if (inputPage > totalPages) loadData(totalPages);
            else loadData(inputPage);
        }
    };
    paginationDiv.appendChild(pageInput);

    const nextButton = document.createElement("button");
    nextButton.innerText = "→";
    nextButton.style.margin = "0 5px";
    nextButton.style.padding = "5px 10px";
    nextButton.style.background = currentPage < totalPages ? "#3498DB" : "#ccc";
    nextButton.style.color = currentPage < totalPages ? "#fff" : "#666";
    nextButton.style.border = "1px solid #ccc";
    nextButton.style.borderRadius = "4px";
    nextButton.style.cursor = currentPage < totalPages ? "pointer" : "not-allowed";
    nextButton.onclick = () => {
        if (currentPage < totalPages) loadData(currentPage + 1);
    };

    paginationDiv.appendChild(nextButton);
    container.appendChild(paginationDiv);
}

async function loadData(page = 1) {
    try {
        const nameFilter = document.getElementById("filter-name").value || "";

        const response = await axios.get(`${apiBaseUrl}`, {
            params: {
                page,
                pageSize: itemsPerPage,
                name: nameFilter,
            },
        });

        const pageResult = response.data;

        createTable(pageResult.items, pageResult.totalCount, page);
    } catch (error) {
        console.error("Error fetching employees:", error);
        document.getElementById("table-container").innerHTML =
            `<p>Error loading employees. Please try again later.</p>`;
    }
}

function nameFilter() {
    currentPage = 1;
    loadData(currentPage);
}

loadData();
