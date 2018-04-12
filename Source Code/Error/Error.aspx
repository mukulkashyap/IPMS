<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Error.aspx.cs" Inherits="IPMS.Error" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <div class="container">
        <div class="row">
            <div class="col-xs-12">
                <div id="error-box">
                    <div class="row">
                        <div class="col-xs-12">
                            <div id="error-box-inner">
                                <img src="../Content/img/error-500-v1.png" alt="Have you seen this page?" />
                            </div>
                            <h1>Ooops Something Went Wrong </h1>
                            <p>
                                We will surely get back to you.
                            </p>
                            <p>
                                Go back to <a href="../Home/ToDo">Dashboard</a>.
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
