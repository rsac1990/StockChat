"use strict";

document.getElementById("SendMessageButton").disabled = true;

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.on("ReceiveMessage", function (user, message, currentDate) {
    AddMessage(user, message, currentDate);
});

connection.on("OnConnected", function (messages) {   
    for (var i = 0; i < messages.length; i++) {
        AddMessage(messages[i].username, messages[i].message, messages[i].time);
    }   
});

connection.start().then(function () { InitializeElements(); })
    .catch(function (err) {
        return console.error(err.toString());
    });


function AddMessage(user, message, currentDate) {

    let currentUser = document.getElementById("LoggedUser").textContent;
    let divElement = document.createElement("div");   
    let chatDiv = document.getElementById("ChatMessagesDiv");

    chatDiv.appendChild(divElement);
   
    let messagesCount = chatDiv.childElementCount
    if (messagesCount > 50) {
        chatDiv.removeChild(chatDiv.firstChild);
    }   

    let divMessageContent = "";
    if (currentUser == user) {
        divMessageContent = `<div class='p-2 rounded-3 mb-2 bg-light'><div class='text-end'><span class='text-muted small'>${currentDate}</span><br/><span class="text-primary fw-bolder">Me:</span><br/> ${message}</div></div>`;
    } 
    else {
        if (user == "StockBot") {
            divMessageContent = `<div class='p-2 rounded-3 mb-2 bg-success text-white'><div class='text-start'><span class='small text-white'>${currentDate}</span><br/><b>${user} says:</b><br/>  ${message}</div></div>`;
        }
        else {
            divMessageContent = `<div class='p-2 rounded-3 mb-2 bg-light'><div class='text-start'><span class='text-muted small'>${currentDate}</span><br/><span class="text-success fw-bolder">${user} says:</span><br/>  ${message}</div></div>`;
        }       
    }

    divElement.innerHTML = divMessageContent;

    chatDiv.scrollTop = chatDiv.scrollHeight;
}

function InitializeElements() {

    let user = document.getElementById("LoggedUser").textContent;

    document.getElementById("SendMessageButton").disabled = false;

    connection.invoke("Connect").catch(function (err) {
        return console.error(err.toString());
    });

    document.getElementById("SendMessageButton").addEventListener("click", function (event) {
        
        let message = document.getElementById("MessageTextBox").value;
        let currentDate = new Date().toLocaleString();

        if (message.trim() == "") {
            alert("The message is required.");
        }
        else {

            connection.invoke("SendMessage", user, message, currentDate).catch(function (err) {
                return console.error(err.toString());
            });

            document.getElementById("MessageTextBox").value = "";
            document.getElementById("MessageTextBox").focus();
            event.preventDefault();
        }
    });

}