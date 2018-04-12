<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CreatePatch.aspx.cs" Inherits="IPMS.Home.CreatePatch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HtmlEditor" TagPrefix="cc1" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <style>
        .ajax__htmleditor_editor_toptoolbar {
            display: none;
        }
    </style>

    <script type="text/javascript">

        //function chechFileExtension() {
        //    var fl = document.getElementById('MainContent_docUpload');
        //    var ext = fl.value.match(/\.(.+)$/)[1];
        //    switch (ext) {
        //        case 'zip':
        //        case 'ZIP':

        //            break;
        //        default:
        //            document.getElementById("MainContent_lblPatch").innerHTML = "Please choose zip file";
        //            fl.value = '';
        //    }
        //}
        function checkValues() {
            var client = document.getElementById("MainContent_DropDownClient").value;
            var dependency = document.getElementById("MainContent_DropDownDependency").value;
            var comment = document.getElementById("MainContent_txtComments").value;
            if (client == "") {
                document.getElementById("MainContent_lblPatch").innerHTML = "Please Select Client";
                return false;
            }
            if (dependency == "") {
                document.getElementById("MainContent_lblPatch").innerHTML = "Please Select Dependency";
                return false;
            }
            if (comment == "") {
                document.getElementById("MainContent_lblPatch").innerHTML = "Please Enter Comment";
                return false;
            }

            if (comment.length > 500) {
                document.getElementById("MainContent_lblPatch").innerHTML = "Comment should not be greater than 500 charecters";
                return false;
            }

            return true;

        }
        function checkFileUpload() {
            $.ajax({
                type: "POST",
                url: "CreatePatch.aspx/CheckUploadFile",
                async: false,
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {

                    document.getElementById("MainContent_hidFile").value = data.d;
                },
                error: function (data) {
                    document.getElementById("MainContent_lblFile").innerHTML = "Oops Something Went Wrong";
                }
            });
            if (document.getElementById("MainContent_hidFile").value.toUpperCase().endsWith(".ZIP")) {
                var str = document.getElementById("MainContent_patchID").value;

                return confirm("Want to upload " + document.getElementById("MainContent_hidFile").value + " as " + str.substr(str.lastIndexOf("/") + 1));
            }
            else {
                document.getElementById("MainContent_lblFile").className = "label label-danger";
                document.getElementById("MainContent_lblFile").innerHTML = document.getElementById("MainContent_hidFile").value;
                return false;
            }

        }
    </script>
    <div class="main-box clearfix">
        <div class="main-box-header clearfix">
            <h2>Create Patch</h2>
        </div>



        <div class="main-box-body clearfix">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-4">
                    </div>
                    <div class="col-md-4" style="padding-bottom: 0.5%;">

                        <asp:DropDownList runat="server" ID="DropDownClient" CssClass="form-control" EnableViewState="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                    </div>
                    <div class="col-md-4" style="padding-bottom: 0.5%;">

                        <asp:DropDownList runat="server" ID="DropDownDependency" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-4">
                    </div>
                    <div class="col-md-4" style="padding-bottom: 0.5%;">

                        <asp:TextBox runat="server" ID="txtComments" CssClass="form-control" placeholder="Enter Comment"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                    </div>
                </div>
                <%--<div class="row">
                    <div class="col-md-4">
                    </div>
                    <div class="col-md-4" style="padding-bottom: 0.5%;">
                        <asp:FileUpload runat="server" ID="docUpload" onchange="chechFileExtension()" CssClass="form-control" ToolTip="Upload Patch" EnableViewState="true" ViewStateMode="Enabled" accept=".zip"/>
                        <asp:Label Text="" runat="server" ID="lblFile" CssClass="label label-primary" />

                    </div>
                    <div class="col-md-4">
                    </div>
                </div>--%>

                <div class="row">
                    <div class="col-md-4">
                    </div>

                    <div class="col-md-4" style="padding-bottom: 0.5%;">
                        <asp:Label ID="lblPatch" runat="server" CssClass="label label-danger"></asp:Label>

                    </div>
                    <div class="col-md-4">
                    </div>
                </div>
                <div class="row">

                    <div class="col-md-4">
                    </div>

                    <div class="col-md-4" style="padding-bottom: 0.5%;">
                        <asp:Button runat="server" ID="Submit" OnClick="SubmitMe" Text="Generate Patch Link" OnClientClick="return checkValues()" CssClass="btn btn-success btn-lg btn-block" />

                    </div>
                    <div class="col-md-4">
                    </div>
                </div>




                <div class="row">
                    <div class="col-md-4">
                    </div>

                    <div class="col-md-4" style="padding-bottom: 0.5%;">
                        <asp:Label ID="lblFile" runat="server" CssClass="label label-pill label-info"></asp:Label>

                    </div>
                    <div class="col-md-4">
                    </div>
                </div>

            </div>


            <div class="row">
                <div class="col-md-4">
                </div>
                <div class="col-md-4">

                    <asp:TextBox runat="server" ID="patchID" CssClass="form-control" Visible="false"></asp:TextBox>
                </div>
                <div class="col-md-4">
                </div>
            </div>
            <asp:Panel ID="email" runat="server" Visible="false">
                <div class="row">
                    <div class="col-md-12">
                        <div class="jumbotron">
                            <div class="row" style="padding-bottom: 0.5%">
                                <div class="col-md-1">
                                    <h6><span class="label label-default">From</span></h6>
                                </div>
                                <div class="col-md-11">
                                    <asp:TextBox ID="from" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row" style="padding-bottom: 0.5%">
                                <div class="col-md-1">
                                    <h6><span class="label label-default">To </span></h6>
                                </div>
                                <div class="col-md-11">
                                    <asp:TextBox ID="txtTo" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row" style="padding-bottom: 0.5%">
                                <div class="col-md-1">
                                    <h6><span class="label label-default">CC </span></h6>
                                </div>
                                <div class="col-md-11">
                                    <asp:TextBox ID="txtcc" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row" style="padding-bottom: 0.5%">
                                <div class="col-md-1">
                                    <h6><span class="label label-default">Subject </span></h6>
                                </div>
                                <div class="col-md-11">
                                    <asp:TextBox ID="txtSubject" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row" style="padding-bottom: 0.5%">
                                <div class="col-md-1">
                                    <h6><span class="label label-default">Message </span></h6>
                                </div>
                                <div class="col-md-11">

                                    <cc1:Editor
                                        ID="Editor1"
                                        Width="100%"
                                        Height="400px"
                                        runat="server" />
                                    <br />
                                </div>
                            </div>
                            <div class="row" style="padding-bottom: 0.5%">
                                <div class="col-md-1">
                                </div>
                                <div class="col-md-11">
                                    <asp:Button ID="sendmail" OnClick="SendEmail" OnClientClick="return checkFileUpload()" runat="server" Text="Upload Patch" CssClass="btn btn-success btn-lg " />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:HiddenField runat="server" ID="hidClient" />
            <asp:HiddenField runat="server" ID="hidCodeMonth" />
            <asp:HiddenField runat="server" ID="hidPatchNumber" />
            <asp:HiddenField runat="server" ID="hidPatchUrl" />
            <asp:HiddenField runat="server" ID="hidFile" />
            <asp:HiddenField runat="server" ID="hidDocUpload" />
            <asp:HiddenField runat="server" ID="hidPatchFolderName" />

        </div>
    </div>
</asp:Content>
