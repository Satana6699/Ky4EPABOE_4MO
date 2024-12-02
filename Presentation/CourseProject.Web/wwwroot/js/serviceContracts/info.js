async function info(deleteButton) {
    const row = deleteButton.closest('tr');
    const symptom = await axios.get("/api/diseaseSymptoms/" + row.dataset.id, {
        headers:
        {
            Authorization: `Bearer ${localStorage.getItem('token')}`,
        },
    });
    const modal = document.getElementById("modal");
    const modalContent = modal.querySelector(".modal-info-content");

    modalContent.innerHTML = `
        <h3>Детали болезни и симптома</h3>
        <p><strong>Болезнь:</strong> ${symptom.data.disease.name}</p>
        <p><strong>Продолжительность болезни:</strong> ${symptom.data.disease.duration}</p>
        <p><strong>Симптом:</strong> ${symptom.data.disease.symptoms}</p>
        <p><strong>Последствия:</strong> ${symptom.data.disease.consequences}</p>
        <p><strong>Симптом:</strong> ${symptom.data.symptom.name}</p>
        <button onclick=\"closeModal()\">Close</button>
        <button onclick=\"deleteRow('${row.dataset.id}')\">Delete</button>
    `;

    modal.style.display = "block";
}