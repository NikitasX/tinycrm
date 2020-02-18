// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    // https://jsfiddle.net/h0to89uj/39/
    function GetFormData(form) {

        if (typeof form == undefined
            || form.length == 0
            || form.trim() == '') {
            return alert('Form/fields was not submitted or wrong input');
        }

        let formObject = [];

        $(form).each(function () {
            formObject.push({
                Name: $(this).attr('name'),
                Value: $(this).val()
            });
        });
        return formObject;
    }

    $('#CreateOptions_VatNumber').val("Mbambis");
    $('#CreateOptions_Email').val("Mbambis@sougias.gr");

    // https://jsfiddle.net/1yxrkf2q/1/
    function ValidateVatString(vat) {
        if (vat.trim() == '' || typeof vat == undefined) {
            return alert('Vat null or whitespace');
        }

        if (vat.length != 9) {
            return alert('Not a Vat. Not 9 digits');
        }
    }

    // https://jsfiddle.net/1yxrkf2q/1/
    function ValidateEmailString(email) {
        if (email.trim() == '' || typeof email == undefined) {
            return alert('Email null or whitespace');
        }

        if (email.indexOf('@') <= 0
            || email.indexOf('.') <= 0) {
            return alert('Not an email');
        }
    }

    $('#CreateOptions_Email').on('blur', () => ValidateEmailString($
        ('#CreateOptions_Email').val()));
    $('#CreateOptions_VatNumber').on('blur', () => ValidateVatString($
        ('#CreateOptions_VatNumber').val()));


    /*
     * 
     *    function GetFormData() {

        let formObject = [];

        $('form input').each(function (k, v) {

            let objectKey = $(`label[for="${v.id}"]`).text();

            formObject.push({
            	Name: objectKey,
              Value: $(this).val()
            });
        });
				//console.log(formObject);
    }
     * 
     * /

    $('#CreateOptions_VatNumber').val("Mbambis");
    $('#CreateOptions_Email').val("Mbambis@sougias.gr");

    GetFormData();

});