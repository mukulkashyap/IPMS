<%@ Page Title="IPMS Home Page" Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="PatchMaintainence.Home.Home" %>

<!DOCTYPE html>
<!--[if lt IE 7]>      <html lang="en" class="no-js lt-ie9 lt-ie8 lt-ie7"> <![endif]-->
<!--[if IE 7]>         <html lang="en" class="no-js lt-ie9 lt-ie8"> <![endif]-->
<!--[if IE 8]>         <html lang="en" class="no-js lt-ie9"> <![endif]-->
<!--[if gt IE 8]><!-->
<html lang="en" class="no-js">
<!--<![endif]-->
<head>
    <!-- Mobile Specific Meta -->
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- Always force latest IE rendering engine -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <!-- Meta Keyword -->
    <meta name="keywords" content="one page, business template, single page, onepage, responsive, parallax, creative, business, html5, css3, css3 animation">
    <!-- meta character set -->
    <meta charset="utf-8">

    <!-- Site Title -->
    <title>IPMS Home Page</title>

    <!--
        Google Fonts
        ============================================= -->
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700" rel="stylesheet" type="text/css">

    <!--
        CSS
        ============================================= -->
    <!-- Fontawesome -->
    <link rel="stylesheet" href="../Content/css/font-awesome.min.css">
    <!-- Bootstrap -->
    <link rel="stylesheet" href="../Content/css/bootstrap/bootstrap.min.css">
    <!-- Fancybox -->
    <link rel="stylesheet" href="../Content/css/home/jquery.fancybox.css">
    <!-- owl carousel -->
    <link rel="stylesheet" href="../Content/css/home/owl.carousel.css">
    <!-- Animate -->
    <link rel="stylesheet" href="../Content/css/home/animate.css">
    <!-- Main Stylesheet -->
    <link rel="stylesheet" href="../Content/css/home/main.css">
    <!-- Main Responsive -->
    <link rel="stylesheet" href="../Content/css/home/responsive.css">


    <!-- Modernizer Script for old Browsers -->
    <script src="../Scripts/modernizr-2.6.2.min.js"></script>

    <script src="../Scripts/cmnScript.js"></script>
    <script src="../Scripts/cmnModalPopups.js"></script>
    <script type="text/javascript">
        function SendMessage()
        {
            debugger;
            var feedbackMessage, feedbackResponse;

            if (document.getElementById("username")) {
                if (document.getElementById("username").value.trim() == "") {
                    document.getElementById("username").innerHTML = "Please Enter Your Name";
                    return false;
                }
            }

            if (document.getElementById("useremail")) {
                if (document.getElementById("useremail").value.trim() == "") {
                    document.getElementById("useremail").innerHTML = "Please Enter Your Email Id";
                    return false;
                }
            }

            if (document.getElementById("usermessage")) {
                if (document.getElementById("usermessage").value.trim() == "") {
                    document.getElementById("usermessage").innerHTML = "Please Enter Your Message";
                    return false;
                }
            }

            $.ajax({
                type: "POST",
                url: "Home.aspx/SubmitMessage",
                async: false,
                cache:false,
                data: JSON.stringify({ userName: document.getElementById("username").value, userEmail: document.getElementById("useremail").value, userMessage: document.getElementById("usermessage").value }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    feedbackMessage = "Your response has been successfully submitted.";
                    feedbackResponse = true;
                },
                error: function (data) {
                    feedbackMessage = "Oops Something Went Wrong. Please try again";
                    feedbackResponse = false;
                }
            });
            resetValues();
            showAskMoreFeedback(feedbackMessage, feedbackResponse);
            return false;
        }

        function resetValues()
        {
            if (document.getElementById("username")) {
                    document.getElementById("username").value = "";
            }

            if (document.getElementById("useremail")) {
                document.getElementById("useremail").value = "";
            }

            if (document.getElementById("usermessage")) {
                document.getElementById("usermessage").value = "";
            }
        }
    </script>

</head>

<body>

    <!--
        Fixed Navigation
        ==================================== -->
    <header id="navigation" class="navbar-fixed-top">
        <div class="container">

            <div class="navbar-header">
                <!-- responsive nav button -->
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="sr-only"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <!-- /responsive nav button -->

                <!-- logo -->
                <h1 class="navbar-brand">
                    <a href="#body">
                        <img src="../Content/img/logo.png" alt="Kasper Logo">
                    </a>
                </h1>
                <!-- /logo -->

            </div>

            <!-- main nav -->
            <nav class="collapse navigation navbar-collapse navbar-right" role="navigation">
                <ul id="nav" class="nav navbar-nav">
                    <li class="current"><a href="#home">Home</a></li>

                    <li><a href="#aboutUs">About Us</a></li>                   
                    <li><a href="#contact">Ask More</a></li>
                     <li><a href="../Login/Login">Login</a></li>
                    <li><a href="../Login/Register">Register</a></li>                    
                </ul>
            </nav>
            <!-- /main nav -->
        </div>


    </header>
    <!--
        End Fixed Navigation
        ==================================== -->


    <!--
        Home Slider
        ==================================== -->
    <section id="home">
            <div id="home-carousel" class="carousel slide" data-ride="carousel" data-interval="false">
                <ol class="carousel-indicators">
                    <li data-target="#home-carousel" data-slide-to="0" class="active"></li>
                    <li data-target="#home-carousel" data-slide-to="1"></li>
                    <li data-target="#home-carousel" data-slide-to="2"></li>
                </ol>
                <!--/.carousel-indicators-->

                <div class="carousel-inner">

                    <div class="item active" style="background-image: url('../Content/img/slider/bg3.jpg')">
                        <div class="carousel-caption">
                            <div class="animated bounceInRight">

                                <div>


                                    <h2>IMPS
                                <br>
                                        INSZoom Patch Management System</h2>

                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="item" style="background-image: url('../Content/img/slider/bg2.jpg')">
                        <div class="carousel-caption">
                            <div class="animated bounceInDown">
                                <div>


                                    <h2>IMPS
                                <br>
                                        INSZoom Patch Management System</h2>

                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="item" style="background-image: url('../Content/img/slider/bg1.jpg')">
                        <div class="carousel-caption">
                            <div class="animated bounceInUp">
                                <div>
                                    <h2>IMPS
                                <br>
                                        INSZoom Patch Management System</h2>


                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!--/.carousel-inner-->
                <nav id="nav-arrows" class="nav-arrows hidden-xs hidden-sm visible-md visible-lg">
                    <a class="sl-prev hidden-xs" href="#home-carousel" data-slide="prev">
                        <i class="fa fa-angle-left fa-3x"></i>
                    </a>
                    <a class="sl-next" href="#home-carousel" data-slide="next">
                        <i class="fa fa-angle-right fa-3x"></i>
                    </a>
                </nav>

            </div>
        </section>
    <!--
        End #home Slider
        ========================== -->



    <!--
        #OrgSettting
        ========================== -->

    <section id="aboutUs" style="padding-bottom:25%">

        <div class="section-title text-center wow fadeInDown">
            <h2>Leadership Team</h2>
            <p> Manage Unmanaged.  </p>
        </div>

      

        <div id="projects" class="clearfix">

            <figure class="mix portfolio-item email">
                <img class="img-responsive" src="../Content/img/portfolio/portfolio-1.jpg" alt="Portfolio Item" style="width:400px;height:500px;">
                <a href="../Content/img/portfolio/portfolio-1.jpg" title="Mukul Kashyap" rel="portfolio" class="fancybox"><span class="plus"></span></a>
                <figcaption class="mask">
                    <h3>Mukul Kashyap</h3>
                    <span>Associate Team Lead</span>
                </figcaption>
            </figure>

            <figure class="mix portfolio-item photography">
                <img class="img-responsive" src="../Content/img/portfolio/portfolio-2.jpg" alt="Portfolio Item" style="width:400px;height:500px;">
                <a href="../Content/img/portfolio/portfolio-2.jpg" title="Mrityunjay Sinha" rel="portfolio" class="fancybox"><span class="plus"></span></a>
                <figcaption class="mask">
                    <h3>Mrityunjay Sinha</h3>
                    <span>Senior Software Engineer</span>
                </figcaption>
            </figure>

            <figure class="mix portfolio-item web">
                <img class="img-responsive" src="../Content/img/portfolio/portfolio-3.jpg" alt="Portfolio Item" style="width:400px;height:500px;">
                <a href="../Content/img/portfolio/portfolio-3.jpg" title="Akshay Gaonkar" rel="portfolio" class="fancybox"><span class="plus"></span></a>
                <figcaption class="mask">
                    <h3>Akshay Gaonkar</h3>
                    <span>Software Engineer</span>
                </figcaption>
            </figure>

            <figure class="mix portfolio-item print">
                <img class="img-responsive" src="../Content/img/portfolio/portfolio-4.jpg" alt="Portfolio Item" style="width:400px;height:500px;">
                <a href="../Content/img/portfolio/portfolio-4.jpg" title="Karthik S K" rel="portfolio" class="fancybox"><span class="plus"></span></a>
                <figcaption class="mask">
                    <h3>Karthik S K</h3>
                    <span>Software Engineer</span>
                </figcaption>
            </figure>

            

        </div>
        <!-- end #projects -->

    </section>
    <!--
        End #aboutUs
        ========================== -->











    <!--
        #contact
        ========================== -->
    <section id="contact">
        <div class="container">
            <div class="row">

                <div class="section-title text-center wow fadeInDown">
                    <h2>Ask More</h2>
                    <p>Connect With Us To Find More Information On This Website.</p>
                </div>

                <div class="col-md-8 col-sm-9 wow fadeInLeft">
                    <div class="contact-form clearfix">
                        <form action="" method="post">
                            <%--<asp:Label ID="lblLogin" runat="server" Font-Size="Medium" CssClass="label label-danger"></asp:Label>                            --%>
                            <div class="input-field">
                                <input type="text" class="form-control" name="name" id="username" placeholder="Your Name">
                            </div>
                            <div class="input-field">
                                <input type="email" class="form-control" name="email" id="useremail" placeholder="Your Email">
                            </div>
                            <div class="input-field message">
                                <textarea name="message" class="form-control" placeholder="Your Message" id="usermessage"></textarea>
                            </div>
                            
                            <input type="submit" class="btn btn-blue pull-right" value="SEND MESSAGE" id="msg-submit" onclick="return SendMessage();">
                        </form>
                    </div>
                    <!-- end .contact-form -->
                </div>
                <!-- .col-md-8 -->

             
                <!-- .col-md-4 -->

            </div>
        </div>
    </section>
    <!--
        End #contact
        ========================== -->

    <!--
        #footer
        ========================== -->
    <footer id="footer" class="text-center">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">

                        <div class="footer-logo wow fadeInDown">
                            <img src="../Content/img/logo.png" alt="logo">
                        </div>

                        <div class="footer-social wow fadeInUp">
                            <h3>We are everywhere</h3>
                            <ul class="text-center list-inline">
                                <li><a href="https://www.facebook.com/INSZoom/"><i class="fa fa-facebook fa-lg"></i></a></li>
                                <li><a href="https://twitter.com/inszoom"><i class="fa fa-twitter fa-lg"></i></a></li>
                                <li><a href="https://www.linkedin.com/company/inszoom"><i class="fa  fa-linkedin fa-lg"></i></a></li>
                                <li><a href="https://www.youtube.com/user/INSZoomInc"><i class="fa fa-youtube fa-lg"></i></a></li>
                            </ul>
                        </div>



                    </div>
                </div>
            </div>
        </footer>
    <!--
        End #footer
        ========================== -->


    <!--
        JavaScripts
        ========================== -->

    <!-- main jQuery -->
     <!-- main jQuery -->
        <script src="../Scripts/jquery-1.11.1.min.js"></script>
        <!-- Bootstrap -->
        <script src="../Scripts/bootstrap.min.js"></script>
        <!-- jquery.nav -->
        <script src="../Scripts/jquery.nav.js"></script>
        <!-- Portfolio Filtering -->
        <script src="../Scripts/jquery.mixitup.min.js"></script>
        <!-- Fancybox -->
        <script src="../Scripts/jquery.fancybox.pack.js"></script>
        <!-- Parallax sections -->
        <script src="../Scripts/jquery.parallax-1.1.3.js"></script>
        <!-- jQuery Appear -->
        <script src="../Scripts/jquery.appear.js"></script>
        <!-- countTo -->
        <script src="../Scripts/jquery-countTo.js"></script>
        <!-- owl carousel -->
        <script src="../Scripts/owl.carousel.min.js"></script>
        <!-- WOW script -->
        <script src="../Scripts/wow.min.js"></script>
        <!-- theme custom scripts -->
        <script src="../Scripts/main.js"></script>
</body>
</html>

