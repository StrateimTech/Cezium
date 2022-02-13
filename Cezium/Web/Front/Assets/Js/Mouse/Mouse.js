function handleMouseState(e) {
    const content = document.getElementById("content");
    const InvertMouseX = document.getElementById("InvertMouseX");
    const InvertMouseY = document.getElementById("InvertMouseY");
    const InvertMouseWheel = document.getElementById("InvertMouseWheel");
    
    if(content.style.opacity === "0.4") {
        content.style.opacity = "1";
        InvertMouseX.disabled = false;
        InvertMouseY.disabled = false;
        InvertMouseWheel.disabled = false;
    } else {
        content.style.opacity = "0.4";
        InvertMouseX.disabled = true;
        InvertMouseY.disabled = true;
        InvertMouseWheel.disabled = true;
    }

    var arr = { state: false };
    $.ajax('https://127.0.0.1:301/api/settings/State', {
        data: JSON.stringify(arr),
        contentType: 'application/json',
        type: 'POST'
    });
}

function handleDebugState(e) {
    const DebugState = document.getElementById("DebugState");
}

function handleInvertMouseX(e) {
    const InvertMouseX = document.getElementById("InvertMouseX");
}

function handleInvertMouseY(e) {
    const InvertMouseY = document.getElementById("InvertMouseY");
}

function handleInvertMouseWheel(e) {
    const InvertMouseWheel = document.getElementById("InvertMouseWheel");
}