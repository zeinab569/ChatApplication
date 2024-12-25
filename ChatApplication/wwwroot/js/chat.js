$(document).ready(function () {
    var currentUserId = $("#chat-data").data("currentUserId");
    var receiverId = $("#chat-data").data("receiverId");

    // Build SignalR connection
    var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

    // Start the connection
    connection.start().then(function () {
        console.log("Connected!");
    }).catch(function (err) {
        console.error(err.toString());
    });

    // Handle sending messages
    $("#send-btn").click(function () {
        var message = $(".message-field").val().trim();
        if (message) {
            // Invoke ChatHub SendMessage function
            connection.invoke("SendMessage", receiverId, message).then(function () {
                $(".message-field").val("").focus();
            }).catch(function (err) {
                console.error("Error sending message: " + err.toString());
            });
        }
    });

    // Handle receiving messages
    connection.on("ReceiveMessage", function (message, date, time, senderId) {
        var messge = '';
        if (senderId == currentUserId) {
            messge = `
            <div class="message sent-message">
                <p>${message}</p>
                <span>${time}</span>
            </div>
        `;
        } else {
            messge = `
            <div class="message received-message">
                <p>${message}</p>
                <span>${time}</span>
            </div>
        `;
        }

        $(".messages-box").append(messge);
        refreshChatBox();
    });

    function refreshChatBox() {
        var chatBox = $(".messages-box");
        chatBox.scrollTop(chatBox[0].scrollHeight);
    }
    $(function () {
        refreshChatBox();
    });

});

