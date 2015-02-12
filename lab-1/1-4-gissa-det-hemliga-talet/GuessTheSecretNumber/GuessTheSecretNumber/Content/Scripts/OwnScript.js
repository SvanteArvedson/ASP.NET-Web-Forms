(function () {
    var textbox = document.getElementById("MakeGuessTextBox");
    
    textbox.addEventListener("focus", function () {
        this.select();
    }, false)

    if (textbox.disabled) {
        document.getElementById("NewGameButton").focus();
    } else {
        textbox.focus();
    }
})();