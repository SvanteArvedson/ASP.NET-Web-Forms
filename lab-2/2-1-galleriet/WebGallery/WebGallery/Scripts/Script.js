(function () {
    "use strict";
    document.getElementById("ThumbnailsList").addEventListener("click", function (e) {
        var hit = e.target,
            imageName = "";

        if (hit.tagName.toLowerCase() === "img") {
            e.preventDefault();

            // Changing the URL
            imageName = ((hit.parentNode.href.split("?"))[1].split("="))[1];
            window.history.pushState(null, "", "?image=" + imageName);

            // Changing the src-attribute for BigImage and classnames for thumbnails
            document.getElementById("BigImage").src = "Content/Images/" + imageName;
            (document.getElementsByClassName("Selected"))[0].removeAttribute("class");
            hit.className = "Selected";
        }
    }, false);

    document.getElementById("RightMessageDiv").addEventListener("click", function (e) {
        var hit = e.target,
            messageDiv = null;

        // If click was on the close icon, FileMessageDiv is removed.
        if (hit.id === "RightMessageCloseAnchor") {
            e.preventDefault();
            messageDiv = document.getElementById("RightMessageDiv");
            messageDiv.parentElement.removeChild(messageDiv);
        }
        else if (hit.id === "RightMessageCloseIcon") {
            e.preventDefault();
            messageDiv = document.getElementById("RightMessageDiv");
            messageDiv.parentElement.removeChild(messageDiv);
        }
    }, false);
}());