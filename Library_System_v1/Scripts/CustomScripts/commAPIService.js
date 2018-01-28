//
// refer link :
// http://www.csharpdocs.com/generic-ajax-function-using-jquery/
// 

var Common = {
    Ajax: function (httpMethod, url, data, type, successCallBack, async, cache) {
        if (typeof async == "undefined") {
            async = false;
        }
        if (typeof cache == "undefined") {
            cache = false;
        }

        var ajaxObj = $.ajax({
            type: httpMethod.toUpperCase(),
            url: url,
            data: JSON.stringify(data),
            dataType: type,
            contentType: "application/json; charset=utf-8",
            async: async,
            cache: cache,
            success: successCallBack,
            error: function (err, type, httpStatus) {
                Common.AjaxFailureCallback(err, type, httpStatus);
            }
        });

        return ajaxObj;
    },

    DisplaySuccess: function (message) {
        Common.ShowSuccessSavedMessage(message);
    },

    DisplayError: function (error) {
        Common.ShowFailSavedMessage(message);
    },

    AjaxFailureCallback: function (err, type, httpStatus) {
        var failureMessage = 'Error occurred in ajax call' + err.status + " - " + httpStatus;
        console.log(failureMessage);
    },

    ShowSuccessSavedMessage: function (messageText) {

        //use jquery BlockUI library to display message

        $.blockUI({ message: messageText });
        setTimeout($.unblockUI, 1500);
    },

    ShowFailSavedMessage: function (messageText) {

        //use jquery BlockUI library to display message

        $.blockUI({ message: messageText });
        setTimeout($.unblockUI, 1500);
    }
};