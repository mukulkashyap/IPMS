<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="IPMS.Login.Register" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title>Register</title>

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
    </style>
    <script type="text/javascript">

       
        function checkRegister() {

            if (document.getElementById("registerName")) {
                if (document.getElementById("registerName").value.trim() == "") {
                    document.getElementById("lblRegister").innerHTML = "Please Enter Name To Register";
                    return false;
                }

                if(document.getElementById("registerName").value.substr(0, 1).match(/[A-Za-z]/) ==null)
                {
                    document.getElementById("lblRegister").innerHTML = "Name should start from alphabet";
                    return false;
                }
            }

            if (document.getElementById("registerEmail")) {
                if (document.getElementById("registerEmail").value.trim() == "") {
                    document.getElementById("lblRegister").innerHTML = "Please Enter Email To Register";
                    return false;
                }
                var re = /^\s*[\w\-\+_]+(\.[\w\-\+_]+)*\@[\w\-\+_]+\.[\w\-\+_]+(\.[\w\-\+_]+)*\s*$/;
                var email = document.getElementById("registerEmail").value.trim();
                if (re.test(email)) {
                    if (email.indexOf('@inszoom.com', email.length - '@inszoom.com'.length) !== -1) {
                        
                    } else {
                        document.getElementById("lblRegister").innerHTML = "Use Only inszoom.com domain(name@inszoom.com).";
                        return false;
                    }
                } else {
                    document.getElementById("lblRegister").innerHTML = "Not a valid e-mail address.";
                    return false;
                }
            }

            if (document.getElementById("registerPassword")) {
                if (document.getElementById("registerPassword").value.trim() == "") {
                    document.getElementById("lblRegister").innerHTML = "Please Enter Password To Register";
                    return false;
                }

                if (document.getElementById("registerPassword").value.trim().length < 8) {
                    document.getElementById("lblRegister").innerHTML = "Password length should be greater than 8 charecters";
                    return false;
                }

            }

            if (document.getElementById("registerPassword") && document.getElementById("registerPassword2")) {
                if (document.getElementById("registerPassword").value.trim() != document.getElementById("registerPassword2").value.trim()) {
                    document.getElementById("lblRegister").innerHTML = "Both Password Didnt Match";
                    return false;
                }
            }
            

            $.ajax({
                type: "POST",
                url: "Register.aspx/checkEmailExists",
                async: false,
                cache: false,
                data: JSON.stringify({ email: document.getElementById("registerEmail").value }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    document.getElementById("hidRegister").value = data.d;
                    if (data.d == true) {
                        document.getElementById("lblRegister").style.visibility = true;
                        document.getElementById("lblRegister").innerHTML = "This Email is already taken. Please try Frgot Password link";

                    }

                },
                error: function (data) {
                    document.getElementById("lblRegister").style.visibility = true;
                    document.getElementById("lblRegister").innerHTML = "Oops Something Went Wrong. Please try again";
                    document.getElementById("hidRegister").value = "false";
                }
            });
            var result = document.getElementById("hidRegister").value;
            if (result == "true") {
                return false;
            }
            else {
                return true;
            }


        }
    </script>

</head>
<body id="login-page-full">
    <form runat="server" id="frmRegister">
        <div id="login-full-wrapper">
            <div class="container">
                <div class="row">
                    <div class="col-xs-12">
                        <div id="login-box">
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
                                            <input class="form-control" runat="server" id="registerName" type="text" placeholder="Full name">
                                        </div>
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="fa fa-envelope"></i></span>
                                            <input class="form-control" type="email" id="registerEmail" runat="server" placeholder="Email address(name@inszoom.com)">
                                        </div>
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="fa fa-lock"></i></span>
                                            <input type="password" class="form-control" id="registerPassword" runat="server" placeholder="Enter password">
                                        </div>
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="fa fa-unlock-alt"></i></span>
                                            <input type="password" class="form-control" id="registerPassword2" runat="server" placeholder="Re-enter password">
                                        </div>
                                        <div id="remember-me-wrapper">
                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <div class="checkbox-nice">
                                                        <input type="checkbox" id="terms-cond" checked="checked" />
                                                        <label for="terms-cond">
                                                            I accept terms and conditions
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <asp:HiddenField ID="hidRegister" runat="server" />
                                                <asp:Label ID="lblRegister" runat="server" CssClass="label label-danger"></asp:Label>
                                                <asp:Button runat="server" class="btn btn-success col-xs-12" OnClick="RegisterMe" OnClientClick="return checkRegister()" Text="Register" ID="user_register" />
                                                
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <script src="../Scripts/js/demo-skin-changer.js"></script>
        <script src="../Scripts/jquery-1.11.1.min.js"></script>
        <script src="../Scripts/bootstrap.min.js"></script>
        <script src="../Scripts/js/jquery.nanoscroller.min.js"></script>
        <script src="../Scripts/js/demo.js"></script>
        <script src="../Scripts/js/scripts.js"></script>
    </form>
</body>
</html>
