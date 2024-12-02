function numberPressed(event) {
    var charCode = (event.which) ? event.which : event.keyCode;
    var input = event.target.value.replace(/\D/g, '').substring(0, 12);

    if (input.length < 12) {
        if ((charCode >= 48 && charCode <= 57) || (charCode >= 96 && charCode <= 105) ||
            charCode == 8 || charCode == 9 || charCode == 46 || charCode == 37 || charCode == 39)
            return true;
    } else {
        if (charCode == 8 || charCode == 9 || charCode == 46 || charCode == 37 || charCode == 39)
            return true;
    }
        
    return false;
}

function enforceFormat(event) {
    if (!numberPressed(event)) {
        event.preventDefault();
    }
}

function formatToPhone(event) {
    var input = event.target.value.replace(/\D/g, '').substring(0, 12);
    var countryCode = "+38 (0";
    var operatorCode = input.substring(3, 5);
    var phoneDigits = input.substring(5, 12);
    var size = input.length;
    var startPosition = event.target.selectionStart;

    if (size < 4) {
        input = countryCode + "  )";
        startPosition = 6;
    } else if (size < 6) {
        if (operatorCode.length == 1) {
            operatorCode = operatorCode + " ";
            startPosition = 7;
        } else {
            startPosition = 10;
        }
        input = countryCode + operatorCode + ") ";
    } else if (size < 9) {
        input = countryCode + operatorCode + ") " + phoneDigits;
    } else if (size < 11) {
        input = countryCode + operatorCode + ") " + phoneDigits.substring(0, 3) + "-" + phoneDigits.substring(3);
        if (phoneDigits > 3)
            startPosition++;
    } else {
        input = countryCode + operatorCode + ") " + phoneDigits.substring(0, 3) + "-" + phoneDigits.substring(3, 5) + "-" + phoneDigits.substring(5);
        if (phoneDigits > 5)
            startPosition++;
    }

    event.target.value = input;
    event.target.setSelectionRange(startPosition, startPosition);
    //if (size < 6) {
    //    event.target.setSelectionRange(middle.length + 7, middle.length + 6);
    //}
}

function PhoneValidation(input) {
    if (input.length != 12)
        return false;
    return true;
}
