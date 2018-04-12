/* Added By Mrityunjay*/

function ShowConfirmationMessage() {
    ModalPopups.Alert("idCustom1",
    "&nbsp;&nbsp;&nbsp;",
    "<div  style='padding-left: 8px;padding-bottom: 0px'><b>We have emailed your Password to you.</b><BR/><b>If you don't receive it in 10 minutes, please check in your <br/> Spam/Junk folder.</b></div>",
   {
       width: 350,
       height: 200,
       fontSize: 20,
       fontFamily: "Calibri",
       borderColor: "white"
   }
   );
    document.getElementById('idCustom1_okButton').style.height = '20px';
    document.getElementById('idCustom1_okButton').style.fontSize = '10pt';
    document.getElementById('idCustom1_shadow').style.display = 'none';
    document.getElementById('idCustom1_popupBody').style.height = '60px';
    document.getElementById('idCustom1_popupFooter').style.top = '90px';
    document.getElementById('idCustom1_popup').style.height = '130px';
}


function openForgotPasswordPopUp(strAlertMsg) {
    ModalPopups.Confirm("idCustom2",
              "&nbsp;",
    "<div  style='padding-left: 8px;padding-bottom: 0px'><B>" + strAlertMsg + "</B><br /><asp:Label ID='lblUserMsg' runat='server' CssClass='label label-danger'></asp:Label><input type='email' class='form-control' name='useremail' id='useremail' placeholder='Your Registered Email Id' required=''><br/><br/><br/></div>",
     {
         width: 350,
         height: 200,
         fontSize: 16,
         fontFamily: "Calibri",
         borderColor: "white",
         buttons: "yes,no",
         yesButtonText: "Send Mail",
         noButtonText: "No",
         onYes: "funcSendForgotPasswordMail()",
         onNo: "funCancel()"
     }
);
    document.getElementById('idCustom2_shadow').style.display = 'none';
    document.getElementById('idCustom2_popupBody').style.height = '60px';
    document.getElementById('idCustom2_popupFooter').style.top = '90px';
    document.getElementById('idCustom2_popup').style.height = '130px';
    document.getElementById('idCustom2_yesButton').style.height = '30px';
    document.getElementById('idCustom2_yesButton').style.width = '80px';
    document.getElementById('idCustom2_yesButton').style.fontSize = '10pt';
    document.getElementById('idCustom2_noButton').style.height = '30px';
    document.getElementById('idCustom2_noButton').style.width = '80px';
    document.getElementById('idCustom2_noButton').style.fontSize = '10pt';
}

function funCancel() {
    ModalPopups.Cancel("idCustom2");
}

function showAskMoreFeedback(strMessage, indicator) {
    ModalPopups.Alert("idCustom1",
    "&nbsp;&nbsp;&nbsp;",
    "<div  style='padding-left: 8px;padding-bottom: 0px'><asp:Label ID='lblFeedback' runat='server' Font-Size='Medium' CssClass='label label-danger'></asp:Label></div>",
   {
       width: 350,
       height: 200,
       fontSize: 20,
       fontFamily: "Calibri",
       borderColor: "white"
   }
   );
    document.getElementById('idCustom1_okButton').style.height = '20px';
    document.getElementById('idCustom1_okButton').style.fontSize = '10pt';
    document.getElementById('idCustom1_shadow').style.display = 'none';
    document.getElementById('idCustom1_popupBody').style.height = '60px';
    document.getElementById('idCustom1_popupFooter').style.top = '90px';
    document.getElementById('idCustom1_popup').style.height = '130px';
    document.getElementById("lblFeedback").innerHTML = "Your response has been successfully submitted.";

    if (indicator == true)
    {
        document.getElementById("lblFeedback").className = "label label-success";
    }
}