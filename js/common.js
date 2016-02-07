$(document).ready(function () {
    $.fn.callAjax = function (options) {
        var extOptions = $.extend({}, { type: "POST",contentType: "application/json; charset=utf-8", asyn: true, dataType: "json" }, options);
        var xhr = $.ajax(extOptions);
        return xhr;
    };

    $.fn.validateEmail = function (email) {
        var emailRegex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        return (!emailRegex.test(email)) ? false : true;
    };
});