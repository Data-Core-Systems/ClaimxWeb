/* function to be used for ajax call */
/* if need to send synch call - do not pass callbackfunction parameter */
function ajaxCall(url, paramnames, paramvalues, callbackfunction, callbackparameter, isShowLoader) {
    var ajaxObj = createXMLHttpRequest();
    var paramString = "";
    var x = "";
    var asynch = false;
    paramString = prepareParamString(paramString, "ajax", "true");

    if (paramnames != "undefined") {
        for (x in paramnames) {
            paramString = prepareParamString(paramString, paramnames[x], paramvalues[x]);
        }
    }
    if (callbackfunction != null && callbackfunction != "undefined") {
        ajaxObj.onreadystatechange = function () { xmlHttpStatChange(ajaxObj, callbackfunction, callbackparameter); }
        asynch = true;

        if (typeof isShowLoader != 'undefined' && isShowLoader == false)
            ShowLoader(false);
        else
            ShowLoader(true);
    }
    ajaxObj.open("post", url, asynch);
    ajaxObj.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    ajaxObj.send(paramString);
    if (!asynch) {
        return verifyReturnData(ajaxObj.responseText);
    }
    else
        return "";
}

/* Returns the output from ajax call */
function xmlHttpReturnVal(ajaxObj) {
    if (ajaxObj.readyState == 4) {
        {
            if (typeof (IsQueryPageLoad) == 'undefined' || IsQueryPageLoad == false)
                ShowLoader(false);
            return verifyReturnData(ajaxObj.responseText);
        }
    }
    else
        return "";
}

function verifyReturnData(data) {
    if (data == "SessionExpired") {
        window.location.href = "/SessionExpired.aspx";
        data = "";
    }
    else if (data == "500Error") {
        RedirectURL("/500ErrorHandler.aspx");
        data = "";
    }
    return data;
}

/* Called when statechange event fires */
function xmlHttpStatChange(ajaxObj, callbackfunction, param) {
    if (callbackfunction != null && ajaxObj.readyState == 4)
        callbackfunction(ajaxObj.readyState == 4, xmlHttpReturnVal(ajaxObj), param);
}


/* Creates Ajax Obj */
function createXMLHttpRequest() {
    var ajaxObj = null;

    try {
        if (navigator.appName == "Netscape")
            ajaxObj = new XMLHttpRequest();
        else
            ajaxObj = new ActiveXObject("Microsoft.XMLHTTP")
    }
    catch (e) {

        try {
            ajaxObj = new ActiveXObject("Msxml2.XMLHTTP");
        }
        catch (e) {
        }
    }
    return ajaxObj;
}

/* Prepares parameter string */
function escapeme(val) {
    val = escape(val);
    val = val.replace(
			new RegExp("\\+", "g"),
				"%2B"
			)
    return val;
}

function prepareParamString(str, param, value) {
    if (!str == "")
        str = str + "&";
    str = str + escapeme(param) + "=" + escapeme(value);
    return str;
}


function ShowLoader(val)
{}

