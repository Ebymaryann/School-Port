$(document).ready(function () {
    $("#myForm").validate({
        rules: {
            FirstName: {
                required: true,
                minlength: 3
            },
            LastName: {
                required: true,
                minlength: 3
            },
            Email: {
                required: true,
                email: true
            },
            PhoneNumber: {
                required: true,
                digits: true,
                minlength: 11
            },
            DateOfBirth: {
                required: true,
                date: true
            },
            Gender: {
                required: true
            },
            State: {
                required: true
            },
            LGA: {
                required: true
            }
        },
        messages: {
            FirstName: {
                required: "Please enter your first name",
                minlength: "First name must be at least 3 characters"
            },
            LastName: {
                required: "Please enter your last name",
                minlength: "Last name must be at least 3 characters"
            },
            Email: {
                required: "Email is required",
                email: "Please enter a valid email address"
            },
            PhoneNumber: {
                required: "Phone number is required",
                digits: "Only numbers allowed",
                minlength: "Phone number must be at least 11 digits"
            },
            DateOfBirth: {
                required: "Please select your date of birth",
                date: "Please enter a valid date"
            },
            Gender: {
                required: "Please select your gender"
            },
            State: {
                required: "Please select your state"
            },
            LGA: {
                required: "Please select your LGA"
            }
        },
        errorPlacement: function (error, element) {
            var name = element.attr("name");
            $("#error-" + name).html(error);
        },
        success: function (label, element) {
            var name = $(element).attr("name");
            $("#error-" + name).html("");
        }
    });
});
