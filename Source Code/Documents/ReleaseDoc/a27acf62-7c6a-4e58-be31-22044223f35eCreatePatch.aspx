<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CreatePatch.aspx.cs" Inherits="PatchMaintainence.Home.CreatePatch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HtmlEditor" TagPrefix="cc1" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <style>
        .ajax__htmleditor_editor_toptoolbar {
            display: none;
        }
    </style>

    <script type="text/javascript">
        function checkValues() {
            var client = document.getElementById("MainContent_DropDownClient").value;
            var dependency = document.getElementById("MainContent_DropDownDependency").value;
            var comment = document.getElementById("MainContent_txtComments").value;
            if (client == "") {
                alert("Please Select Client");
                return false;
            }
            if (dependency == "") {
                alert("Please Select Dependency");
                return false;
            }
            if (comment == "") {
                alert("Please Enter Comment");
                return false;
            }

            return true;

        }
        function checkFileUpload() {
            var file = document.getElementById("MainContent_docUpload").value;
            if (file == "") {
                alert("Please Choose Release Doc.");
                return false;
            }
        }
    </script>

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
        <div class="row">
            <div class="col-md-4">
            </div>
            <div class="col-md-4" style="padding-bottom: 0.5%;">
                <asp:FileUpload runat="server" ID="docUpload" CssClass="form-control" ToolTip="Upload Release Note" EnableViewState="true" ViewStateMode="Enabled" />
                <asp:Label Text="" runat="server" ID="lblFile" CssClass="label label-primary" />

            </div>
            <div class="col-md-4">
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
            </div>
            <div class="col-md-4" style="padding-bottom: 0.5%;">
                <asp:Button runat="server" ID="Submit" OnClick="SubmitMe" Text="Create Patch" OnClientClick="return checkValues()" CssClass="btn btn-success btn-lg btn-block" />

            </div>
            <div class="col-md-4">
            </div>
        </div>




        <div class="row">
            <div class="col-md-4">
            </div>
            <div class="col-md-4">
                <b>
                    <asp:Label runat="server" ID="patchID" Visible="false"></asp:Label></b>

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
                                <asp:Button ID="sendmail" OnClick="SendEmail" OnClientClick="return checkFileUpload()" runat="server" Text="Send Email" CssClass="btn btn-success btn-lg " />
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
    <asp:HiddenField runat="server" ID="hidClient" />
    <asp:HiddenField runat="server" ID="hidCodeMonth" />
    <asp:HiddenField runat="server" ID="hidPatchNumber" />
    <asp:HiddenField runat="server" ID="hidPatchUrl" />
    <asp:HiddenField runat="server" ID="hidDocUpload" />
</asp:Content>
