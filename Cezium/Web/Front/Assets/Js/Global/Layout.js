function openNav() {
    const sideNavbar = document.getElementById("side-navbar");
    const main = document.getElementById("main");

    const sideNavExpandedContent = document.getElementById("side-nav-expanded-content");

    const hamburgerNavButtonText = document.getElementById("hamburger-nav-button-text");

    const sideNavCondensed = document.getElementById("side-nav-condensed");

    if(sideNavbar.style.width === "250px") {
        sideNavbar.style.width = "48px";
        main.style.marginLeft = "48px";

        sideNavExpandedContent.style.visibility = "hidden";
        sideNavExpandedContent.style.opacity = "0";

        hamburgerNavButtonText.style.visibility = "hidden";
        hamburgerNavButtonText.style.opacity = "0";

        sideNavCondensed.style.display = "block";
    } else {
        sideNavbar.style.width = "250px";
        main.style.marginLeft = "250px";

        sideNavExpandedContent.style.height = "100%";
        sideNavExpandedContent.style.visibility = "visible";
        sideNavExpandedContent.style.opacity = "1";

        hamburgerNavButtonText.style.visibility = "visible";
        hamburgerNavButtonText.style.opacity = "1";

        sideNavCondensed.style.display = "none";
    }
}