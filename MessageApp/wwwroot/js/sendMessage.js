document.getElementById("sendMessageForm").addEventListener("submit", async (e) => {
    e.preventDefault();
    const content = document.getElementById("content").value;
    const sequenceNumber = document.getElementById("sequenceNumber").value;

    const response = await fetch("/api/messages", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ content, sequenceNumber }),
    });

    if (response.ok) {
        alert("Message sent successfully!");
    } else {
        alert("Failed to send message.");
    }
});