function openNav() {
    if(document.getElementById("side-navbar").style.width === "250px") {
        document.getElementById("side-navbar").style.width = "48px";
        document.getElementById("main").style.marginLeft = "48px";

        document.getElementById("side-nav-expanded-content").style.visibility = "hidden";
        document.getElementById("side-nav-expanded-content").style.opacity = "0";

        document.getElementById("hamburger-nav-button-text").style.visibility = "hidden";
        document.getElementById("hamburger-nav-button-text").style.opacity = "0";

        document.getElementById("side-nav-condensed").style.display = "block";
    } else {
        document.getElementById("side-navbar").style.width = "250px";
        document.getElementById("main").style.marginLeft = "250px";

        document.getElementById("side-nav-expanded-content").style.height = "100%";
        document.getElementById("side-nav-expanded-content").style.visibility = "visible";
        document.getElementById("side-nav-expanded-content").style.opacity = "1";

        document.getElementById("hamburger-nav-button-text").style.visibility = "visible";
        document.getElementById("hamburger-nav-button-text").style.opacity = "1";

        document.getElementById("side-nav-condensed").style.display = "none";
    }
}