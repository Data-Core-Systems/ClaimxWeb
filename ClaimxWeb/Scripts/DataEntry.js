$(document).ready(function () {
    LoadFirst();
    $("input[tabIndex=1]").focus();
    
});
function LoadImagePath() {
    var p = ['Action'];
    var v = ['LoadImage'];
    var imageText = ajaxCall("DataEntry.aspx", p, v, null, null);
    LoadImage(imageText);

}
function LoadNext() {
    
    //alert(document.getElementById("dvDataWindow").getElementsByTagName("input").length);
    var $temp = '[';
    var data = document.getElementById("dvDataWindow").getElementsByTagName("input");
    
    $(data).each(function (index, item) {
        //alert(item);

        $temp = $temp.concat(" { ");
        $temp = $temp.concat("ct03_field_name:" + "'" + ($(item).attr("fieldname")) + "'");
        $temp = $temp.concat(" , ");
        $temp = $temp.concat("ct03_field_data:" + "'" + ($(item).attr("value")) + "'");
        $temp = $temp.concat(" , "); 
        $temp = $temp.concat("ct03_form_field_id:" + "'" + ($(item).attr("fieldid")) + "'");
        $temp = $temp.concat(" } , ");
    });
   
    $temp= $temp.substring(0,$temp.length -2).concat(']');

    //$temp = $temp.concat(" ] ");
    alert($temp);
    



    var p = ['Action', 'Value'];
    var v = ['LoadNextImage', $temp];
    var text = ajaxCall("DataEntry.aspx", p, v, null, null);
    $('div#dvMain').html(text);
    LoadImagePath();
    LoadImage(text);
    $("input[tabIndex=1]").focus();

}
function LoadFirst() {

   
    var p = ['Action'];
    var v = ['LoadFirstImage'];
    var text = ajaxCall("DataEntry.aspx", p, v, null, null);
    $('div#dvMain').html(text);
    LoadImagePath();
    LoadImage(text);

}
function LoadPrevious() {
    var p = ['Action'];
    var v = ['LoadPreviousImage'];
    var text = ajaxCall("DataEntry.aspx", p, v, null, null);
    $('div#dvMain').html(text);
    LoadImagePath();
    LoadImage(text);
    $("input[tabIndex=1]").focus();
   

}

function LoadImage(imagePath) {
    var canvas = document.querySelector("#layer1");
    var context = canvas.getContext("2d");
    var background = new Image();
    background.src = imagePath;  //images/1431810100001.jpeg "../202769/123456090101/61985001.jpeg"
    background.onload = function () {
    context.drawImage(background, 0, 0);
    }
}
