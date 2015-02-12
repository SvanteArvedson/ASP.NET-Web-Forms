(function () {
    "use strict";

    // Focus on TextBox.
    var textBox = document.getElementById("InputSubtotal");
    textBox.addEventListener("focus", function (e) {
        e.target.select();
    }, false);
    textBox.focus();
})();