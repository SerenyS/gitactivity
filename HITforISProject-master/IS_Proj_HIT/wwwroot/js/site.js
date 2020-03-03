﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
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

    //========= PCA elements ===========
    // Set Pain values
    $('#cryingButtonGroup').on('click', 'button', function () {
        // Get element that was clicked
        $('#cryingValue').text(this.value);
    });
    // Lock/unlock selected pain assessment
    $('.painAssessment').on('click', function (e) {
        // First close all 
        $('.painAssessment').each(function () {
            $('#collapse' + $(this).attr("value")).removeClass('show');
        });
        // Now open the selected assessment
        $('#collapse' + this.value).addClass('show');
    });
    // PCA informational messages
    $('#PcaInfoModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        var infoSection = button.data('whatever') // Extract info from data-* attributes
        // Update the modal's content. 
        // TODO: Is there a way to get this information from the database instead?
        var msg = ""
        var ttl = ""
        if (infoSection.substring(0, 5) == "cries") { ttl = "CRIES Scale" }
        else if (infoSection.substring(0, 9) == "nonVerbal") { ttl="Non-Verbal Assessment"}
        if (infoSection == "WongBaker") {
            ttl = "The FACES Instructions for Usage";
            msg = 
                "The FACES Scale Guidelines<br />" +
                "•	The FACES Scale is recommended for people ages three and older; it is not limited to children.<br/>" +
                "•	This self - assessment tool must be understood by the patient, so they are able to choose the face that best illustrates the physical pain they are experiencing. The tool is not for use with infants or patients who are unresponsive; other tools have been provided for this purpose.<br/>" +
                "•	It is a self - assessment tool and should not be used by a third person, parents, healthcare professionals, or caregivers, to assess the patient’s pain.There are other tools for those purposes.<br/><br/>" + 
                "Explain to the person that each face represents a person who has no pain(hurt), or some, or a lot of pain. Face 0 doesn’t hurt at all. Face 2 hurts just a little bit. Face 4 hurts a little bit more. Face 6 hurts even more. Face 8 hurts a whole lot. Face 10 hurts as much as you can imagine although you don’t have to be crying to have this worst pain.<br /> <br />"+
                "Ask the person to choose the face that best depicts the pain they are experiencing.<br />"
        } else if (infoSection == "cries") {
            msg = "CRIES assesses crying, oxygenation, vital signs, facial expression, and sleeplessness. It is often used for infants six months old and younger and is widely used in the neonatal intensive care setting."
        } else if (infoSection == "criesCrying") {
            ttl ="Crying - Characteristic cry of pain is high pitched"
            msg = "0 - No cry or cry that is not high-pitched<br/>" +
                "1 - Cry high pitched but baby is easily consolable<br/>" +
                "2 - Cry high pitched and baby is inconsolable<br/>"
        } else if (infoSection == "criesO2") {
            ttl = "Requires O2 for SaO2 < 95% - Babies experiencing pain manifest decreased oxygenation. " +
                "Consider other causes of hypoxemia, e.g., oversedation, atelectasis, pneumothorax)"
            msg = "0 - No oxygen required<br/>" +
                "1 - < 30% oxygen required<br/>" +
                "2 - > 30% oxygen required<br/>"
        } else if (infoSection == "criesVitalIncrease") {
            ttl = "Increased vital signs (BP and HR) - Take BP last as this may awaken child making other assessments difficult"
            msg = "0 - Both HR and BP unchanged or less than baseline<br/>" +
                "1 - HR or BP increased but increase is < 20% of baseline<br/>" +
                "2 - HR or BP is increased > 20% over baseline<br/>"
        } else if (infoSection == "criesExpression") {
            ttl = "Expression - The facial expression most often associated with pain is a grimace. " +
                "A grimace may be characterized by brow lowering, eyes squeezed shut, deepening naso-labial furrow, " +
                "or open lips and mouth."
            msg = "0 - No grimace present<br/>" +
                "1 - Grimace alone is present<br/>" +
                "2 - Grimace and non-cry vocalization grunt it present<br/>"
        } else if (infoSection == "criesSleepless") {
            ttl = "Sleepless - Scored based upon the infant's state during the hour preceding this recorded score."
            msg = "0 - Child has been continuously asleep<br/>" +
                "1 - Child has awakened at frequent intervals<br/>" +
                "2 - Child has been awake constantly<br/>"
        } else if (infoSection == "nonVerbal") {
            ttl = "Nonverbal Pain Assessment"
            msg = "Quantifies pain in patients unable to speak (due to intubation, dementia, etc). Each of 5 vitals has up to three fields: vital name, vital value, and vital description."
        } else if (infoSection == "nonVerbalFace") {
            ttl = "Face"
            msg = "0 - No particular expression or smile<br/>" +
                "1 - Occasional grimace, tearing, frowning, wrinkled forehead<br/>" +
                "2 - Frequent grimace, tearing, frowning, wrinkled forehead<br/>"
        } else if (infoSection == "nonVerbalMovement") {
            ttl = "Activity (movement)"
            msg = "0 - Lying quietly, normal position<br/>" +
                "1 - Seeking attention through movement or slow, cautious movement <br /> " +
                "2 - Restless, excessive activity and/or withdrawal reflexes<br/>"
        } else if (infoSection == "nonVerbalGuarding") {
            ttl = "Guarding"
            msg = "0 - Lying quietly, no positioning of hands over areas of the body<br/>" +
                "1 - Splinting areas of the body, tense<br/>" +
                "2 - Rigid, stiff<br/>"
        } else if (infoSection == "nonVerbalPhysiology") {
            ttl = "Physiology (vital signs)"
            msg = "0 - Baseline vital signs unchanged<br/>" +
                "1 - Change in SBP>20 mmHg or HR>20 bpm<br/>" +
                "2 - Change in SBP>30 mmHg or HR>25 bpm<br/>"
        } else if (infoSection == "nonVerbalRespiratory") {
            ttl = "Respiratory"
            msg = "0 - Baseline RR/SpO2 synchronous with ventilator<br/>" +
                "1 - RR>10 bpm over baseline, 5% decrease SpO2 or mild ventilator asynchrony<br/>" +
                "2 - RR>20 bpm over baseline, 10% decrease SpO2 or severe ventilator asynchrony<br/>"
        } else { msg = "Not found" }
        var modal = $(this)
        modal.find('.modal-title').html(ttl)
        modal.find('.modal-body p').html(msg)
    });


});

//function to show / hide the textarea within the PCA form
function ShowHide(id, idArea) {
    var yes = document.getElementById(id);
    var area = document.getElementById(idArea);
    area.className = yes.checked ? 'form-control d-block mb-2' : 'd-none';
}
    