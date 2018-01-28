var _loginObj = () => {
    var self = this;

    // html element Declaraction object
    this.elemObj = {
        strEmail: ko.observable("").extend({
            required: true,
            message: "Enter valid email Id.",
            email : true
        }),
        strPassword: ko.observable("").extend({ required: true, message: "Please enter password." }),
        boolRememberMe: ko.observable(false)
    }

    // Validate the login page.
    this.validateLogIn = () => {
        elemOnbj = self.elemObj;
    };

    // error grouping..
    self.errors = ko.validation.group(this.elemObj, { observable: true, deep: true });
}
