function CallWebService(strUrl, strMethod, strArgs, fnComplete, fnError) {
    $.ajax({
        type: "POST",
        url: strUrl + "/" + strMethod,
        data: JSON.stringify(strArgs),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (fnComplete !== undefined) {
                objData = JSON.parse(data.d);
                fnComplete(objData);
            }
        },
        error: function (request, status, error) {
            if (fnError !== undefined)
                fnError(request, status, error);
            else
                console.log(request,status,error)
        }
    })
}