// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    console.log('Hi');

    $.validator.addMethod("alphabetsnspace", function (value, element) {
        return this.optional(element) || /^[a-zA-Z ]*$/.test(value);
    });

    // Check for DOB of less that today's date
    $.validator.addMethod("maxDate", function (value, element) {
        var curDate = new Date();
        var inputDate = new Date(value);
        if (inputDate < curDate)
            return true;
        return false;
    }, "Invalid Date from the future!");   // error message

    // Check for DOB of 100 years before this year
    $.validator.addMethod("minDate", function (value, element) {
        var testYear = (new Date).getFullYear();
        var testDate = new Date((testYear - 100), 1, 1);
        var inputDate = new Date(value);
        if (inputDate > testDate)
            return true;
        return false;
    }, "Too far in the past!");   // error message


    $("form[name='patient']").validate({
        // Specify validation rules
        rules: {
            FirstName: {
                required: true,
                alphabetsnspace: true
            },
            MiddleName: "alphabetsnspace",
            lastName: {
                required: true,
                alphabetsnspace: true
            },
            aliasFirstName: "alphabetsnspace",
            aliasMiddleName: "alphabetsnspace",
            aliasLastName: "alphabetsnspace",
            MaidenName: "alphabetsnspace",
            MothersMaidenName: "alphabetsnspace",
            Dob: {
                required: true,
                date: true,
                maxDate: true,
                minDate: true
            },
            Ssn: {
                required: true,
                minlength: 9
            }

        },
        // Specify validation error messages
        messages: {
            
            FirstName: {
                required: "Please provide a first name",
                alphabetsnspace: "First name must be all letters"
            },
            lastName: {
                required: "Please provide a last name",
                alphabetsnspace: "Last name must be all letters"
            },
            Ssn: {
                required: "Please provide a Social Security Number",
                minlength: "Your SSN must be at least 10 digits long"
            },
            MiddleName: "Only one letter allowed",
            aliasFirstName: "Only letters allowed",
            aliasMiddleName: "Only letters allowed",
            aliasLastName: "Only letters allowed",
            MothersMaidenName: "Only letters allowed",
            MaidenName: "Only letters allowed"
        },
        // Make sure the form is submitted to the destination defined
        // in the "action" attribute of the form when valid
        submitHandler: function (form) {
            form.submit();
        }
    });

    $("form[name='patientEdit']").validate({
        // Specify validation rules
        rules: {
            FirstName: {
                required: true,
                alphabetsnspace: true
            },
            MiddleName: "alphabetsnspace",
            lastName: {
                required: true,
                alphabetsnspace: true
            },
            aliasFirstName: "alphabetsnspace",
            aliasMiddleName: "alphabetsnspace",
            aliasLastName: "alphabetsnspace",
            MaidenName: "alphabetsnspace",
            MothersMaidenName: "alphabetsnspace",
            Dob: {
                required: true,
                date: true,
                maxDate: true,
                minDate: true
            },
            Ssn: {
                required: true,
                minlength: 9
            }

        },
        // Specify validation error messages
        messages: {
            FirstName: {
                required: "Please provide a first name",
                alphabetsnspace: "First name must be all letters"
            },
            lastName: {
                required: "Please provide a last name",
                alphabetsnspace: "Last name must be all letters"
            },
            Ssn: {
                required: "Please provide a Social Security Number",
                minlength: "Your SSN must be at least 10 digits long"
            },
            MiddleName: "Only one letter allowed",
            aliasFirstName: "Only letters allowed",
            aliasMiddleName: "Only letters allowed",
            aliasLastName: "Only letters allowed",
            MothersMaidenName: "Only letters allowed",
            MaidenName: "Only letters allowed"
        },
        // Make sure the form is submitted to the destination defined
        // in the "action" attribute of the form when valid
        submitHandler: function (form) {
            form.submit();
        }
    });

    $("form[name='employment']").validate({
        // Specify validation rules
        rules: {
            employerName: {
                required: true
            },
            address: {
                required: true
            },
            PhoneNumber: {
                required: true,
                number: true,
                maxlength: 10,
                minlength: 10

            },
            occupation: {
                required: true
            }
        },
        // Specify validation error messages
        messages: {
            employerName: {
                required: "Please provide an Employer Name"
            },
            address: {
                required: "Please provide an address"
            },
            PhoneNumber: {
                required: "Please provide a phone number",
                number: "Must be a 10 digit number starting with 1",
                minlength: "Too short - make it a 10 digit number",
                maxlength: "Too long - make it a 10 digit number"
            },
            occupation: {
                required: "Please provide an occupation"
            }
        },
        // Make sure the form is submitted to the destination defined
        // in the "action" attribute of the form when valid
        submitHandler: function (form) {
            form.submit();
        }
    });

    // Calc age from DOB input in add/edit patient when leaving DOB field
    $('.dob').focusout(function () {
        var dob = $('.dob').val();
        console.log("Hello " + dob);
        if (dob != '') {
            var DateCreated = new Date(Date.parse(dob));
            var today = new Date();

            var dayDiff = Math.ceil(today - DateCreated) / (1000 * 60 * 60 * 24 * 365);
            console.log("dayDiff " + dayDiff);
            var age = parseInt(dayDiff);
            console.log("age " + age);
            $('.age').text(age);

        }
    });

    // Calc age in div in Details page
    $(function () {
        var dob = $('.dob').html();
        var dob1 = $('.dob').val();
        
        if (dob != '') {
            var DateCreated = new Date(Date.parse(dob));
            var today = new Date();

            var dayDiff = Math.ceil(today - DateCreated) / (1000 * 60 * 60 * 24 * 365);
            
            var age = parseInt(dayDiff);
            
            $('.age').text(age);
            $('.age').css('background-color', 'gold');

        }

        if (dob1 != '') {
            var DateCreated = new Date(Date.parse(dob1));
            var today = new Date();

            var dayDiff = Math.ceil(today - DateCreated) / (1000 * 60 * 60 * 24 * 365);
            console.log("dayDiff " + dayDiff);
            var age = parseInt(dayDiff);
            console.log("age " + age);
            $('.age').text(age);

        }
    });

    // When SSN is being edited, the dashes are added
    $('#Ssn').keyup(function () {
        var val = this.value.replace(/\D/g, '');
        val = val.replace(/^(\d{3})/, '$1-');
        val = val.replace(/-(\d{2})/, '-$1-');
        val = val.replace(/(\d)-(\d{4}).*/, '$1-$2');
        this.value = val;
    });

    // When page loads, if SSN field is on there, this adds the dashes
    $(function () {
        var val = $('#Ssn').text();
        val.replace(/\D/g, '');
        val = val.replace(/^(\d{3})/, '$1-');
        val = val.replace(/-(\d{2})/, '-$1-');
        val = val.replace(/(\d)-(\d{4}).*/, '$1-$2');
        $('#Ssn').html(val);
    });

    // When user clicks save or create on Add Patient or Edit Patient page
    $('#formatSsnAndSubmitForm').on('click', function () {
        var temp = $('#Ssn').val();
        var fixed = temp.replace(/-/g, '')
        $('#Ssn').val(fixed);
    });


    $('#formatSsnAndSubmitForm').hide();

    $("#patientEdit").on('change', function () {
        $('#formatSsnAndSubmitForm').show();
    });

    $('#patient').on('change', function () {
        $('#formatSsnAndSubmitForm').show();
    });

    $("#showAddAlertModal").click(function () {
        alert("HI");
        //modal.style.display = "block";
        //$("#ModelPopUp").modal('show');
    });

     
    


    $("#myInput").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#allergylist").filter(function () {
            $(this).toggle($(this).asp-items().toLowerCase().indexOf(value) > -1)
        });
    });
   
});
    


