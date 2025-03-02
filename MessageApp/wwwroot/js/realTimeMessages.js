const socket = new WebSocket("ws://localhost:8080/ws");

socket.onopen = () => {
    console.log("WebSocket connection established.");
};

socket.onmessage = (event) => {
    try {
        const message = JSON.parse(event.data);
        console.log("Received message:", message); 

        if (!message.timestamp || !message.content) {
            console.warn("Message is missing required fields:", message);
            return;
        }

        // Преобразуем timestamp в объект Date
        const date = new Date(message.timestamp);
        if (isNaN(date.getTime())) {
            console.error("Invalid date format:", message.timestamp);
            return;
        }
        
        const formattedTimestamp = `${String(date.getDate()).padStart(2, "0")}.${String(date.getMonth() + 1).padStart(2, "0")}.${date.getFullYear()}, ${String(date.getHours()).padStart(2, "0")}:${String(date.getMinutes()).padStart(2, "0")}:${String(date.getSeconds()).padStart(2, "0")}`;

        const messagesDiv = document.getElementById("messages");
        messagesDiv.innerHTML += `<p>${formattedTimestamp}: ${message.content} (Seq: ${message.sequenceNumber})</p>`;
    } catch (error) {
        console.error("Error processing message:", error);
    }
};

socket.onerror = (error) => {
    console.error("WebSocket error:", error);
};

socket.onclose = (event) => {
    console.log(`WebSocket connection closed. Code: ${event.code}, Reason: ${event.reason}`);
};
