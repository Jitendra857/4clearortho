<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="PatientCaseDetails.aspx.cs" Inherits="_4eOrtho.Admin.PatientCaseDetails" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Jquery-UI/jquery-ui.css" rel="stylesheet" />
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .parsonal_textfild label {
            width: 236px;
        }

        .custom-combobox {
            position: relative;
            display: inline-block;
        }

        .custom-combobox-toggle {
            position: absolute;
            top: 0;
            bottom: 0;
            margin-left: -1px;
            padding: 0;
        }

        .custom-combobox-input {
            margin: 0;
            padding: 5px 10px;
        }
    </style>
    <div id="container" class="cf">
        <div class="page_title">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 50%;">
                        <h2 class="padd">
                            <asp:Label ID="lblHeader" runat="server" Text="Share Case"></asp:Label>
                        </h2>
                    </td>
                    <td style="width: 50%;">
                        <span class="dark_btn_small">
                            <asp:Button ID="btnBack" runat="server" Text="Back" Width="100px" PostBackUrl="~/Admin/ListNewCaseDetails.aspx"
                                TabIndex="5" meta:resourcekey="btnBackResource1"></asp:Button>
                        </span>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divMsg" runat="server">
            <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
        </div>
        <div class="widecolumn">

            <%--Shared Doctor--%>
            <div class="personal_box alignleft">
                <div class="parsonal_textfild" runat="server">
                    <label>
                        <strong>
                            <asp:Label ID="lblShareCase" runat="server" Text="Share Case with Doctor"></asp:Label></strong>
                    </label>
                    <div class="ClientTextalign">
                        &nbsp;
                    </div>
                </div>
                <div class="clear"></div>
                <div class="parsonal_textfild" runat="server">
                    <label>
                        <asp:Label ID="Label2" runat="server" Text="Select Doctor"></asp:Label>
                        <span class="asteriskclass" id="span1" runat="server">*</span> <span class="alignright">:</span>
                    </label>
                    <div class="ClientTextalign">
                        <div class="ui-widget">
                            <asp:DropDownList ID="combobox" ClientIDMode="Static" runat="server"></asp:DropDownList>
                            <div style="display: inline;margin-left: 40px;">
                                <asp:RequiredFieldValidator ID="rqvddlDoctor" ForeColor="Red" runat="server" SetFocusOnError="True"
                                    ControlToValidate="combobox" Display="Dynamic" InitialValue="0" ErrorMessage="Please select doctor."
                                    CssClass="errormsg" ValidationGroup="validation"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <%--<ajaxToolkit:ValidatorCalloutExtender ID="cmverqvddlDoctor" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rqvddlDoctor" Enabled="True">
                        </ajaxToolkit:ValidatorCalloutExtender>--%>
                    </div>
                </div>
            </div>
            <%--Shared Doctor--%>

            <%--Patient Information--%>
            <div class="personal_box alignleft">
                <div class="parsonal_textfild" runat="server">
                    <label>
                        <strong>
                            <asp:Label ID="lblPatientInformation" runat="server" Text="Patient Details"></asp:Label></strong>
                    </label>
                    <div class="ClientTextalign">
                        &nbsp;
                    </div>
                </div>
                <div class="clear"></div>
                <div class="parsonal_textfild" runat="server">
                    <label>
                        <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                        <span class="alignright">:</span>
                    </label>
                    <div class="ClientTextalign">
                        <asp:TextBox ID="txtPatientName" ReadOnly="true" Enabled="false" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="clear"></div>
                <div class="parsonal_textfild" runat="server">
                    <label>
                        <asp:Label ID="lblDOB" runat="server" Text="Date of Birth"></asp:Label>
                        <span class="alignright">:</span>
                    </label>
                    <div class="ClientTextalign">
                        <asp:TextBox ID="txtDOB" ReadOnly="true" Enabled="false" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="clear"></div>
                <div class="parsonal_textfild" runat="server">
                    <label>
                        <asp:Label ID="lblGender" runat="server" Text="Gender"></asp:Label>
                        <span class="alignright">:</span>
                    </label>
                    <div class="ClientTextalign">
                        <asp:TextBox ID="txtGender" ReadOnly="true" Enabled="false" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
            <%--Patient Information--%>

            <%--Case Details--%>
            <div class="personal_box alignleft">
                <div class="parsonal_textfild" runat="server">
                    <label>
                        <strong>
                            <asp:Label ID="lblCaseDetails" runat="server" Text="Case Details"></asp:Label></strong>
                    </label>
                    <div class="ClientTextalign">
                        &nbsp;
                    </div>
                </div>
                <div class="clear"></div>
                <div class="parsonal_textfild" runat="server">
                    <label>
                        <asp:Label ID="lblCaseType" runat="server" Text="Case Type"></asp:Label>
                        <span class="alignright">:</span>
                    </label>
                    <div class="ClientTextalign">
                        <asp:TextBox ID="txtCaseType" ReadOnly="true" Enabled="false" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="clear"></div>
                <div class="parsonal_textfild" runat="server">
                    <label>
                        <asp:Label ID="lblOrthoCondition" runat="server" Text="Ortho Condition"></asp:Label>
                        <span class="alignright">:</span>
                    </label>
                    <div class="ClientTextalign">
                        <asp:TextBox ID="txtOrthoCondition" ReadOnly="true" Enabled="false" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="clear"></div>
                <div class="parsonal_textfild" runat="server">
                    <label>
                        <asp:Label ID="lblCaseDetail" runat="server" Text="Case Details"></asp:Label>
                        <span class="alignright">:</span>
                    </label>
                    <div class="ClientTextalign">
                        <asp:TextBox ID="txtCaseDetails" ReadOnly="true" Enabled="false" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
            <%--Case Details--%>

            <%--Package Details--%>
            <div class="personal_box alignleft">
                <div class="parsonal_textfild" runat="server">
                    <label>
                        <strong>
                            <asp:Label ID="lblPackageDetails" runat="server" Text="Package Details"></asp:Label></strong>
                    </label>
                    <div class="ClientTextalign">
                        &nbsp;
                    </div>
                </div>
                <div class="clear"></div>
                <div class="parsonal_textfild" runat="server">
                    <label>
                        <asp:Label ID="lblSelectedPackage" runat="server" Text="Package"></asp:Label>
                        <span class="alignright">:</span>
                    </label>
                    <div class="ClientTextalign">
                        <asp:TextBox ID="txtPackage" ReadOnly="true" Enabled="false" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="clear"></div>
                <div class="parsonal_textfild" runat="server">
                    <label>
                        <asp:Label ID="lblPackageAmount" runat="server" Text="Package Amount"></asp:Label>
                        <span class="alignright">:</span>
                    </label>
                    <div class="ClientTextalign">
                        <asp:TextBox ID="txtPackageAmount" ReadOnly="true" Enabled="false" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="clear"></div>
                <div class="parsonal_textfild" runat="server">
                    <label>
                        <asp:Label ID="lblPackageQty" runat="server" Text="Quantity"></asp:Label>
                        <span class="alignright">:</span>
                    </label>
                    <div class="ClientTextalign">
                        <asp:TextBox ID="txtQuantity" ReadOnly="true" Enabled="false" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="clear"></div>
                <div class="parsonal_textfild" runat="server">
                    <label>
                        <asp:Label ID="lblTotalPackageAmount" runat="server" Text="Total Amount"></asp:Label>
                        <span class="alignright">:</span>
                    </label>
                    <div class="ClientTextalign">
                        <asp:TextBox ID="txtTotalPackageAmount" ReadOnly="true" Enabled="false" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
            <%--Package Details--%>
        </div>
        <div class="clear">
        </div>
        <div class="alignright" style="padding: 0;">
            <span class="blue_btn">
                <asp:Button ID="btnSubmit" runat="server" ValidationGroup="validation" OnClientClick="return ;" Text="Send" OnClick="btnSubmit_Click" />
            </span>
        </div>
    </div>
    <asp:HiddenField ID="hdnSharedDoctorEmail" runat="server" ClientIDMode="Static" />
    <script type="text/javascript">

        function ShareValidationMessage(obj) {
            if ($('#hdnSharedDoctorEmail').val()) {
                if (confirm("This case is already shared. Are you sure to shared with other doctor also ?"))
                    return true;
                else
                    return false;
            }
            return true;
        }

        $(function () {
            $.widget("custom.combobox", {
                _create: function () {
                    this.wrapper = $("<span>")
                        .addClass("custom-combobox")
                        .insertAfter(this.element);

                    this.element.hide();
                    this._createAutocomplete();
                    this._createShowAllButton();
                },

                _createAutocomplete: function () {
                    var selected = this.element.children(":selected"),
                        value = selected.val() ? selected.text() : "";

                    this.input = $("<input>")
                        .appendTo(this.wrapper)
                        .val(value)
                        .attr("title", "")
                        .addClass("custom-combobox-input ui-widget ui-widget-content ui-state-default ui-corner-left")
                        .autocomplete({
                            delay: 0,
                            minLength: 0,
                            source: $.proxy(this, "_source")
                        })
                        .tooltip({
                            classes: {
                                "ui-tooltip": "ui-state-highlight"
                            }
                        });

                    this._on(this.input, {
                        autocompleteselect: function (event, ui) {
                            ui.item.option.selected = true;
                            this._trigger("select", event, {
                                item: ui.item.option
                            });
                        },

                        autocompletechange: "_removeIfInvalid"
                    });
                },

                _createShowAllButton: function () {
                    var input = this.input,
                        wasOpen = false;

                    $("<a>")
                        .attr("tabIndex", -1)
                        .attr("title", "Show All Doctors")
                        .tooltip()
                        .appendTo(this.wrapper)
                        .button({
                            icons: {
                                primary: "ui-icon-triangle-1-s"
                            },
                            text: false
                        })
                        .removeClass("ui-corner-all")
                        .addClass("custom-combobox-toggle ui-corner-right")
                        .on("mousedown", function () {
                            wasOpen = input.autocomplete("widget").is(":visible");
                        })
                        .on("click", function () {
                            input.trigger("focus");

                            // Close if already visible
                            if (wasOpen) {
                                return;
                            }

                            // Pass empty string as value to search for, displaying all results
                            input.autocomplete("search", "");
                        });
                },

                _source: function (request, response) {
                    var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
                    response(this.element.children("option").map(function () {
                        var text = $(this).text();
                        if (this.value && (!request.term || matcher.test(text)))
                            return {
                                label: text,
                                value: text,
                                option: this
                            };
                    }));
                },

                _removeIfInvalid: function (event, ui) {

                    // Selected an item, nothing to do
                    if (ui.item) {
                        return;
                    }

                    // Search for a match (case-insensitive)
                    var value = this.input.val(),
                        valueLowerCase = value.toLowerCase(),
                        valid = false;
                    this.element.children("option").each(function () {
                        if ($(this).text().toLowerCase() === valueLowerCase) {
                            this.selected = valid = true;
                            return false;
                        }
                    });

                    // Found a match, nothing to do
                    if (valid) {
                        return;
                    }

                    // Remove invalid value
                    this.input
                        .val("")
                        .attr("title", value + " didn't match any item")
                        .tooltip("open");
                    this.element.val("");
                    this._delay(function () {
                        this.input.tooltip("close").attr("title", "");
                    }, 2500);
                    this.input.autocomplete("instance").term = "";
                },

                _destroy: function () {
                    this.wrapper.remove();
                    this.element.show();
                }
            });

            $("#combobox").combobox();
        });
    </script>
</asp:Content>
