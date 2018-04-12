<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="UserDetail.aspx.cs" Inherits="IPMS.Home.UserDetail" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HtmlEditor" TagPrefix="cc1" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <script>


        function chechFileExtension() {
            var fl = document.getElementById('MainContent_imageUpload');
            var ext = fl.value.match(/\.(.+)$/)[1];
            switch (ext) {
                case 'jpg':
                case 'JPG':
                case 'jpeg':
                case 'JPEG':
                case 'png':
                case 'PNG':
                    break;
                default:
                    document.getElementById("MainContent_lblImage").innerHTML = "Please choose jpg,png or jpeg";
                    fl.value = '';
            }
        }
        function checkSave() {
            if (document.getElementById("MainContent_imageUpload")) {
                if (document.getElementById("MainContent_imageUpload").value == "") {
                    document.getElementById("MainContent_lblImage").innerHTML = "Please choose file";
                    return false;
                }
            }
        }
        function ShowProfileEdit() {
            $('#pnlProfileView').hide();
            $('#pnlProfileEdit').show();
            // document.getElementById("pnlProfileView").style.visibility = "hidden";
            // document.getElementById("pnlProfileEdit").style.visibility = "visible";
        }

        function HideProfileEdit() {
            $('#pnlProfileView').show();
            $('#pnlProfileEdit').hide();
            // document.getElementById("pnlProfileView").style.display = "block";
            // document.getElementById("pnlProfileEdit").style.display = "none";
        }
        function SaveProfileData() {
            if (document.getElementById("MainContent_inputName").value == "") {
                document.getElementById("nameDiv").className = "form-group has-error";
                document.getElementById("userName").style.visibility = 'visible';
                return false;
            }
            $('#pnlProfileEdit').hide();
            $('#pnlProfileView').show();
            document.getElementById("pnlProfileView").style.visibility = "visible";
            document.getElementById("pnlProfileEdit").style.visibility = "hidden";
            document.getElementById("hidSaveData").value = 'Profile';
        }

        function SavePasswordData() {
            if ((document.getElementById("MainContent_inputPassword").value.length < 8)) {
                document.getElementById("passwordDiv").className = "form-group has-error";
                document.getElementById("userPswd").style.visibility = 'visible';
                return false;
            }
            if (document.getElementById("MainContent_inputPassword").value != document.getElementById("MainContent_inputConfirmPassword").value) {
                document.getElementById("confirmDiv").className = "form-group has-error";
                document.getElementById("userConfirm").style.visibility = 'visible';
                return false;
            }

            $('#pnlPasswordEdit').show();
            document.getElementById("hidSaveData").value = 'Password';
        }

    </script>
    <input type="hidden" name="hidSaveData" id="hidSaveData" value="" />
    <div class="row" id="user-profile">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <a id="modal-603227" href="#modal-container-603227" role="button" class="btn" data-toggle="modal" style="display: none;"></a>
                    <div class="modal fade" id="modal-container-603227" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">

                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                        ×
                                    </button>
                                    <h4 class="modal-title" id="myModalLabel">Upload Image
                                    </h4>
                                </div>
                                <div class="modal-body">
                                    <asp:FileUpload ID="imageUpload" runat="server" placeholder = "Please choose jpg, png or jpeg files" onchange="chechFileExtension()" accept=".png,.jpg,.jpeg,.gif" />
                                </div>
                                <div class="modal-footer">
                                    <asp:Label runat="server" ID="lblImage" CssClass="label label-danger"></asp:Label>
                                    <asp:Button runat="server" CssClass="btn btn-success" type="button" OnClick="SaveImage" OnClientClick="return checkSave()" Text="Save" class="btn btn-primary" />
                                    <button type="button" class="btn btn-success" data-dismiss="modal">
                                        Close
                                    </button>
                                    <asp:Button runat="server" CssClass="btn btn-success" type="button" OnClick="RemoveImage" Text="Remove Image" class="btn btn-primary" />
                                </div>
                            </div>

                        </div>

                    </div>

                </div>
            </div>
        </div>
        <div class="col-lg-3 col-md-4 col-sm-4">
            <div class="main-box clearfix">
                <div class="main-box-body clearfix">
                    <input type="image" src="<%=GetImageURl() %>" alt="" onclick="$('#modal-603227').click(); return false;" class="profile-img img-responsive center-block">
                    <div class="profile-since">
                        <asp:Literal ID="ltlName" runat="server"></asp:Literal>
                    </div>
                    <div class="profile-details clearfix" style="word-wrap:break-word;" >
                        <ul class="fa-ul">
                            <li><i class="fa-li fa fa-envelope"></i>Email ID: <span>
                                <asp:Literal ID="ltlEmail"  runat="server"></asp:Literal></span>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-lg-6 col-md-8 col-sm-8">
            <div class="main-box clearfix">
                <div class="tabs-wrapper profile-tabs">

                    <ul class="nav nav-tabs">
                        <li class="active"><a href="#tab-profile" data-toggle="tab">Profile</a></li>
                        <li><a href="#tab-password" data-toggle="tab">Password</a></li>
                    </ul>

                    <div class="tab-content">

                        <div class="tab-pane fade in active" id="tab-profile">
                            <div id="userProfile">

                                <div id="pnlProfileView" style="visibility: visible">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="main-box">
                                                <header class="main-box-header clearfix">
                                                    <h2>Personal Info</h2>
                                                </header>

                                                <div class="main-box-body clearfix">
                                                    <div class="form-horizontal" role="form">
                                                        <div class="form-group">
                                                            <div class="col-lg-2 control-label"><b>Name</b></div>
                                                            <div class="col-lg-10">
                                                                <div class="details" style="padding-top: 6px">
                                                                    <asp:Literal ID="ltlName1" runat="server"></asp:Literal>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-lg-2 control-label"><b>Signature</b></div>
                                                            <div id="emailbody" style="padding-top: 6px" class="col-lg-10 details">
                                                                <asp:Literal ID="ltlSignature" runat="server"></asp:Literal>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-lg-offset-10 col-lg-9">
                                                                <button type="button" onclick="ShowProfileEdit();" class="btn btn-success">Update</button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div id="pnlProfileEdit" style="display: none">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="main-box">
                                                <header class="main-box-header clearfix">
                                                    <h2>Personal Info</h2>
                                                </header>
                                                <div class="main-box-body clearfix">
                                                    <div class="form-horizontal" role="form">
                                                        <div class="form-group">
                                                            <label for="inputName" class="col-lg-2 control-label">Name</label>
                                                            <div class="col-lg-9" id="nameDiv">
                                                                <asp:TextBox runat="server" class="form-control" name="inputName" ID="inputName" placeholder="Name" />
                                                                <div id="userName" style="visibility: hidden; padding-left: 20%">
                                                                    <span class="help-block"><i class="icon-remove-sign"></i>Enter user name</span>
                                                                </div>
                                                            </div>
                                                        </div>



                                                        <div class="container-fluid">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <a id="modal-603228" href="#modal-container-603228" role="button" class="btn" data-toggle="modal" style="display:none"></a>
                                                                    <div class="modal fade" id="modal-container-603228" role="dialog" aria-labelledby="myModalLabelforRefer" aria-hidden="true">
                                                                        <div class="modal-dialog">
                                                                            <div class="modal-content">
                                                                                <div class="modal-header">

                                                                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                                                                        ×
                                                                                    </button>
                                                                                    <h4 class="modal-title" id="myModalLabelforRefer">Refer Keywords
                                                                                    </h4>
                                                                                </div>
                                                                                <div class="modal-body">
                                                                                   <span style="color:#03a9f4;"><b> Use below keywords to be replaced with actual value while composing email.</b></span><br /><br />
                                                                                    Complete Patch Url - [[patchUrl]]<br />
                                                                                    Task Name - [[taskName]] <br />
                                                                                    Dependency - [[dependency]]<br />
                                                                                    Client Name - [[clientName]]<br />
                                                                                    Your Name - [[name]]
                                                                                </div>
                                                                                <div class="modal-footer">

                                                                                    <button type="button" class="btn btn-success" data-dismiss="modal">
                                                                                        Close
                                                                                    </button>

                                                                                </div>
                                                                            </div>

                                                                        </div>

                                                                    </div>

                                                                </div>
                                                            </div>
                                                        </div>

                                                        

                                                        <div class="form-group">
                                                            <label for="inputSignature" class="col-lg-2 control-label">Signature</label>
                                                            <div class="col-lg-9">
                                                                <cc1:Editor
                                                                    ID="Editor1"
                                                                    Width="100%"
                                                                    Height="400px"
                                                                    runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-lg-offset-7 col-lg-8">
                                                                <asp:Button runat="server" OnClientClick="$('#modal-603228').click();return false;" Text="Refer Keyword" CssClass="btn btn-success" />
                                                                <asp:Button runat="server" OnClientClick="return SaveProfileData();" Text="Save" CssClass="btn btn-success" />
                                                                <button type="button" onclick="HideProfileEdit();" class="btn btn-success">Cancel</button>
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

                        <div class="tab-pane fade" id="tab-password">
                            <div id="userPassword">

                                <div id="pnlPasswordEdit" style="visibility: visible">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="main-box">
                                                <header class="main-box-header clearfix">
                                                    <h2>User Password</h2>
                                                </header>
                                                <div class="main-box-body clearfix">
                                                    <div class="form-horizontal" role="form">
                                                        <div class="form-group">
                                                            <label for="inputPassword" class="col-lg-2 control-label">New Password</label>
                                                            <div class="col-lg-6" id="passwordDiv">
                                                                <asp:TextBox runat="server" type="password" CssClass="form-control" name="inputPassword" ID="inputPassword" placeholder="New Password" />
                                                                <%--<input type="password" class="form-control" name="inputPassword" id="inputPassword" placeholder="New Password">--%>
                                                                <div id="userPswd" style="visibility: hidden; padding-left: 20%">
                                                                    <span class="help-block"><i class="icon-remove-sign"></i>Password length should be minimum 8 characters</span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label for="inputConfirmPassword" class="col-lg-2 control-label">Confirm Password</label>
                                                            <div class="col-lg-6" id="confirmDiv">
                                                                <asp:TextBox runat="server" type="password" CssClass="form-control" name="inputConfirmPassword" ID="inputConfirmPassword" placeholder="Confirm Password" />
                                                                <%--<input type="password" class="form-control" name="inputConfirmPassword" id="inputConfirmPassword" placeholder="Confirm Password">--%>
                                                            </div>
                                                            <div id="userConfirm" style="visibility: hidden; padding-left: 20%">
                                                                <span class="help-block"><i class="icon-remove-sign"></i>Confirm password didn't match</span>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-lg-offset-10 col-lg-6">
                                                                <asp:Button runat="server" OnClientClick="return SavePasswordData();" Text="Save" CssClass="btn btn-success" />
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

                    </div>
                </div>
            </div>
        </div>

    </div>

</asp:Content>

