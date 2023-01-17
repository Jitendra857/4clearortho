<%@ Page Language="C#" MasterPageFile="~/OrthoInnerMaster.Master" AutoEventWireup="true" CodeBehind="AddUpdateStage.aspx.cs" Inherits="_4eOrtho.AddUpdateStage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>
    <link href="../Styles/Jquery-UI/jquery-ui-1.8.23.custom.css" rel="Stylesheet" type="text/css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="Styles/lightbox.min.css" rel="stylesheet" />   
    <asp:UpdatePanel ID="upProductMaster" runat="server">
        <ContentTemplate>
            <div class="title main_right_cont" style="width: 100%;">
                <div class="supply-button3 back">
                    <asp:Button ID="btnBack" runat="server" Text="Back" PostBackUrl="~/PatientStageDetails.aspx" TabIndex="7" meta:resourcekey="btnBackResource1" />
                </div>
                <h2>
                    <asp:Label ID="lblHeader" Text="Add/Update Stage"  runat="server" meta:resourcekey="lblHeaderResource1"></asp:Label>
                </h2>
            </div>
            <div class="main_right_cont minheigh">
                <div id="divMsg" runat="server">
                    <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
                </div>
                <div class="personal_box alignleft">
                    <asp:PlaceHolder ID="phDetail" runat="server">
                       
                     
                       
                        <div class="parsonal_textfild alignleft">
                            <label>
                                <asp:Label ID="lblAmount" runat="server" Text="Stage Name" meta:resourcekey="lblAmountResource1"></asp:Label>
                                <span class="alignright">:</span>
                            </label>
                            <asp:TextBox ID="txtstagename" Enabled="true"  runat="server" MaxLength="15" Style="text-align: left;" TabIndex="2" meta:resourcekey="txtAmountResource1" ></asp:TextBox>
                            
                             <asp:Label ID="lblstagename" runat="server" ForeColor="Red" Font-Size="12px" Text="" meta:resourcekey="lblAmountResource1"></asp:Label>
                            <asp:RequiredFieldValidator ID="rqvAmount" ForeColor="Red" runat="server" SetFocusOnError="True"
                                ControlToValidate="txtstagename" Display="None" ErrorMessage="Please fill stage."
                                CssClass="errormsg" ValidationGroup="validation" meta:resourcekey="rqvAmountResource1" />
                           <%-- <asp:RegularExpressionValidator ID="rgvAmount" runat="server" ControlToValidate="txtAmount"
                                SetFocusOnError="True" ValidationExpression="\d+(\.\d{1,2})?" ValidationGroup="validation"
                                CssClass="errormsg" ErrorMessage="Only Numeric Values with two precesion values is allowed"
                                meta:resourcekey="rgvAmountResource1"></asp:RegularExpressionValidator>--%>
                            <%--<ajaxToolkit:FilteredTextBoxExtender ID="fteMobile" runat="server" Enabled="True"
                                TargetControlID="txtAmount" ValidChars="0123456789." />--%>
                            <ajaxToolkit:ValidatorCalloutExtender ID="rqveuploadPhoto" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rqvAmount" Enabled="True" Width="140px">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </div>
                        <div class="parsonal_textfild alignleft">
                            <label>
                                <asp:Label ID="lblProductAmount" runat="server" Text="Stage Amount" meta:resourcekey="lblProductAmountResource1"></asp:Label>
                                <span class="alignright">:</span>
                            </label>
                            <asp:TextBox ID="txtstageprice" runat="server" MaxLength="15" Style="text-align: left;" TabIndex="3" meta:resourcekey="txtQuantityResource1"  AutoPostBack="true"></asp:TextBox>
                            <asp:RangeValidator ID="rvQuantity" runat="server" ForeColor="Red" Display="None" SetFocusOnError="True" ControlToValidate="txtstageprice"
                                ErrorMessage="Please enter valid price." ValidationGroup="validation" MinimumValue="1" MaximumValue="987654321" meta:resourcekey="rvQuantityResource1"></asp:RangeValidator>
                            <asp:RequiredFieldValidator ID="rqvQuantity" ForeColor="Red" runat="server" 
                                ControlToValidate="txtstageprice" Display="None" ErrorMessage="Please enter stage price."
                                CssClass="errormsg" ValidationGroup="validation" meta:resourcekey="rqvQuantityResource1" />
                            <%--<asp:RegularExpressionValidator ID="rgvQuantity" runat="server" ControlToValidate="txtstageprice"
                                SetFocusOnError="True" ValidationExpression="^(0|[1-9][0-9]*)$" ValidationGroup="validationProduct"
                                CssClass="errormsg" ErrorMessage="Only Numeric Values are allowed" meta:resourcekey="rgvQuantityResource1"></asp:RegularExpressionValidator>--%>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteQuantity" runat="server" Enabled="True"
                                TargetControlID="txtstageprice" ValidChars="0123456789" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="rqveQuantity" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rqvQuantity" Enabled="True" Width="140px">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rvQuantity" Enabled="True" Width="140px">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </div>
                        <div class="parsonal_textfild alignleft">
                            <label>
                                <asp:Label ID="lblTotalAmount" runat="server" Text="Description" meta:resourcekey="lblTotalAmountResource1"></asp:Label>
                                <span class="alignright">:</span>
                            </label>
                           <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Style="height: 100px" TabIndex="13" meta:resourcekey="txtNotesResource1"></asp:TextBox>
                        </div>
                         <div class="parsonal_textfild alignleft">
                                <label>
                                    <asp:Label runat="server" ID="lblIsActive" Text="Is Active" meta:resourcekey="lblIsActiveResource1"></asp:Label>
                                    <span class="alignright">:</span>
                                </label>
                                <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" TabIndex="14" meta:resourcekey="chkIsActiveResource1" />
                            </div>
                        
                       
                    </asp:PlaceHolder>
                   
                </div>
                <div class="date2" id="divButtons" runat="server">
                    <div>
                        <div>
                            <div class="supply-button3">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSave_Click" ValidationGroup="validation"
                                     TabIndex="5" meta:resourcekey="btnSubmitResource1" />
                            </div>
                            <div class="supply-button3" style="width: 150px;">
                                <asp:Button ID="btnMakePayment" runat="server" Text="Make Payment" ValidationGroup="validation1" Visible="false"
                                    TabIndex="5" meta:resourcekey="btnMakePaymentResource1" />
                            </div>
                            <div class="supply-button3">
                                <asp:Button ID="btnBackPayment" runat="server" Text="Back" Visible="false" CausesValidation="false"
                                    TabIndex="5" meta:resourcekey="btnBackPaymentResource1" />
                            </div>
                            <div class="supply-button3">
                                <asp:Button runat="server" ID="btnReset" Text="Cancel" TabIndex="6" OnClientClick="window.open(window.location.href,'_self');return false;" meta:resourcekey="btnResetResource1" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
       
    </asp:UpdatePanel>
  
  
</asp:Content>

   
  