(function() {
    var printButton = document.getElementById("PrintImageButton");

    printButton.addEventListener("click", function (e) {
        e.preventDefault();
        window.print();
    }, false);


}())