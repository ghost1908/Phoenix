function AddPersonPartyEvent(e) {
    var addRow = $(e).closest("tr");
    var newRow = $(e).closest("tbody").find('tr[data-id="newrow"]').clone(true, true);
    $(newRow).attr('data-id', '');

    var newTicketNumber = $(addRow).find('#partyTicketNumber');
    var newEntryDate = $(addRow).find('#partyEntryDate');
    var newAdoptionDate = $(addRow).find('#partyAdoptionDate');
    var newAdoptionNumber = $(addRow).find('#partyAdoptionNumber');
    var newAdoptionComment = $(addRow).find('#partyAdoptionComment');
    var newDisposalDate = $(addRow).find('#partyDisposalDate');
    var newDisposalNumber = $(addRow).find('#partyDisposalNumber');
    var newDisposalCauseNames = $(addRow).find('#partyDisposalCauseNames');
    var newDisposalComment = $(addRow).find('#partyDisposalComment');

    $(newRow).children().eq(0).html($(newTicketNumber).val());
    $(newRow).children().eq(1).html($(newEntryDate).val() === "" ? "" : moment($(newEntryDate).val(), 'DD-MM-YYYY').format('DD.MM.YYYY'));
    $(newRow).children().eq(2).attr('data-pp-anumber', $(newAdoptionNumber).val().trim());
    $(newRow).children().eq(2).attr('data-pp-adate', moment($(newEntryDate).val(), 'DD-MM-YYYY').format('DD.MM.YYYY'));
    $(newRow).children().eq(3).attr('data-pp-acomment', ($(newAdoptionComment).val().trim() == "" ? "" : ('\n' + $(newAdoptionComment).val().trim())));
    $(newRow).children().eq(2).html($(newAdoptionNumber).val().trim() + ' від ' + ($(newAdoptionDate).val() === "" ? "" : moment($(newAdoptionDate).val(), 'DD-MM-YYYY').format('DD.MM.YYYY')) + ($(newAdoptionComment).val().trim() == "" ? "" : ('\n' + $(newAdoptionComment).val().trim())));
    $(newRow).children().eq(3).attr('data-pp-dnumber', $(newDisposalNumber).val().trim());
    $(newRow).children().eq(3).attr('data-pp-ddate', moment($(newDisposalDate).val(), 'DD-MM-YYYY').format('DD.MM.YYYY'));
    $(newRow).children().eq(3).attr('data-pp-dcause', $(newDisposalCauseNames).find(":selected").val());
    $(newRow).children().eq(3).attr('data-pp-dcomment', ($(newDisposalComment).val().trim() == "" ? "" : ('\n' + $(newDisposalComment).val().trim())));
    $(newRow).children().eq(3).html($(newDisposalNumber).val().trim() + ' від ' + ($(newDisposalDate).val() === "" ? "" : moment($(newDisposalDate).val(), 'DD-MM-YYYY').format('DD.MM.YYYY')) + '\n' + $(newDisposalCauseNames).find(":selected").text() + ($(newDisposalComment).val().trim() == "" ? "" : ('\n' + $(newAdoptionComment).val().trim())));

    $(addRow).before(newRow);
    $(newRow).removeAttr('class');
    $(addRow).children().eq(3).children().eq(0).val('');
}

function AcceptPersonPartyEvent(e) {
    alert(e);
}

function EditPersonPartyEvent(e) {
    var currentRow = $(e).closest('tr');
    var currentData = $(currentRow).find('td');
    var editRow = $(e).closest("tbody").find('tr[data-id="editrow"]');

    editRow.insertBefore(currentRow);
    $(editRow).attr('data-editid', $(currentRow).attr('data-id'));
    editRow.removeClass('d-none');

    var editTicketNumber = $(editRow).find('#editPartyTicketNumber');
    var editEntryDate = $(editRow).find('#editPartyEntryDate');
    var editAdoptionDate = $(editRow).find('#editPartyAdoptionDate');
    var editAdoptionNumber = $(editRow).find('#editPartyAdoptionNumber');
    var editAdoptionComment = $(editRow).find('#editPartyAdoptionComment');
    var editDisposalDate = $(editRow).find('#editPartyDisposalDate');
    var editDisposalNumber = $(editRow).find('#editPartyDisposalNumber');
    var editDisposalCauseNames = $(editRow).find('#editPartyDisposalCauseNames');
    var editDisposalComment = $(editRow).find('#editPartyDisposalComment');

    $(editTicketNumber).val($(currentData)[0].innerText);
    $(editEntryDate).val($(currentData)[1].innerText);
    if ($($(currentData)[2]).attr('data-pp-adate') != "") {
        $('#editPartyAdoption').prop('checked', true);
        $('#editPartyAdoption').trigger('change');
    }
    $(editAdoptionDate).val($($(currentData)[2]).attr('data-pp-adate'));
    $(editAdoptionNumber).val($($(currentData)[2]).attr('data-pp-anumber'));
    $(editAdoptionComment).val($($(currentData)[2]).attr('data-pp-acomment'));
    if ($($(currentData)[3]).attr('data-pp-ddate') != "") {
        $('#editPartyDisposal').prop('checked', true);
        $('#editPartyDisposal').trigger('change');
    }
    $(editDisposalDate).val($($(currentData)[3]).attr('data-pp-ddate'));
    $(editDisposalNumber).val($($(currentData)[3]).attr('data-pp-dnumber'));
    $(editDisposalCauseNames).val($($(currentData)[3]).attr('data-pp-dcause'));
    $(editDisposalComment).val($($(currentData)[3]).attr('data-pp-dcomment'));

    currentRow.addClass('d-none');
    $(e).closest("tbody").find('tr[data-id="addrow"]').addClass('d-none');
}

function CancelPersonPartyEvent(e) {
    var currentRow = $(e).closest('tr');
    var editRow = $(e).closest("tbody").find('tr[data-id="' + $(currentRow).attr('data-editid') + '"]');

    editRow.insertBefore(currentRow);
    editRow.removeClass('d-none');
    currentRow.addClass('d-none');
    $(e).closest("tbody").find('tr[data-id="addrow"]').removeClass('d-none');
}

function DeletePersonPartyEvent(e) {
    var currentRow = $(e).closest('tr');
    var editRow = $(e).closest("tbody").find('tr[data-id="' + $(currentRow).attr('data-editid') + '"]');

    $(editRow).attr('data-action', '2');
    editRow.insertBefore(currentRow);
    editRow.removeClass('d-none');
    currentRow.addClass('d-none');
    $(e).closest("tbody").find('tr[data-id="addrow"]').removeClass('d-none');
}

$('#biPartyAdoptionDate').on('dp.change', function (e) {
    var entDate = moment($('#partyEntryDate').val(), 'DD.MM.YYYY');
    if (entDate.isValid()) {
        if (entDate > e.date) {
            $('#partyEntryDate').val(e.date.format(e.date._f));
        }
    }
});

$('#biPartyDisposalDate').on('dp.change', function (e) {
    var adoDate = moment($('#partyAdoptionDate').val(), 'DD.MM.YYYY');
    if (adoDate.isValid()) {
        if (adoDate > e.date) {
            $('#partyDisposalDate').val($('#partyAdoptionDate').val());
        }
    }
});

$('#partyAdoption').change(function () {
    if ($(this).is(':checked')) {
        $('#biPartyAdoptionDate').removeClass('d-none');
        $('#partyAdoptionNumber').removeClass('d-none');
        $('#partyAdoptionComment').removeClass('d-none');
        if ($('#partyEntryDate').val() != '')
            $('#partyAdoptionDate').val($('#partyEntryDate').val());
    }
    else {
        $('#biPartyAdoptionDate').addClass('d-none');
        $('#partyAdoptionNumber').addClass('d-none');
        $('#partyAdoptionComment').addClass('d-none');
        $('#partyAdoptionDate').val('');
    }
});

$('#partyDisposal').change(function () {
    if ($(this).is(':checked')) {
        $('#biPartyDisposalDate').removeClass('d-none');
        $('#partyDisposalNumber').removeClass('d-none');
        $('#partyDisposalCauseNames').removeClass('d-none');
        $('#partyDisposalComment').removeClass('d-none');
    }
    else {
        $('#biPartyDisposalDate').addClass('d-none');
        $('#partyDisposalNumber').addClass('d-none');
        $('#partyDisposalCauseNames').addClass('d-none');
        $('#partyDisposalComment').addClass('d-none');
        $('#partyDisposalDate').val('');
    }
});

$('#editPartyAdoption').change(function () {
    if ($(this).is(':checked')) {
        $('#editBiPartyAdoptionDate').removeClass('d-none');
        $('#editPartyAdoptionNumber').removeClass('d-none');
        $('#editPartyAdoptionComment').removeClass('d-none');
    }
    else {
        $('#editBiPartyAdoptionDate').addClass('d-none');
        $('#editPartyAdoptionNumber').addClass('d-none');
        $('#editPartyAdoptionComment').addClass('d-none');
        $('#editPartyAdoptionDate').val('');
    }
});

$('#editPartyDisposal').change(function () {
    if ($(this).is(':checked')) {
        $('#editBiPartyDisposalDate').removeClass('d-none');
        $('#editPartyDisposalNumber').removeClass('d-none');
        $('#editPartyDisposalCauseNames').removeClass('d-none');
        $('#editPartyDisposalComment').removeClass('d-none');
    }
    else {
        $('#editBiPartyDisposalDate').addClass('d-none');
        $('#editPartyDisposalNumber').addClass('d-none');
        $('#editPartyDisposalCauseNames').addClass('d-none');
        $('#editPartyDisposalComment').addClass('d-none');
        $('#editPartyDisposalDate').val('');
    }
});