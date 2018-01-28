userObj = () => {
    var self = this;

    // html element Declaraction object
    $('.PasswordContent').hide();
    $('.PasswordSetUpContent').hide();
    this.elemObj = {
        strEmail: ko.observable("").extend({
            required: true,
            message: "Enter valid email Id.",
            email: true
        }),
        strPassword: ko.observable("").extend({ required: true, message: "Please enter password." }),
        boolRememberMe: ko.observable(false),
        strFPEmail: ko.observable("").extend({
            required: true,
            message: "Enter valid email Id.",
            email: true
        }),
        strFPOtp: ko.observable("").extend({ required: true, message: "Please Valid OTP" }),
        strFPNPassword: ko.observable("").extend({ required: true, message: "Please enter password." }),
        strFPRPassword: ko.observable("").extend({ required: true, message: "Please enter password." })
    }

    // Validate the login page.
    this.validateLogIn = () => {
        elemOnbj = self.elemObj;
    };


    // Validate the Forgot Password page.
    this.validateEmail = () => {
        elem = self.elemObj;
        $('.EmailContent').hide();
        $('.PasswordContent').show();
    };
    // Validate the Forgot Password OTP Page.
    this.validateOTP = () => {
        elem = self.elemObj;
        $('.EmailContent').hide();
        $('.PasswordContent').hide();
        $('.PasswordSetUpContent').show();
    };
    this.validateNewPassword = () => {
        elem = self.elemObj;
        alert("ok")
    };
    // error grouping..
    self.errors = ko.validation.group(this.elemObj, { observable: true, deep: true });
}





$(document).ready(function () {
    ko.validation.rules.pattern.message = 'Invalid.';

    ko.validation.init({
        insertMessages: true,
        decorateElement: true,
        errorElementClass: 'has-error',
        errorMessageClass: 'help-block'
    }, true);

    ko.applyBindings(userObj);
});

