
function handleRustState(rustState) {
    const content = document.getElementById("content");

    const debugContent = document.getElementById("DebugContent");
    const debugState = document.getElementById("DebugState");
    
    const fovScale = document.getElementById("FovScale");
    const sensitivityScale = document.getElementById("SensitivityScale");
    const smoothnessScale = document.getElementById("SmoothnessScale");

    const horizontalValue = document.getElementById("HorizontalScale");
    const verticalValue = document.getElementById("VerticalScale");

    const infiniteAmmo = document.getElementById("InfiniteAmmo");
    const tapping = document.getElementById("Tapping");

    const randomization = document.getElementById("Randomization");

    const randomizationContent = document.getElementById("RandomizationContent");
    const reverseRandomizationContent = document.getElementById("ReverseRandomizationContent");

    const reverseRandomization = document.getElementById("ReverseRandomization");

    const minRandomizationXScale = document.getElementById("MinRandomizationXScale");
    const maxRandomizationXScale = document.getElementById("MaxRandomizationXScale");

    const minRandomizationYScale = document.getElementById("MinRandomizationYScale");
    const maxRandomizationYScale = document.getElementById("MaxRandomizationYScale");
    
    if(rustState.checked) {
        content.style.opacity = "1";

        debugContent.style.opacity = "1";
        debugState.disabled = false;
        
        fovScale.disabled = false;
        sensitivityScale.disabled = false;
        smoothnessScale.disabled = false;

        horizontalValue.disabled = false;
        verticalValue.disabled = false;

        infiniteAmmo.disabled = false;
        tapping.disabled = false;

        randomization.disabled = false;

        if(randomization.checked) {
            reverseRandomization.disabled = false;

            minRandomizationXScale.disabled = false;
            maxRandomizationXScale.disabled = false;

            minRandomizationYScale.disabled = false;
            maxRandomizationYScale.disabled = false;
        }
    } else {
        content.style.opacity = "0.4";

        debugContent.style.opacity = "0.4";
        debugState.disabled = true;

        fovScale.disabled = true;
        sensitivityScale.disabled = true;
        smoothnessScale.disabled = true;

        horizontalValue.disabled = true;
        verticalValue.disabled = true;

        infiniteAmmo.disabled = true;
        tapping.disabled = true;

        randomization.disabled = true;
        
        if(randomization.checked) {
            reverseRandomization.disabled = true;

            minRandomizationXScale.disabled = true;
            maxRandomizationXScale.disabled = true;

            minRandomizationYScale.disabled = true;
            maxRandomizationYScale.disabled = true;
        }
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

function handleDebugState(debugState) {
}

function handleInfiniteAmmo(infiniteAmmo) {
}

function handleTapping(tapping) {
}

function handleRandomization(randomization) {
    const randomizationContent = document.getElementById("RandomizationContent");
    const reverseRandomizationContent = document.getElementById("ReverseRandomizationContent");

    const reverseRandomization = document.getElementById("ReverseRandomization");

    const minRandomizationXScale = document.getElementById("MinRandomizationXScale");
    const maxRandomizationXScale = document.getElementById("MaxRandomizationXScale");

    const minRandomizationYScale = document.getElementById("MinRandomizationYScale");
    const maxRandomizationYScale = document.getElementById("MaxRandomizationYScale");


    if(randomization.checked) {
        randomizationContent.style.opacity = "1";
        reverseRandomizationContent.style.opacity = "1";
        
        reverseRandomization.disabled = false;
        
        minRandomizationXScale.disabled = false;
        maxRandomizationXScale.disabled = false;

        minRandomizationYScale.disabled = false;
        maxRandomizationYScale.disabled = false;
    } else {
        randomizationContent.style.opacity = "0.4";
        reverseRandomizationContent.style.opacity = "0.4";

        reverseRandomization.disabled = true;

        minRandomizationXScale.disabled = true;
        maxRandomizationXScale.disabled = true;

        minRandomizationYScale.disabled = true;
        maxRandomizationYScale.disabled = true;
    }
}

function handleReverseRandomization(reverseRandomization) {
}