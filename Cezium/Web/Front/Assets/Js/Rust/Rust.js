
function handleRustState(e) {
    const content = document.getElementById("content");

    if (content.style.opacity === "0.4") {
        content.style.opacity = "1";
    } else {
        content.style.opacity = "0.4";
    }

    // var person = {"FirstName":"Andrew","LastName":"Lock","Age":"31"};
    // $.ajax({
    //     type: "POST",
    //     url: "/Mouse?handler=FindUser",
    //     // data: {id: id},
    //     contentType: "application/json; charset=utf-8",
    //     // dataType: "json",
    //     // AntiforgeryToken is required by RazorPages
    //     headers: {
    //         RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
    //     },
    //     data: JSON.stringify(person)
    // }).done(function () {
    //     alert("success");
    // }).fail(function () {
    //     alert("error");
    // });

    // $.ajax({
    //     type: 'GET',
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