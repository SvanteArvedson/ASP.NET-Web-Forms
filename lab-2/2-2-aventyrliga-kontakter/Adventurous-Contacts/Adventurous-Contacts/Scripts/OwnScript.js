(function () {
    // Event handler on Body takes care of all behaviours.
    document.body.addEventListener("click", function (e) {
        var hit = e.target;

        if (hit.id === "CloseIcon" || hit.id === "CloseIconHyperLink") {
            e.preventDefault();
            var rightBox = document.getElementById("RightBox");
            if (hit.id === "CloseIcon") {
                hit.parentNode.parentNode.parentNode.removeChild(rightBox);
            } else {
                hit.parentNode.parentNode.removeChild(rightBox);
            }
        } else if (hit.className === "Button DeleteButton") {
            if (!confirm("Vill du verkligen radera kontakten?")) {
                e.preventDefault();
            }
        }

    }, false);
}());