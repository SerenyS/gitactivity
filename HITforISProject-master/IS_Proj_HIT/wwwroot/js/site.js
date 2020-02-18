// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {

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

    $.validator.addMethod("valueNotEquals", function (value, element, arg) {
        return arg !== value;
    }, "Value must not equal arg.");

    $.validator.setDefaults({ ignore: ":hidden:not(select)" })
    //$.validator.setDefaults({ ignore: ":hidden:not(.chosen-select)" })

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
            MaritalStatusId: "required",
            SexId: "required",
            EthnicityId: "required",

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

            Dob: {
                required: "Please provide a valid date of birth",
                date: "Please provide a valid date of birth",
                maxDate: "Invalid Date from the future!",
                minDate: "Too far in the past!"
            },
            MiddleName: "Only one letter allowed",
            aliasFirstName: "Only letters allowed",
            aliasMiddleName: "Only letters allowed",
            aliasLastName: "Only letters allowed",
            MothersMaidenName: "Only letters allowed",
            MaidenName: "Only letters allowed",
            MaritalStatusId: "Select a Marital Status from the list",
            EthnicityId: "Select an Ethnicity from the list",
            SexId: "Select a Sex from the list"
            
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
            },
            MaritalStatusId: "required",
            SexId: "required",
            EthnicityId: "required",
            

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
            MaidenName: "Only letters allowed",
            MaritalStatusId: "Select a Marital Status from the list",
            EthnicityId: "Select an Ethnicity from the list",
            SexId: "Select a Sex from the list",
            Dob: {
                required: "Please provide a valid date of birth",
                date: "Please provide a valid date of birth",
                maxDate: "Please provide a valid date of birth",
                minDate: "Please provide a valid date of birth"
            }
            
        },
        // Make sure the form is submitted to the destination defined
        // in the "action" attribute of the form when valid
        submitHandler: function (form) {
            form.submit();
        }
    });

    var admitDateTime;
    $(document).ready(function () {
        admitDateTime = $("#admitDateTime").val();
    });
    $("#editEncounter").on("submit", function () {
        console.log(admitDateTime);
        if ($("#admitDateTime").val() == '') {
            $("#admitDateTime").val(admitDateTime);
            console.log($("#admitDateTime").val());
        }
    });

    $("form[name='encounter']").validate({
        // Specify validation rules
        rules: {
            RoomNumber: "required",
            ChiefComplaint: "required",
            FacilityId: "required",
            DepartmentId: "required",
            PointOfOriginId: "required",
            PlaceOfServiceId: "required",
            AdmitTypeId: "required",
            EncounterPhysiciansId: "required",
            EncounterTypeId: "required"
        },
        // Specify validation error messages
        messages: {
            RoomNumber: "Please enter the room number the patient is in for their encounter",
            ChiefComplaint: "Please enter the patients reason for coming to the hospitial",
            FacilityId: "Select a Facility from the list",
            DepartmentId: "Select a Department from the list",
            PointOfOriginId: "Select a Point of Origin from the list",
            PlaceOfServiceId: "Select a Place of Service from the list",
            AdmitTypeId: "Select an Admission Types from the list",
            EncounterPhysiciansId: "Select a Physician from the list",
            EncounterTypeId: "Select an Encounter Type from the list"

        },
        // Make sure the form is submitted to the destination defined
        // in the "action" attribute of the form when valid
        submitHandler: function (form) {
            form.submit();
        }
    });

    $("form[name='editEncounter']").validate({
        // Specify validation rules
        rules: {
            RoomNumber: "required",
            ChiefComplaint: "required",
            FacilityId: "required",
            DepartmentId: "required",
            PointOfOriginId: "required",
            PlaceOfServiceId: "required",
            AdmitTypeId: "required",
            EncounterPhysiciansId: "required",
            EncounterTypeId: "required"
        },
        // Specify validation error messages
        messages: {
            RoomNumber: "Please enter the room number the patient is in for their encounter",
            ChiefComplaint: "Please enter the patients reason for coming to the hospitial",
            FacilityId: "Select a Facility from the list",
            DepartmentId: "Select a Department from the list",
            PointOfOriginId: "Select a Point of Origin from the list",
            PlaceOfServiceId: "Select a Place of Service from the list",
            AdmitTypeId: "Select an Admission Types from the list",
            EncounterPhysiciansId: "Select a Physician from the list",
            EncounterTypeId: "Select an Encounter Type from the list"

        },
        // Make sure the form is submitted to the destination defined
        // in the "action" attribute of the form when valid
        submitHandler: function (form) {
            form.submit();
        }
    });

    // Calc age from DOB input in add/edit patient when leaving DOB field HELPMEIMDYING
    $('.dob').focusout(function () {
        var dob = $('.dob').val();
        console.log("Hello " + dob);
        console.log('Hi you left the field, ' + dob);

        if (dob != '') {
            var DateCreated = new Date(Date.parse(dob));
            var today = new Date();

            var dayDiff = Math.ceil(today - DateCreated) / (1000 * 60 * 60 * 24 * 365);
            console.log("dayDiff " + dayDiff);
            var age = parseInt(dayDiff);
            console.log("calcified age " + age);
            $('.age').text(age);

            // Format DOB in patient banner
            var str = $('.dob').val();
            var year = str.substring(0, 4);
            var month = str.substring(5, 7);
            var day = str.substring(8, 10);
            $('#calcDOB').text(month + '/' + day + '/' + year);

        }
    });

    // Update first, middle and last names in patient banner with those fields are edited in the Edit Page
    $('#FirstName').focusout(function () {
        var firstname = $('#FirstName').val();
        var middlename = $('#MiddleName').val();
        var lastname = $('#LastName').val();
        $('#fullName').text(firstname + " " + middlename + " " + lastname);
    });
    $('#MiddleName').focusout(function () {
        var firstname = $('#FirstName').val();
        var middlename = $('#MiddleName').val();
        var lastname = $('#LastName').val();
        $('#fullName').text(firstname + " " + middlename + " " + lastname);
    });
    $('#LastName').focusout(function () {
        var firstname = $('#FirstName').val();
        var middlename = $('#MiddleName').val();
        var lastname = $('#LastName').val();
        $('#fullName').text(firstname + " " + middlename + " " + lastname);
    });

    // When Edit page loads
    $(function () {
        //TODO: THIS CODE IS BROKEN AND ALWAYS THROWS AN ERROR. 
        //      IF YOU FIND WHAT THIS IS SUPPOSED TO DO, FIX IT MAYBE
        if (true) return;
        // DOB in Edit page patient banner
        var dob = $('.dob').val();
        if (dob != '') {
            var DateCreated = new Date(Date.parse(dob));
            var today = new Date();

            var dayDiff = Math.ceil(today - DateCreated) / (1000 * 60 * 60 * 24 * 365);

            var age = parseInt(dayDiff);

            $('.age').text(age);
            

        }
        // MRN in Edit page patient banner
        var mrn = $('.mrn').val();
        $('#calcMRN').text(mrn);

        // First, middle and last name
        var firstname = $('#FirstName').val();
        var middlename = $('#MiddleName').val();
        var lastname = $('#LastName').val();
        $('#fullName').append(firstname + '&nbsp;' + middlename + '&nbsp;' + lastname);

        // Format DOB in patient banner
        var str = $('.dob').val();
        var year = str.substring(0, 4);
        var month = str.substring(5, 7);
        var day = str.substring(8, 10);
        $('#calcDOB').text(month + '/' + day + '/' + year);
    });

    // Calc age in div in Details page
    $(function () {
        var dob = $('.dob').html();

        if (dob != '') {
            var DateCreated = new Date(Date.parse(dob));
            var today = new Date();

            var dayDiff = Math.ceil(today - DateCreated) / (1000 * 60 * 60 * 24 * 365);

            var age = parseInt(dayDiff);

            $('.age').text(age);
            $('.age').css('background-color', 'gold');

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


    $("#myInput").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#allergylist").filter(function () {
            $(this).toggle($(this).asp-items().toLowerCase().indexOf(value) > -1)
        });
    });

    $('.chosen').chosen({ width: '50%' });

});

//functions to show / hide the textarea within the PCA form

function ShowHidePerson() {
    var yes = document.getElementById('person');
    var area = document.getElementById('personArea');
    area.className = yes.checked ? 'form-control d-block mb-2' : 'd-none';
    
};

function ShowHidePlace() {
    var yes = document.getElementById('place');
    var area = document.getElementById('placeArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';

};

function ShowHideTime() {
    var yes = document.getElementById('time');
    var area = document.getElementById('timeArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
    
}

function ShowHidePurpose() {
    var yes = document.getElementById('purpose');
    var area = document.getElementById('purposeArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideHead() {
    var yes = document.getElementById('head');
    var area = document.getElementById('headArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideEye() {
    var yes = document.getElementById('eye');
    var area = document.getElementById('eyeArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideEar() {
    var yes = document.getElementById('ear');
    var area = document.getElementById('earArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideNose() {
    var yes = document.getElementById('nose');
    var area = document.getElementById('noseArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideMouth() {
    var yes = document.getElementById('mouth');
    var area = document.getElementById('mouthArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideRash() {
    var yes = document.getElementById('rash');
    var area = document.getElementById('rashArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideRed() {
    var yes = document.getElementById('red');
    var area = document.getElementById('redArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideHearing() {
    var yes = document.getElementById('hearing');
    var area = document.getElementById('hearingArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideDry() {
    var yes = document.getElementById('dry');
    var area = document.getElementById('dryArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideSign() {
    var yes = document.getElementById('sign');
    var area = document.getElementById('signArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideLung() {
    var yes = document.getElementById('lung');
    var area = document.getElementById('lungArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideDepth() {
    var yes = document.getElementById('depth');
    var area = document.getElementById('depthArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideRate() {
    var yes = document.getElementById('rate');
    var area = document.getElementById('rateArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideChest() {
    var yes = document.getElementById('chest');
    var area = document.getElementById('chestArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideLine() {
    var yes = document.getElementById('line');
    var area = document.getElementById('lineArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideHeart() {
    var yes = document.getElementById('heart');
    var area = document.getElementById('heartArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideHrate() {
    var yes = document.getElementById('Hrate');
    var area = document.getElementById('HrateArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideRhythm() {
    var yes = document.getElementById('rhythm');
    var area = document.getElementById('rhythmArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHidePulse() {
    var yes = document.getElementById('pulse');
    var area = document.getElementById('pulseArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideDrain() {
    var yes = document.getElementById('drain');
    var area = document.getElementById('drainArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideAbnormal() {
    var yes = document.getElementById('abnormal');
    var area = document.getElementById('abnormalArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideBowel() {
    var yes = document.getElementById('bowel');
    var area = document.getElementById('bowelArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideLump() {
    var yes = document.getElementById('lump');
    var area = document.getElementById('lumpArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideTube() {
    var yes = document.getElementById('tube');
    var area = document.getElementById('tubeArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideSkin() {
    var yes = document.getElementById('skin');
    var area = document.getElementById('skinArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideColor() {
    var yes = document.getElementById('color');
    var area = document.getElementById('colorArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideSensation() {
    var yes = document.getElementById('sensation');
    var area = document.getElementById('sensationArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideEndema() {
    var yes = document.getElementById('endema');
    var area = document.getElementById('endemaArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideRom() {
    var yes = document.getElementById('rom');
    var area = document.getElementById('romArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideStaple() {
    var yes = document.getElementById('staple');
    var area = document.getElementById('stapleArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideMood() {
    var yes = document.getElementById('mood');
    var area = document.getElementById('moodArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideCognition() {
    var yes = document.getElementById('cognition');
    var area = document.getElementById('cognitionArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideThought() {
    var yes = document.getElementById('thought');
    var area = document.getElementById('thoughtArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideSleep() {
    var yes = document.getElementById('sleep');
    var area = document.getElementById('sleepArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHidePain() {
    var yes = document.getElementById('pain');
    var area = document.getElementById('painArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideElimination() {
    var yes = document.getElementById('elimination');
    var area = document.getElementById('eliminationArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideAppetite() {
    var yes = document.getElementById('appetite');
    var area = document.getElementById('appetiteArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}

function ShowHideActivity() {
    var yes = document.getElementById('activity');
    var area = document.getElementById('activityArea');
    area.className = yes.checked ? 'form-control d-block' : 'd-none';
}
    


