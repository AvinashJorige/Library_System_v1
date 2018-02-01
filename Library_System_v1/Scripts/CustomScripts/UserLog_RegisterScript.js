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
        elemnObj = self.elemObj;
        var _logModel = {};
        _logModel.Email = elemnObj.strEmail();
        _logModel.Password = elemnObj.strPassword();

        var url = "/Login/Index";

        Common.Ajax('POST', url, { _logModel }, 'json', (res) => {
            if (res && res.redirectionURL) {
                sessionStorage("logInfo", log_info, 'set');
                location.href = res.redirectionURL;
            }
            else {
                $(".alert-danger").show();
                $(".dangerMsg").html(res.error);
            }
        });
    };


    // Validate the Forgot Password page.
    this.validateEmail = () => {
        elem = self.elemObj;

        if (elem.strFPEmail()) {
            var _forgetPasswordModel = {};
            _forgetPasswordModel.Email = elem.strFPEmail();
            Common.Ajax('POST', "/Login/forgetPassword", { _forgetPasswordModel }, 'json', (res) => {
                if (res && res.obj) {
                    console.log("otpCode", res.otpCode);
                    $('.EmailContent').hide();
                    $(".alert-danger").hide();
                    $(".alert-info").show();
                    $(".infoMsg").html("otpCode :" + res.otpCode);
                    $('.PasswordContent').show();
                }
                else {
                    $(".alert-danger").show();
                    $(".dangerMsg").html(res.error);
                }
            },
            (error) => {
                alert("Invalid email. Doesn't matches with our records");
                console.log("ERROR EMAIL VALIDATION :", error);
            });
        }
    };
    // Validate the Forgot Password OTP Page.
    this.validateOTP = () => {
        elem = self.elemObj;
        var _fPass = {};
        _fPass.Otp = elem.strFPOtp();
        Common.Ajax('POST', "/Login/VerifyOtp", { _fPass }, 'json', (res) => {
            if (res && res.obj) {
                $('.EmailContent').hide();
                $('.PasswordContent').hide();
                $('.PasswordSetUpContent').show();
            }
            else {
                $(".alert-danger.dangerMsg").show().html(res.error);
            }
        },
        (error) => {
            alert("Invalid OTP Number");
            console.log("ERROR OTP VALIDATION :", error);
        });
    };

    this.validateNewPassword = () => {
        elem = self.elemObj;
        var _lgModel = {};
        _lgModel.Password = elem.strFPNPassword();
        Common.Ajax('POST', "/Login/PasswordReset", { _lgModel }, 'json', (res) => {
            if (!res.error) {
                location.href = "/Login/Index";
            }
            else {
                $(".alert-danger.dangerMsg").show().html(res.error);
            }
        },
        (error) => {
            alert("error : ", error);
            console.log("ERROR PASSWORD CHANGE :", error);
        });
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

