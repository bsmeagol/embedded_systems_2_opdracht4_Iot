"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
    
});

connection.on("PreviousMessages", function (user1, message1, user2, message2, user3, message3) {
    
    var encodedMsg = user3 + " says " + message3;
    var li1 = document.createElement("li");
    li1.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li1);

    var encodedMsg = user2 + " says " + message2;
    var li2 = document.createElement("li");
    li2.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li2);

    var encodedMsg = user1 + " says " + message1;
    var li3 = document.createElement("li");
    li3.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li3);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });   
    event.preventDefault();
});



document.getElementById("showPrev").addEventListener("click", function (event) {
    
    connection.invoke("ShowPrevMessages").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
    document.getElementById("showPrev").disabled = 'true';
});