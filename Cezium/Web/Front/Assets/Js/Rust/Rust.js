﻿function handleLoad() {
    handleDataUpdate();
}

function handleDataUpdate() {
    $.ajax({
        type: 'GET',
        url: '/Rust?handler=State',
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            const rustState = document.getElementById("RustState");
            rustState.checked = data;
            handleRustState(rustState, true);
        }
    });

    $.ajax({
        type: 'GET',
        url: '/Rust?handler=Debug',
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            const debugState = document.getElementById("DebugState");
            debugState.checked = data;
        }
    });

    $.ajax({
        type: 'GET',
        url: '/Rust?handler=Fov',
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            const fovScale = document.getElementById("FovScale");
            const fovValue = document.getElementById("FovValue");
            if(data !== fovScale.value && fovValue.innerHTML !== data) {
                fovScale.value = data;
                fovValue.innerHTML = data;
            }
        }
    });

    $.ajax({
        type: 'GET',
        url: '/Rust?handler=Sensitivity',
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            const sensitivityScale = document.getElementById("SensitivityScale");
            const sensitivityValue = document.getElementById("SensitivityValue");
            if(data !== sensitivityScale.value && sensitivityValue.innerHTML !== data) {
                sensitivityScale.value = data;
                sensitivityValue.innerHTML = data;
            }
        }
    });

    $.ajax({
        type: 'GET',
        url: '/Rust?handler=Smoothness',
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            const smoothnessScale = document.getElementById("SmoothnessScale");
            const smoothnessValue = document.getElementById("SmoothnessValue");
            if(data !== smoothnessScale.value && smoothnessValue.innerHTML !== data) {
                smoothnessScale.value = data;
                smoothnessValue.innerHTML = data;
            }
        }
    });

    $.ajax({
        type: 'GET',
        url: '/Rust?handler=Granularization',
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            const granularizationScale = document.getElementById("GranularizationScale");
            const granularizationValue = document.getElementById("GranularizationValue");
            if(data !== granularizationScale.value && granularizationValue.innerHTML !== data) {
                granularizationScale.value = data;
                granularizationValue.innerHTML = data;
            }

            var gameSens = calculateGranularizationGameSens();
            const granularizationSensitivity = document.getElementById("GranularizationSensitivity");
            granularizationSensitivity.innerHTML = gameSens;
        }
    });

    $.ajax({
        type: 'GET',
        url: '/Rust?handler=Horizontal',
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            const horizontalScale = document.getElementById("HorizontalScale");
            const horizontalValue = document.getElementById("HorizontalValue");

            if(data !== horizontalScale.value && horizontalValue.innerHTML  !== data) {
                horizontalScale.value = data;
                horizontalValue.innerHTML = data;
            }
        }
    });

    $.ajax({
        type: 'GET',
        url: '/Rust?handler=Vertical',
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            const verticalScale = document.getElementById("VerticalScale");
            const verticalValue = document.getElementById("VerticalValue");

            if(data !== verticalScale.value && verticalValue.innerHTML !== data) {
                verticalScale.value = data;
                verticalValue.innerHTML = data;
            }
        }
    });

    $.ajax({
        type: 'GET',
        url: '/Rust?handler=InfiniteAmmo',
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            const infiniteAmmo = document.getElementById("InfiniteAmmo");
            infiniteAmmo.checked = data;
        }
    });

    $.ajax({
        type: 'GET',
        url: '/Rust?handler=Tapping',
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            const tapping = document.getElementById("Tapping");
            tapping.checked = data;
        }
    });

    $.ajax({
        type: 'GET',
        url: '/Rust?handler=GlobalCompensation',
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            const GlobalCompensation = document.getElementById("GlobalCompensation");
            GlobalCompensation.checked = data;
        }
    });

    $.ajax({
        type: 'GET',
        url: '/Rust?handler=LocalCompensation',
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            const LocalCompensation = document.getElementById("LocalCompensation");
            LocalCompensation.checked = data;
            handleGlobalCompensation(LocalCompensation, true);
        }
    });

    $.ajax({
        type: 'GET',
        url: '/Rust?handler=Randomization',
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            const randomization = document.getElementById("Randomization");
            randomization.checked = data;
            handleRandomization(randomization, true);
        }
    });

    $.ajax({
        type: 'GET',
        url: '/Rust?handler=ReverseRandomization',
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            const reverseRandomization = document.getElementById("ReverseRandomization");
            reverseRandomization.checked = data;
        }
    });

    $.ajax({
        type: 'GET',
        url: '/Rust?handler=StaticRandomization',
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            const staticRandomization = document.getElementById("StaticRandomization");
            staticRandomization.checked = data;
        }
    });

    $.ajax({
        type: 'GET',
        url: '/Rust?handler=RandomizationX',
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            let json = JSON.parse(data);

            const minRandomizationXScale = document.getElementById("MinRandomizationXScale");
            const maxRandomizationXScale = document.getElementById("MaxRandomizationXScale");

            minRandomizationXScale.value = json.Item1;
            maxRandomizationXScale.value = json.Item2;

            const minRandomizationXValue = document.getElementById("MinRandomizationXValue");
            minRandomizationXValue.innerHTML = minRandomizationXScale.value;

            const maxRandomizationXValue = document.getElementById("MaxRandomizationXValue");
            maxRandomizationXValue.innerHTML = maxRandomizationXScale.value;
        }
    });

    $.ajax({
        type: 'GET',
        url: '/Rust?handler=RandomizationY',
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            let json = JSON.parse(data);
            
            const minRandomizationYScale = document.getElementById("MinRandomizationYScale");
            const maxRandomizationYScale = document.getElementById("MaxRandomizationYScale");

            minRandomizationYScale.value = json.Item1;
            maxRandomizationYScale.value = json.Item2;

            const minRandomizationYValue = document.getElementById("MinRandomizationYValue");
            minRandomizationYValue.innerHTML = minRandomizationYScale.value;

            const maxRandomizationXValue = document.getElementById("MaxRandomizationYValue");
            maxRandomizationXValue.innerHTML = maxRandomizationYScale.value;
        }
    });

    $.ajax({
        type: 'GET',
        url: '/Rust?handler=RandomizationTiming',
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            let json = JSON.parse(data);

            const minRandomizationTimingScale = document.getElementById("MinRandomizationTimingScale");
            const maxRandomizationTimingScale = document.getElementById("MaxRandomizationTimingScale");

            minRandomizationTimingScale.value = json.Item1;
            maxRandomizationTimingScale.value = json.Item2;

            const minRandomizationTimingValue = document.getElementById("MinRandomizationTimingValue");
            minRandomizationTimingValue.innerHTML = minRandomizationTimingScale.value + "%";

            const maxRandomizationTimingValue = document.getElementById("MaxRandomizationTimingValue");
            maxRandomizationTimingValue.innerHTML = maxRandomizationTimingScale.value + "%";
        }
    });

    $.ajax({
        type: 'GET',
        url: '/Rust?handler=Gun',
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            const dataElement = document.getElementById(data);
            dataElement.checked = true;
        }
    });

    $.ajax({
        type: 'GET',
        url: '/Rust?handler=Scope',
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            const dataElement = document.getElementById(data);
            dataElement.checked = true;
        }
    });

    $.ajax({
        type: 'GET',
        url: '/Rust?handler=Attachment',
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            const dataElement = document.getElementById(data);
            dataElement.checked = true;
        }
    });
}


function handleRustState(rustState, ignore) {
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
    const GlobalCompensation = document.getElementById("GlobalCompensation");
    const LocalCompensation = document.getElementById("LocalCompensation");

    const randomization = document.getElementById("Randomization");
    const reverseRandomization = document.getElementById("ReverseRandomization");
    const staticRandomization = document.getElementById("StaticRandomization");

    const minRandomizationXScale = document.getElementById("MinRandomizationXScale");
    const maxRandomizationXScale = document.getElementById("MaxRandomizationXScale");

    const minRandomizationYScale = document.getElementById("MinRandomizationYScale");
    const maxRandomizationYScale = document.getElementById("MaxRandomizationYScale");

    const minRandomizationTimingScale = document.getElementById("MinRandomizationTimingScale");
    const maxRandomizationTimingScale = document.getElementById("MaxRandomizationTimingScale");
    
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
        GlobalCompensation.disabled = false;
        LocalCompensation.disabled = false;

        randomization.disabled = false;

        if(randomization.checked) {
            reverseRandomization.disabled = false;
            staticRandomization.disabled = false;

            minRandomizationXScale.disabled = false;
            maxRandomizationXScale.disabled = false;

            minRandomizationYScale.disabled = false;
            maxRandomizationYScale.disabled = false;

            minRandomizationTimingScale.disabled = false;
            maxRandomizationTimingScale.disabled = false;
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
        GlobalCompensation.disabled = true;
        LocalCompensation.disabled = true;

        randomization.disabled = true;
        
        if(randomization.checked) {
            reverseRandomization.disabled = true;
            staticRandomization.disabled = true;

            minRandomizationXScale.disabled = true;
            maxRandomizationXScale.disabled = true;

            minRandomizationYScale.disabled = true;
            maxRandomizationYScale.disabled = true;

            minRandomizationTimingScale.disabled = true;
            maxRandomizationTimingScale.disabled = true;
        }
    }

    if(ignore === false) {
        const data = {"Value": rustState.checked};
        $.ajax({
            type: "POST",
            url: "/Rust?handler=State",
            contentType: "application/json; charset=utf-8",
            headers: {
                RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            data: JSON.stringify(data)
        });
    }
}

function handleDebugState(debugState) {
    const data = {"Value": debugState.checked};
    $.ajax({
        type: "POST",
        url: "/Rust?handler=Debug",
        contentType: "application/json; charset=utf-8",
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: JSON.stringify(data)
    });
}

function handleFovTag(fov) {
    const fovValue = document.getElementById("FovValue");
    fovValue.innerHTML = fov.value;
}

function handleFov(fov) {
    const data = {"Value": fov.value};
    $.ajax({
        type: "POST",
        url: "/Rust?handler=Fov",
        contentType: "application/json; charset=utf-8",
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: JSON.stringify(data)
    });
}

function handleSensitivityTag(sensitivity) {
    const sensitivityValue = document.getElementById("SensitivityValue");
    sensitivityValue.innerHTML = sensitivity.value;
}

function handleSensitivity(sensitivity) {
    const data = {"Value": sensitivity.value};
    $.ajax({
        type: "POST",
        url: "/Rust?handler=Sensitivity",
        contentType: "application/json; charset=utf-8",
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: JSON.stringify(data)
    });
}

function handleSmoothnessTag(smoothness) {
    const smoothnessValue = document.getElementById("SmoothnessValue");
    smoothnessValue.innerHTML = smoothness.value;
}

function handleSmoothness(smoothness) {
    const data = {"Value": smoothness.value};
    $.ajax({
        type: "POST",
        url: "/Rust?handler=Smoothness",
        contentType: "application/json; charset=utf-8",
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: JSON.stringify(data)
    });
}

function handleGranularizationTag(granularization) {
    const granularizationValue = document.getElementById("GranularizationValue");
    granularizationValue.innerHTML = granularization.value;
}

function handleGranularization(granularization) {
    var gameSens = calculateGranularizationGameSens();
    const granularizationSensitivity = document.getElementById("GranularizationSensitivity");
    granularizationSensitivity.innerHTML = gameSens;
    
    const data = {"Value": granularization.value};
    $.ajax({
        type: "POST",
        url: "/Rust?handler=Granularization",
        contentType: "application/json; charset=utf-8",
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: JSON.stringify(data)
    });
}

function calculateGranularizationGameSens() {
    const granularizationValue = document.getElementById("GranularizationScale").value;
    const sensitivityValue = document.getElementById("SensitivityScale").value;

    return sensitivityValue/granularizationValue;
}

function handleHorizontalTag(horizontal) {
    const horizontalValue = document.getElementById("HorizontalValue");
    horizontalValue.innerHTML = horizontal.value;
}

function handleHorizontal(horizontal) {
    const data = {"Value": horizontal.value};
    $.ajax({
        type: "POST",
        url: "/Rust?handler=Horizontal",
        contentType: "application/json; charset=utf-8",
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: JSON.stringify(data)
    });
}

function handleVerticalTag(vertical) {
    const verticalValue = document.getElementById("VerticalValue");
    verticalValue.innerHTML = vertical.value;
}

function handleVertical(vertical) {
    const data = {"Value": vertical.value};
    $.ajax({
        type: "POST",
        url: "/Rust?handler=Vertical",
        contentType: "application/json; charset=utf-8",
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: JSON.stringify(data)
    });
}
    
function handleInfiniteAmmo(infiniteAmmo) {
    const data = {"Value": infiniteAmmo.checked};
    $.ajax({
        type: "POST",
        url: "/Rust?handler=InfiniteAmmo",
        contentType: "application/json; charset=utf-8",
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: JSON.stringify(data)
    });
}

function handleTapping(tapping) {
    const data = {"Value": tapping.checked};
    $.ajax({
        type: "POST",
        url: "/Rust?handler=Tapping",
        contentType: "application/json; charset=utf-8",
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: JSON.stringify(data)
    });
}

function handleGlobalCompensation(GlobalCompensation, ignore) {
    const LocalCompensation = document.getElementById("LocalCompensation");
    const LocalCompensationContent = document.getElementById("LocalCompensationContent");
    if(GlobalCompensation.checked) {
        LocalCompensationContent.style.opacity = "1";
        LocalCompensation.disabled = false;
    } else {
        LocalCompensationContent.style.opacity = "0.4";
        LocalCompensation.disabled = true;
    }
    if(ignore === false) {
        const data = {"Value": GlobalCompensation.checked};
        $.ajax({
            type: "POST",
            url: "/Rust?handler=GlobalCompensation",
            contentType: "application/json; charset=utf-8",
            headers: {
                RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            data: JSON.stringify(data)
        });
    }
}

function handleLocalCompensation(LocalCompensation) {
    const data = {"Value": LocalCompensation.checked};
    $.ajax({
        type: "POST",
        url: "/Rust?handler=LocalCompensation",
        contentType: "application/json; charset=utf-8",
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: JSON.stringify(data)
    });
}

function handleRandomization(randomization, ignore) {
    const randomizationContent = document.getElementById("RandomizationContent");
    const reverseRandomizationContent = document.getElementById("ReverseRandomizationContent");
    const reverseRandomization = document.getElementById("ReverseRandomization");

    const staticRandomizationContent = document.getElementById("StaticRandomizationContent");
    const staticRandomization = document.getElementById("StaticRandomization");

    const minRandomizationXScale = document.getElementById("MinRandomizationXScale");
    const maxRandomizationXScale = document.getElementById("MaxRandomizationXScale");

    const minRandomizationYScale = document.getElementById("MinRandomizationYScale");
    const maxRandomizationYScale = document.getElementById("MaxRandomizationYScale");

    const minRandomizationTimingScale = document.getElementById("MinRandomizationTimingScale");
    const maxRandomizationTimingScale = document.getElementById("MaxRandomizationTimingScale");

    if(randomization.checked) {
        randomizationContent.style.opacity = "1";
        reverseRandomizationContent.style.opacity = "1";
        staticRandomizationContent.style.opacity = "1";
        
        reverseRandomization.disabled = false;
        staticRandomization.disabled = false;
        
        minRandomizationXScale.disabled = false;
        maxRandomizationXScale.disabled = false;

        minRandomizationYScale.disabled = false;
        maxRandomizationYScale.disabled = false;

        minRandomizationTimingScale.disabled = false;
        maxRandomizationTimingScale.disabled = false;
    } else {
        randomizationContent.style.opacity = "0.4";
        reverseRandomizationContent.style.opacity = "0.4";
        staticRandomizationContent.style.opacity = "0.4";

        reverseRandomization.disabled = true;
        staticRandomization.disabled = true;

        minRandomizationXScale.disabled = true;
        maxRandomizationXScale.disabled = true;

        minRandomizationYScale.disabled = true;
        maxRandomizationYScale.disabled = true;

        minRandomizationTimingScale.disabled = true;
        maxRandomizationTimingScale.disabled = true;
    }
    
    if(ignore === false) {
        const data = {"Value": randomization.checked};
        $.ajax({
            type: "POST",
            url: "/Rust?handler=Randomization",
            contentType: "application/json; charset=utf-8",
            headers: {
                RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            data: JSON.stringify(data)
        });
    }
}

function handleReverseRandomization(reverseRandomization) {
    const data = {"Value": reverseRandomization.checked};
    $.ajax({
        type: "POST",
        url: "/Rust?handler=ReverseRandomization",
        contentType: "application/json; charset=utf-8",
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: JSON.stringify(data)
    });
}

function handleStaticRandomization(staticRandomization) {
    const data = {"Value": staticRandomization.checked};
    $.ajax({
        type: "POST",
        url: "/Rust?handler=StaticRandomization",
        contentType: "application/json; charset=utf-8",
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: JSON.stringify(data)
    });
}

function handleRandomizationXTag() {
    const minRandomizationXScale = document.getElementById("MinRandomizationXScale");
    const maxRandomizationXScale = document.getElementById("MaxRandomizationXScale");

    if(+minRandomizationXScale.value > +maxRandomizationXScale.value) {
        minRandomizationXScale.value = +maxRandomizationXScale.value;
    }
    
    const minRandomizationXValue = document.getElementById("MinRandomizationXValue");
    minRandomizationXValue.innerHTML = minRandomizationXScale.value;

    const maxRandomizationXValue = document.getElementById("MaxRandomizationXValue");
    maxRandomizationXValue.innerHTML = maxRandomizationXScale.value;
}

function handleRandomizationX() {
    const minRandomizationXScale = document.getElementById("MinRandomizationXScale");
    const maxRandomizationXScale = document.getElementById("MaxRandomizationXScale");
    
    if(+minRandomizationXScale.value > +maxRandomizationXScale.value) {
        minRandomizationXScale.value = +maxRandomizationXScale.value;
    }
    
    const data = {"Item1": minRandomizationXScale.value, "Item2": maxRandomizationXScale.value};
    $.ajax({
        type: "POST",
        url: "/Rust?handler=RandomizationX",
        contentType: "application/json; charset=utf-8",
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: JSON.stringify(data)
    });
}

function handleRandomizationYTag() {
    const minRandomizationYScale = document.getElementById("MinRandomizationYScale");
    const maxRandomizationYScale = document.getElementById("MaxRandomizationYScale");

    if(+minRandomizationYScale.value > +maxRandomizationYScale.value) {
        minRandomizationYScale.value = +maxRandomizationYScale.value;
    }

    const minRandomizationYValue = document.getElementById("MinRandomizationYValue");
    minRandomizationYValue.innerHTML = minRandomizationYScale.value;

    const maxRandomizationXValue = document.getElementById("MaxRandomizationYValue");
    maxRandomizationXValue.innerHTML = maxRandomizationYScale.value;
}

function handleRandomizationY() {
    const minRandomizationYScale = document.getElementById("MinRandomizationYScale");
    const maxRandomizationYScale = document.getElementById("MaxRandomizationYScale");

    if(+minRandomizationYScale.value > +maxRandomizationYScale.value) {
        minRandomizationYScale.value = +maxRandomizationYScale.value;
    }
    const data = {"Item1": minRandomizationYScale.value, "Item2": maxRandomizationYScale.value};
    $.ajax({
        type: "POST",
        url: "/Rust?handler=RandomizationY",
        contentType: "application/json; charset=utf-8",
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: JSON.stringify(data)
    });
}

function handleRandomizationTimingTag() {
    const minRandomizationTimingScale = document.getElementById("MinRandomizationTimingScale");
    const maxRandomizationTimingScale = document.getElementById("MaxRandomizationTimingScale");

    if(+minRandomizationTimingScale.value > +maxRandomizationTimingScale.value) {
        minRandomizationTimingScale.value = +maxRandomizationTimingScale.value;
    }

    const minRandomizationTimingValue = document.getElementById("MinRandomizationTimingValue");
    minRandomizationTimingValue.innerHTML = minRandomizationTimingScale.value + "%";

    const maxRandomizationTimingValue = document.getElementById("MaxRandomizationTimingValue");
    maxRandomizationTimingValue.innerHTML = maxRandomizationTimingScale.value + "%";
}

function handleRandomizationTiming() {
    const minRandomizationTimingScale = document.getElementById("MinRandomizationTimingScale");
    const maxRandomizationTimingScale = document.getElementById("MaxRandomizationTimingScale");

    if(+minRandomizationTimingScale.value > +maxRandomizationTimingScale.value) {
        minRandomizationTimingScale.value = +maxRandomizationTimingScale.value;
    }
    const data = {"Item1": minRandomizationTimingScale.value, "Item2": maxRandomizationTimingScale.value};
    $.ajax({
        type: "POST",
        url: "/Rust?handler=RandomizationTiming",
        contentType: "application/json; charset=utf-8",
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: JSON.stringify(data)
    });
}

function handleWeaponChange(weapon) {
    const data = {"Value": weapon};
    $.ajax({
        type: "POST",
        url: "/Rust?handler=Gun",
        contentType: "application/json; charset=utf-8",
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: JSON.stringify(data)
    });
}

function handleScopeChange(scope) {
    const data = {"Value": scope};
    $.ajax({
        type: "POST",
        url: "/Rust?handler=Scope",
        contentType: "application/json; charset=utf-8",
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: JSON.stringify(data)
    });
}

function handleAttachmentChange(attachment) {
    const data = {"Value": attachment};
    $.ajax({
        type: "POST",
        url: "/Rust?handler=Attachment",
        contentType: "application/json; charset=utf-8",
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: JSON.stringify(data)
    });
}