function info(deleteButton)
{
    const row = deleteButton.closest('tr');
    const id = row.dataset.id;

    const modal = document.getElementById("modal");
    const modalContent = modal.querySelector(".modal-info-content");

    modalContent.innerHTML = 
    `
        <h3>Информация о тарифе</h3>
        <p><strong>Абонентская плата:</strong> ${row.cells[0].innerText} р</p>
        <p><strong>Цена местного звонка:</strong> ${row.cells[1].innerText} р</p>
        <p><strong>Цена междугороднего звонка:</strong> ${row.cells[2].innerText} р</p>
        <p><strong>Цена международного звонка:</strong> ${row.cells[3].innerText} р</p>
        <p><strong>Тип счета:</strong> ${row.cells[4].innerText}</p>
        <p><strong>Цена смс:</strong> ${row.cells[5].innerText} р</p>
        <p><strong>Цена ммс:</strong> ${row.cells[6].innerText} р</p>
        <p><strong>Цена за передачу данных:</strong> ${row.cells[7].innerText} р</p>
        <button onclick="closeModal()">Close</button>
        <button class="edit-buttons" onclick="deleteRow('${id}')">Delete</button>
    `;

    modal.style.display = "block";
    const editButtons = document.querySelectorAll('.edit-buttons');
    if (localStorage.getItem('role') === 'admin') {
        // Показать все кнопки редактирования
        editButtons.forEach(button => {
            button.style.display = 'inline-block';
        });
    } else {
        // Скрыть все кнопки редактирования
        editButtons.forEach(button => {
            button.style.display = 'none';
        });
    }
}
