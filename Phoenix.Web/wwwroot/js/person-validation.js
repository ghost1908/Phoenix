function ValidateProfile() {
    var status = true;

    if ($('#profileLastName').val().trim().length == 0) {
        $('#profileLastName').addClass("invalid");
        status = false;
    } else {
        $('#profileLastName').removeClass("invalid");
    }
        

    if ($('#profileFirstName').val().trim().length == 0) {
        $('#profileFirstName').addClass("invalid");
        status = false;
    } else {
        $('#profileFirstName').removeClass("invalid");
    }

    if ($('#AddressRegistration_ID').val().trim().length == 0) {
        $('#profileAddressRegistration').addClass("invalid");
        status = false;
    } else {
        $('#profileAddressRegistration').removeClass("invalid");
    }
        
    if (("+" + $('#profilePhone').val().replace(/\D/g, '').substring(0, 12)).length > 4 &&
        ("+" + $('#profilePhone').val().replace(/\D/g, '').substring(0, 12)).length < 13) {
        $('#profilePhone').addClass("invalid");
        status = false;
    } else {
        $('#profilePhone').removeClass("invalid");
    }

    if ($('#profileIsEmployee').is(':checked') == true) {
        var employeePositions = $('#tableEmployee tbody tr[data-id]');
        var container = $('#alert-container');
        if (employeePositions == undefined || employeePositions.length == 0) {
            //container.html(
            //    `<div class="alert alert-danger alert-dismissible fade show" role="alert">
            //        <p>Необхідно додати посаду співробітника.</p>
            //        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
            //            <span aria-hidden="true">&times;</span>
            //        </button>
            //    </div >`);
            container.bsAlert("Необхідно додати посаду співробітника.");
            status = false;
        } else {
            status = true;
        }
    }

    return status;
}