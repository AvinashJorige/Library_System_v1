
$(document).ready(function () {
    ko.validation.rules.pattern.message = 'Invalid.';

    ko.validation.init({
        insertMessages: true,
        decorateElement: true,
        errorElementClass: 'has-error',
        errorMessageClass: 'help-block'
    }, true);

    ko.applyBindings({
        login: _loginObj,
        forgotpassword: _forgotObj
    });
});