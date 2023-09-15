const chatContainer = document.getElementById("chat");
const userInput = document.getElementById("user-input");

const messages = [
    "Hola, ¡bienvenido a nuestro chatbot de decisiones!",
    "Por favor, elige una opción:\n1. OFERTA ECONÓMICA\n2. PRESENTACIÓN DE DOCUMENTACIÓN\n3. FIRMA DIGITAL",
    "¡Has seleccionado OFERTA ECONÓMICA!",
    "¡Has seleccionado PRESENTACIÓN DE DOCUMENTACIÓN!",
    "¡Has seleccionado FIRMA DIGITAL!"
];

let step = 0;

function displayMessage(message) {
    const messageElement = document.createElement("div");
    messageElement.textContent = message;
    chatContainer.appendChild(messageElement);
}

function processUserInput() {
    const userText = userInput.value;
    displayMessage("Usuario: " + userText);

    if (step === 0) {
        displayMessage(messages[0]);
        step = 1;
    } else if (step === 1) {
        displayMessage(messages[1]);
        step = 2;
    } else if (step === 2 && userText === "1") {
        displayMessage(messages[2]);
        step = 0;
    } else if (step === 2 && userText === "2") {
        displayMessage(messages[3]);
        step = 0;
    } else if (step === 2 && userText === "3") {
        displayMessage(messages[4]);
        step = 0;
    } else {
        displayMessage("Opción no válida, por favor selecciona una opción válida.");
        step = 1;
    }

    userInput.value = "";
    userInput.focus();
}

userInput.addEventListener("keyup", function (event) {
    if (event.key === "Enter") {
        processUserInput();
    }
});

displayMessage(messages[0]);