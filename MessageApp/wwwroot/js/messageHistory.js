document.getElementById("getHistoryForm").addEventListener("submit", async (e) => {
    e.preventDefault();
    const start = document.getElementById("start").value;
    const end = document.getElementById("end").value;

    const response = await fetch(`/api/messages?start=${start}&end=${end}`);
    const messages = await response.json();

    const historyDiv = document.getElementById("history");
    historyDiv.innerHTML = messages.map(msg => {
        const date = new Date(msg.timestamp).toLocaleString("ru-RU", {
            year: "numeric",
            month: "2-digit",
            day: "2-digit",
            hour: "2-digit",
            minute: "2-digit",
            second: "2-digit"
        });
        return `<p>${date}: ${msg.content} (Seq: ${msg.sequenceNumber})</p>`;
    }).join("");
});
