
var _forgotObj = () => {
    var self = this;

    // html element Declaraction object
    this.elemObj = {
        strFPEmail: ko.observable("").extend({
            required: true,
            message: "Enter valid email Id.",
            email: true
        }),
        strFPBindEmail: ko.observable(false)
    }

    // Validate the login page.
    this.validateEmail = () => {
        elem = self.elemObj;
        elem.strFPBindEmail = ko.observable(true);
    };

    // error grouping..
    self.errors = ko.validation.group(this.elemObj, { observable: true, deep: true });

}

$(document).ready(function () {
    //ko.cleanNode(document.getElementById("logInForm"));
    ko.validation.rules.pattern.message = 'Invalid.';

    ko.validation.init({
        insertMessages: true,
        decorateElement: true,
        errorElementClass: 'has-error',
        errorMessageClass: 'help-block'
    }, true);

    ko.applyBindings(_forgotObj, document.getElementById('forgotPasswordForm'));
});