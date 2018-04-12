<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="IPMS.Login.Login" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title>Login</title>

    <link rel="stylesheet" type="text/css" href="../Content/css/bootstrap/bootstrap.min.css" />




    <link rel="stylesheet" type="text/css" href="../Content/css/libs/font-awesome.css" />
    <link rel="stylesheet" type="text/css" href="../Content/css/libs/nanoscroller.css" />

    <link rel="stylesheet" type="text/css" href="../Content/css/compiled/theme_styles.css" />


    <link href='//fonts.googleapis.com/css?family=Open+Sans:400,600,700,300|Titillium+Web:200,300,400' rel='stylesheet' type='text/css'>

    <link type="image/x-icon" href="favicon.png" rel="shortcut icon" />
    <style>
        #login-full-wrapper {
            background: url(../Content/img/login-img.jpg) no-repeat center center;
            background-size: cover;
            width: 100%;
            height: 100%;
            position: absolute;
        }

        #divVerify {
            position: fixed;
            width: 100%;
            bottom: 15%;
            text-align: center;
            font-size: 40px;
            color: aliceblue;
            text-shadow: rgb(3, 3, 3) 0px 2px 50px;
            font-family: Calibri;
            text-align: center;
            background-color:blue;
        }
    </style>
    <script type="text/javascript">
        function checkLogin() {

            if (document.getElementById("loginEmail")) {
                if (document.getElementById("loginEmail").value.trim() == "") {
                    document.getElementById("lblLogin").innerHTML = "Please Enter Email Id To Login";
                    return false;
                }
            }

            if (document.getElementById("loginPassword")) {
                if (document.getElementById("loginPassword").value.trim() == "") {
                    document.getElementById("lblLogin").innerHTML = "Please Enter Password To Login";
                    return false;
                }
            }

            $.ajax({
                type: "POST",
                url: "Login.aspx/SubmitMe",
                async: false,
                cache: false,
                data: JSON.stringify({ userEmail: document.getElementById("loginEmail").value, password: document.getElementById("loginPassword").value }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    document.getElementById("hidLogin").value = data.d;
                    if (data.d == false) {
                        document.getElementById("lblLogin").style.visibility = true;
                        document.getElementById("lblLogin").innerHTML = "Email and Password didn't matched.";

                    }

                },
                error: function (data) {

                    document.getElementById("lblLogin").style.visibility = true;
                    document.getElementById("lblLogin").innerHTML = "Oops Something Went Wrong. Please try again";
                    document.getElementById("hidLogin").value = "false";
                }
            });
            var result = document.getElementById("hidLogin").value;
            if (result == "false") {
                return false;
            }
            else {
                return true;
            }
        }

        function ForgotPasswordPopUp() {
            
            var strAlertMsg = "Enter the registered email address"
            openForgotPasswordPopUp(strAlertMsg);
        }

        function funcSendForgotPasswordMail() {
            debugger;
            var objComment = ModalPopups.GetCustomControl("useremail");
            if (objComment.value.length < 1) {
                alert("Please enter valid email address.")
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "Login.aspx/SendForgotPassowrdMail",
                    async: false,
                    cache: false,
                    data: JSON.stringify({ userEmail: objComment.value }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        document.getElementById("hidLogin").value = data.d;
                        if (data.d == false) {
                            document.getElementById("lblUserMsg").style.visibility = true;
                            document.getElementById("lblUserMsg").innerHTML = "This email is not registered";

                        }

                    },
                    error: function (data) {

                        document.getElementById("lblUserMsg").style.visibility = true;
                        document.getElementById("lblUserMsg").innerHTML = "Oops Something Went Wrong. Please try again";
                        document.getElementById("hidLogin").value = "false";
                    }
                });
                var result = document.getElementById("hidLogin").value;
                if (result == "false") {
                    return false;
                }
                else {
                    funCancel();
                    ShowConfirmationMessage();
                }

            }
        }
    </script>

</head>
<body id="login-page-full">
    <form id="login" runat="server">
        <div id="login-full-wrapper">
            <div class="container">
                <div class="row">                    
                    <div class="col-xs-12">
                        <div id="login-box">
                            <div id="login-box-holder">
                                <div class="row">
                                    <div class="col-xs-12">
                                             
                                        <header id="login-header">
                                            <div id="login-logo">
                                               <a href="../Home/Home.aspx"> <img src="../Content/img/logo.png" alt="" /></a>
                                            </div>
                                        </header>
                                        <div id="login-box-inner">

                                            <div class="input-group">
                                                <span class="input-group-addon"><i class="fa fa-user"></i></span>
                                                <input class="form-control" runat="server" id="loginEmail" type="text" placeholder="Email address">
                                            </div>
                                            <div class="input-group">
                                                <span class="input-group-addon"><i class="fa fa-key"></i></span>
                                                <input type="password" class="form-control" runat="server" id="loginPassword" placeholder="Password">
                                            </div>
                                            <asp:Label ID="lblLogin" runat="server" CssClass="label label-danger"></asp:Label>
                                            <asp:HiddenField runat="server" ID="hidLogin" />


                                            <div id="remember-me-wrapper">
                                                <div class="row">

                                                    <a href="javascript:ForgotPasswordPopUp()" id="login-forget-link" class="col-xs-6">Forgot password?
                                                    </a>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <asp:Button runat="server" ID="Submit" Text="Log in" OnClientClick="return checkLogin()" CssClass="btn btn-success  col-xs-12" />

                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="login-box-footer">
                                <div class="row">
                                    <div class="col-xs-12">
                                        Do not have an account?
                                    <a href="Register.aspx">Register now
                                    </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="divVerify" runat="server" visible="false">We Have Emailed You The Verification Link.</div>

        <asp:HiddenField ID="hidUserStatus" runat="server" />
        <script src="../Scripts/js/demo-skin-changer.js"></script>
        <script src="../Scripts/jquery-1.11.1.min.js"></script>
        <script src="../Scripts/bootstrap.min.js"></script>
        <script src="../Scripts/js/jquery.nanoscroller.min.js"></script>
        <script src="../Scripts/js/demo.js"></script>
        <script src="../Scripts/js/scripts.js"></script>
        <script src="../Scripts/cmnModalPopups.js"></script>
        <script src="../Scripts/cmnScript.js"></script>
    </form>
</body>
</html>
