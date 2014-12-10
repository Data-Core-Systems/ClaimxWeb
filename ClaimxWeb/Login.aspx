<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ClaimxWeb.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .Login {
            position: absolute;
            width: 500px;
            height: 200px;
            z-index: 15;
            top: 50%;
            left: 50%;
            margin: -100px 0 0 -150px;
            background: rgb(62, 106, 245);
            align-content: center;
        }

        .JobSelect {
            position: absolute;
            width: 500px;
            height: 200px;
            z-index: 15;
            top: 50%;
            left: 50%;
            margin: -100px 0 0 -150px;
            background: rgb(62, 106, 245);
            align-content: center;
            display:none;
        }
    </style>
    <script src="Scripts/jquery_1.8.3_.min.js"></script>
    <script src="Scripts/AjaxCall.js"></script>
    <script>

       
        function Login() {

            $.ajax({
                type: "POST",
                url: "Login.aspx/doLogin",
                data: '{ username: "' + $('#UID').val() + '", password: "' + $('#PWD').val() + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    
                    if (response.d != 'fail') {

                        var JobDetails = JSON.parse(response.d);
                        
                        document.getElementById('DivLogin').style.display = "none";
                        document.getElementById('DivJobSelect').style.display = "block";

                        $.each(JobDetails, function (item,data) {
                            
                            $('#Jobs')
                                .append($("<option></option>")
                                .attr("value", data.CL01_JOB_NO)
                                .text(data.CL01_JOB_DESC + '(' + data.CL01_JOB_NO + ')'));
                           
                            
                        });
                        
                    }
                    else {
                        $('#ErrorMsg').text('Invalid User ID or Password');
                    }
                },
                failure: function (response) {
                    $('#ErrorMsg').text('Problem in Connection');
                }
            }
                );

        }
        function JobSelect()
        {
            alert($('#Jobs option:selected').val());
            $.ajax({
                type: "POST",
                url: "Login.aspx/JobSelect",
                data: '{ jobid: "' + $('#Jobs option:selected').val() + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d != 'fail') {
                        //window.open("DataEntry.aspx", "current"); 
                        ajaxCall("DataEntry.aspx", "", "", "", "", false);

                    }
                },
                failure: function (response) { }
            });
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="Login" id="DivLogin">
            <div>
                <table style="width: 100%; vertical-align: central; padding: 65px 10px 10px 10px">
                    <tr>
                        <td>User Name:
                        </td>
                        <td>
                            <input type="text" id="UID" />

                        </td>
                    </tr>
                    <tr>
                        <td>Password:
                        </td>
                        <td>
                            <input type="password" id="PWD" />

                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="align-content: space-around">
                            <input type="button" value="Clear" />
                            <input type="button" value="Login" onclick="Login()" />

                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <label id="ErrorMsg" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
         <div class="JobSelect" id="DivJobSelect">
            <div>
                <table style="width: 100%; vertical-align: central; padding: 65px 10px 10px 10px">
                    <tr>
                        <td align="left">Select Job:
                        </td>
                       
                    </tr>
                    <tr>
                        <td align="right"><select id="Jobs"></select>
                        </td>
                        
                    </tr>
                    <tr>
                        
                        <td align="Right">
                           
                            <input type="button" value="Select" onclick="JobSelect()" />

                        </td>
                    </tr>
                   
                </table>
            </div>
        </div>
    </form>
</body>
</html>
