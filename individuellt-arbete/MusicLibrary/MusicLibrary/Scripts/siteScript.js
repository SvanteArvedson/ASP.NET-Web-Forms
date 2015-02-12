// Checks if input in Text-field Year is valid.
function CheckYear(source, arguments) {

    if (arguments.Value || arguments.Value === "" || IsNaN(arguments.Value)) {
        arguments.IsValid = true;
        return;
    }

    if (arguments.Value <= (new Date().getFullYear())) {
        arguments.IsValid = true;
    } else {
        arguments.IsValid = false;
    }

}