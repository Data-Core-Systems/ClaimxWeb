//Creating Zone for a field
function DrawRect(ctrlZoneProp) {
    var txtBoxZone = new Object();
    eval('txtBoxZone = ' + ctrlZoneProp + ';');
    var background = document.getElementById('Image2');
    context1.fillStyle = "rgba(255, 255, 255)";
    context1.globalAlpha = 0.2;
    context1.clearRect(0, 0, 1800, 2000);
    context1.fillRect(txtBoxZone.ZoneX, txtBoxZone.ZoneY, txtBoxZone.ZoneW, txtBoxZone.ZoneH);
    //    $("div#dvImg").scrollTop(100);
    //    $("div#dvImg").scrollLeft(200);

}
//Checking alpha numeric
function isAlphabetNumeric(evt) {
    var iKeyCode = (evt.which) ? evt.which : evt.keyCode

    if ((iKeyCode > 64 && iKeyCode < 91) || (iKeyCode > 47 && iKeyCode < 59) || (iKeyCode > 96 && iKeyCode < 123) || iKeyCode == 8 || iKeyCode == 9 || iKeyCode == 37 || iKeyCode == 32 || iKeyCode == 46)
        return true;
    else if (iKeyCode == 92 || iKeyCode == 46 || iKeyCode == 45 || iKeyCode == 47 || iKeyCode == 43 || iKeyCode == 33 || iKeyCode == 36 || iKeyCode == 37 || iKeyCode == 38 || iKeyCode == 42 || iKeyCode == 40 || iKeyCode == 37 || iKeyCode == 41 || iKeyCode == 44 || iKeyCode == 58 || iKeyCode == 59 || iKeyCode == 34 || iKeyCode == 39 || iKeyCode == 96 || iKeyCode == 63 || iKeyCode == 94 || iKeyCode == 126)
        return false;
}
function isNumber(evt) {
    var iKeyCode = (evt.which) ? evt.which : evt.keyCode
    if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57) && (iKeyCode != 45))
        return false;
    return true;
}
function isAlpha(evt) {
    var iKeyCode = (evt.which) ? evt.which : evt.keyCode

    if ((iKeyCode > 64 && iKeyCode < 91) || (iKeyCode > 96 && iKeyCode < 123) || iKeyCode == 8 || iKeyCode == 9 || iKeyCode == 46 || iKeyCode == 32) {
        return true;

    }
    else {
        return false;

    }
}

//Checking key press event
function KeyPress(e, type) {
    if (type == 'A') {
        return isAlpha(e);
    }
    else if (type == 'AN') {
        return isAlphabetNumeric(e);
    }
    else if (type == 'N') {
        return isNumber(e);
    }
}

//Jump to next field if max length fulfils
function Validate(e) {
    var element = $(e.target);
    var tabIndex = element.attr('tabIndex');
    var input = element.val();
    var maxLength = element.attr("maxLength");
    var inputLength = input.length;
    var nextElement = $("input[tabIndex=" + (parseInt(tabIndex) + 1) + "]");
    var box_count = $('input[name="txtBox"]').length;
    if (inputLength == maxLength) {
        var status = DoValidate(e);
        if (status == true) {
            if (box_count > tabIndex) {
                nextElement.focus();
            }
            else {
                LoadNext();
            }
        }
    }
    else {
        if (e.keyCode == 13) {
            var status = DoValidate(e);
            if (status == true) {
                if (box_count > tabIndex) {
                    nextElement.focus();
                }
                else {
                    LoadNext();
                }
            }
        }
        else if (e.keyCode == 34)
        {
            LoadNext();
        }
        else if (e.keyCode == 33)
        {
            LoadPrevious();
        }
    }

    return false; //Otherwise the form will be submitted
}
//Do validate
function DoValidate(e) {

    var element = $(e.target);
    var status = CheckRequired(element);
    if (status == false) {
        return false;
    }
    else {
        status = CheckMinimumLength(element);
        if (status == false) {
            return false;
        }
        else {
            status = CheckSchema(element);
            if (status == false) {
                return false;
            }
        }
    }
    return true;

}

//Check required field
function CheckRequired(element) {
    var val_req = element.attr("Essential");
    if (val_req == "YES") {
        if (element.val() == '') {
            alert(element.attr("fieldname") + " is a required field");
            element.focus();
            return false;
        }
    }

    return true;

}

//Check minimum field
function CheckMinimumLength(element) {

    var minlength = element.attr("minLength");
    var input = element.val().length;
    if (input < minlength) {
        alert("Minimum length is " + minlength);
        element.focus();
        return false;
    }
    else {
        return true;
    }

}
//Check Schema
function CheckSchema(element) {
    var status = false;
    var schema = element.attr("schema");
    var input = element.val().length;
    if (input > 0 && schema != "") {
        var arr = schema.split("/");
        for (var index = 0; index < parseInt(arr.length) ; index++) {
            if (arr[index] == element.val()) {
                status = true;
                break;
            }
        }
        if (status == false) {
            alert("Value should be " + schema);
            element.focus();
            return false;
        }
        else {
            return true;
        }
    }
    else {
        return true;
    }

}