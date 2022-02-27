// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


var Status = {
    Ok: 0,
    Error: 1,
    BadRequest: 2,
    NotFound: 3
}


$.AjaxDelete = function (url, model) {
    $.ajax(
        {

            type: "post",
            url: url,
            //contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: model,
            success: function (result) {
                if (result.status == Status.Ok) {
                    $.ShowSuccess('İşlem Başarılı', result.message)
                    $("tr[data-id=" + model.id + "]").hide(500, function () {
                        $(this).remove()
                    })
                }
                else if (result.status == Status.Error) {
                    $.ShowError('Uyarı', result.message)
                }
                else if (result.status == Status.BadRequest) {
                    $.ShowError('Uyarı', result.message)
                }
                else if (result.status == Status.NotFound) {
                    $.ShowError('Hata Oluştu', result.message)
                }

            }
        })
}


$.AjaxCreate = function (url, model) {
    $.ajax(
        {
            type: "post",
            url: url,
            dataType: "json",
            data: model,
            success: function (result) {
                if (result.status == Status.Ok) {
                    window.location = "/Account/Index/";
                    $.ShowSuccess('İşlem Başarılı', result.message);


                }
                else if (result.status == Status.BadRequest) {
                    $.ShowError('Uyarı', result.message);
                }
                else if (result.status == Status.NotFound) {
                    $.ShowError('Hata Oluştu', result.message);

                }

            }
        })
}


$.ShowError = function (title, content) {
    $.alert({
        title: title,
        content: content,
        type: 'red',
        closeIcon: true,
        typeAnimated: true,
        boxWidth: '35%',
        useBootstrap: false,
    });
}

$.ShowSuccess = function (title, content) {
    $.alert({
        title: title,
        content: content,
        type: 'green',
        closeIcon: true,
        typeAnimated: true,
        boxWidth: '35%',
        useBootstrap: false,
        offsetTop: 10,
    });
}



$.ShowConfirm = function (title, content, okButtonText, canselButtonText, okButtonDelegate) {
    $.confirm({
        title: title,
        content: content,
        type: 'orange',
        closeIcon: true,
        typeAnimated: true,
        boxWidth: '35%',
        useBootstrap: false,
        offsetTop: 40,
        buttons: {
            ok: {
                text: okButtonText,
                btnClass: 'btn-danger',
                action: okButtonDelegate
            },
            cancel: {
                text: canselButtonText,
                btnClass: 'btn-info'
            },

        }
    });
}
