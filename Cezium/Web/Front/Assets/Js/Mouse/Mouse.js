function handleMouseState(e) {
    const content = document.getElementById("content");
    const InvertMouseX = document.getElementById("InvertMouseX");
    const InvertMouseY = document.getElementById("InvertMouseY");
    const InvertMouseWheel = document.getElementById("InvertMouseWheel");

    if (content.style.opacity === "0.4") {
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

    var person = {"FirstName":"Andrew","LastName":"Lock","Age":"31"};
    $.ajax({
        type: "POST",
        url: "/Mouse?handler=FindUser",
        // data: {id: id},
        contentType: "application/json; charset=utf-8",
        // dataType: "json",
        // AntiforgeryToken is required by RazorPages
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: JSON.stringify(person)
    }).done(function () {
        alert("success");
    }).fail(function () {
        alert("error");
    });


    // $.ajax({
    //     type: 'POST',
    //     // Note the difference in url (this works if you're actually in Index page)
    //     url: '/mouse?handler=FindUser',
    //     headers: {
    //         RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
    //     },
    //     success: function (data) {
    //         alert(data);
    //     },
    //     error: function (error) {
    //         alert("Error: " + error);
    //     }
    // })
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