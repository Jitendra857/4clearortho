<%@ Page Title="" Language="C#" MasterPageFile="~/Ortho.Master" AutoEventWireup="true" CodeBehind="RegistrationReport.aspx.cs" Inherits="_4eOrtho.RegistrationReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="Styles/dataTables.min.css" rel="stylesheet" />
    <asp:UpdatePanel ID="upReport" runat="server">
        <ContentTemplate>
            <div class="rightbar">
                <div class="main_right_cont" style="width: 100%;">
                    <div class="title">
                        <h2>
                            <asp:Label runat="server" ID="lblHeader" Text="Doctor Registration Report" meta:resourcekey="lblHeaderResource1"></asp:Label>
                        </h2>
                    </div>
                </div>
                <asp:Panel ID="pnlSearch" runat="server" meta:resourcekey="pnlSearchResource1">
                    <div class="date2">
                        <div class="parsonal_textfild">
                            <label class="ml15">
                                <asp:Label ID="lblType" runat="server" Text="Type" meta:resourcekey="lblTypeResource1"></asp:Label><span class="alignright">:</span>
                            </label>
                            <div class="radio-selection" style="width: 232px;">
                                <asp:RadioButton ID="rbtnMonth" runat="server" GroupName="ViewType" Text="Month" Checked="True" OnClick="javascript:SelectType();" meta:resourcekey="rbtnMonthResource1" />
                                <asp:RadioButton ID="rbtnDateRange" runat="server" GroupName="ViewType" Text="Date Range" OnClick="javascript:SelectType();" meta:resourcekey="rbtnDateRangeResource1" />
                            </div>
                        </div>
                        <div class="clear">
                        </div>
                        <div id="divDate" class="parsonal_textfild">
                            <label id="lblFromDate" class="ml15">
                                <asp:Label ID="lblRegistrationDate" runat="server" Text="Registered From : " meta:resourcekey="lblRegistrationDateResource1"></asp:Label><span class="alignright">:</span>
                            </label>
                            <label id="lblFromDateText" class="datepicket-field">
                                <asp:TextBox ID="txtFromDate" CssClass="From-Date not-edit textfild search-datepicker" Width="130px"
                                    runat="server" meta:resourcekey="txtFromDateResource1"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="rfvtxtFromDate" ForeColor="Red" runat="server" ControlToValidate="txtFromDate"
                                    Display="None" ErrorMessage="Please select from date." CssClass="errormsg"
                                    SetFocusOnError="True" ValidationGroup="serachValidation"  />
                                <ajaxToolkit:ValidatorCalloutExtender ID="vceDateofBirth" runat="server" CssClass="customCalloutStyle"
                                    TargetControlID="rfvtxtFromDate" Enabled="True" />--%>
                            </label>
                            <label id="lblToDate" class="smalltitle" style="width: 50px;">
                                <asp:Label ID="lblTo" runat="server" Text="To" Width="40px"></asp:Label><span
                                    class="alignright">:</span>
                            </label>
                            <label id="lblToDateText" class="datepicket-field">
                                <asp:TextBox ID="txtToDate" CssClass="To-Date not-edit textfild search-datepicker" Width="130px"
                                    runat="server"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="rfvtxtToDate" ForeColor="Red" runat="server" ControlToValidate="txtToDate"
                                    Display="None" ErrorMessage="Please select to date." CssClass="errormsg"
                                    SetFocusOnError="True" ValidationGroup="serachValidation"  />
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="customCalloutStyle"
                                    TargetControlID="rfvtxtToDate" Enabled="True" />--%>
                            </label>
                        </div>
                        <div id="divMonth" class="parsonal_textfild">
                            <label id="lblFromMonth" class="ml15">
                                <asp:Label ID="lblRegistrationMonthYear" runat="server" Text="Month/Year"
                                    meta:resourcekey="lblRegistrationMonthYearResource1"></asp:Label><span class="alignright">:</span>
                            </label>
                            <div id="divFromMonth" class="parsonal_select" style="width: 100px;">
                                <asp:DropDownList ID="ddlFromMonth" runat="server" Width="100px" meta:resourcekey="ddlFromMonthResource1">
                                    <asp:ListItem Text="January" Value="1" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                    <asp:ListItem Text="February" Value="2" meta:resourcekey="ListItemResource4"></asp:ListItem>
                                    <asp:ListItem Text="March" Value="3" meta:resourcekey="ListItemResource5"></asp:ListItem>
                                    <asp:ListItem Text="April" Value="4" meta:resourcekey="ListItemResource6"></asp:ListItem>
                                    <asp:ListItem Text="May" Value="5" meta:resourcekey="ListItemResource7"></asp:ListItem>
                                    <asp:ListItem Text="June" Value="6" meta:resourcekey="ListItemResource8"></asp:ListItem>
                                    <asp:ListItem Text="July" Value="7" meta:resourcekey="ListItemResource9"></asp:ListItem>
                                    <asp:ListItem Text="August" Value="8" meta:resourcekey="ListItemResource10"></asp:ListItem>
                                    <asp:ListItem Text="September" Value="9" meta:resourcekey="ListItemResource11"></asp:ListItem>
                                    <asp:ListItem Text="October" Value="10" meta:resourcekey="ListItemResource12"></asp:ListItem>
                                    <asp:ListItem Text="November" Value="11" meta:resourcekey="ListItemResource13"></asp:ListItem>
                                    <asp:ListItem Text="December" Value="12" meta:resourcekey="ListItemResource14"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div id="divFromYear" class="parsonal_select" style="width: 100px; margin-left: 8px;">
                                <asp:DropDownList ID="ddlYear" runat="server" Width="100px" meta:resourcekey="ddlYearResource1">
                                </asp:DropDownList>
                            </div>
                            <asp:Label ID="lblErrorMessage" runat="server" Style="color: red; display: none;" Text="Please select date(s)."></asp:Label>
                        </div>
                        <div class="clear" style="height: 5px;">
                        </div>
                        <div class="Search_button" style="margin-left: 198px !important;">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="serachValidation" CssClass="btn" OnClientClick="return validate()" OnClick="btnSearch_Click"></asp:Button>
                        </div>
                        <div class="clear" style="height: 15px;">
                        </div>
                    </div>
                </asp:Panel>
                <div class="list-data" style="font-size: 13px; min-height: 700px;">
                    <table id="datatable" class="display" cellspacing="0" width="100%">
                        <asp:Repeater ID="rptReport" runat="server">
                            <HeaderTemplate>
                                <thead>
                                    <tr>
                                        <th>Regirsterd On
                                        </th>
                                        <th>FirstName
                                        </th>
                                        <th>LastName
                                        </th>
                                        <th>CountryName
                                        </th>
                                        <th>StateName
                                        </th>
                                        <th>City
                                        </th>
                                        <th>Gender
                                        </th>
                                        <th>Status
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Convert.ToDateTime(Eval("CreatedDate")).ToString("dd-MMM-yyyy") %>
                                    </td>
                                    <td><%# Eval("FirstName") %>
                                    </td>
                                    <td><%# Eval("LastName") %>
                                    </td>
                                    <td><%# Eval("CountryName") %>
                                    </td>
                                    <td><%# Eval("StateName") %>
                                    </td>
                                    <td><%# Eval("City") %>
                                    </td>
                                    <td style="text-align: center;"><%# Convert.ToString(Eval("Gender")).Trim().ToLower() == "m" ? "Male" : "Female" %>
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:Image ID="imgStatus" runat="server" ImageUrl='<%# Convert.ToBoolean(Eval("IsActive"))?"Content/images/icon-active.gif" :"Content/images/icon-inactive.gif" %>' ToolTip='<%# Convert.ToBoolean(Eval("IsActive"))?"Active" :"In-Active" %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upReport"
        DisplayAfter="10">
        <ProgressTemplate>
            <div class="processbar" style="background-color:white;">
                <img src="content/images/loading.gif" alt="loading" width="32" height="32px" alt="Processing..." style="top: 50%; left: 50%; position: absolute;"></img>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <script src="Scripts/dataTables.min.js"></script>
    <style type="text/css">
        .lalign {
            float: left !important;
        }

        .ralign {
            float: right !important;
        }

        .dataTables_length select {
            width: 56px;
            border: solid 1px #dddddd;
            background: #fff;
            border-radius: 3px;
            -moz-border-radius: 3px;
            -webkit-border-radius: 3px;
            padding: 5px 5px;
            margin-bottom: 5px;
        }

        .dataTables_filter input {
            border: solid 1px #dddddd;
            background: #fff;
            border-radius: 3px;
            -moz-border-radius: 3px;
            -webkit-border-radius: 3px;
            padding: 5px 5px;
            margin-bottom: 5px;
        }

        .dataTables_wrapper {
            border: 1px solid #dddddd;
            padding: 5px;
        }

        .dt-buttons {
            margin-left: 5px;
            padding: 2px;
            height: 25px;
            border: 1px solid #dddddd;
        }

            .dt-buttons a {
                padding: 15px !important;
                border: none !Important;
                background-repeat: no-repeat !Important;
                margin-right: 0px !important;
            }

        .buttons-excel {
            background: url('../content/images/xlsx.png') !important;
            background-size: cover;
        }

        .buttons-pdf {
            background: url('../content/images/pdf.png') !important;
            background-size: cover;
        }

        .buttons-print {
            background: url('../content/images/print.png') !important;
            background-size: cover;
        }

        .buttons-csv {
            background: url('../content/images/csv.png') !important;
            background-size: cover;
        }

        .buttons-copy {
            background: url('../content/images/copy.png') !important;
            background-size: cover;
        }

        .dt-button span {
            display: none;
        }
    </style>
    <script type="text/javascript">

        function preloadFunc() {
            $('.right_section').hide();
            $('.left_section').width("100%");
        }
        window.onpaint = preloadFunc();

        $(document).ready(function () {
            blockui(".rightbar");
            Init();
            Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(blockpage);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(unblockpage);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(Init);
        });

        function blockpage() {
            blockui("[id$=upReport]");
        }

        function unblockpage() {
            $("[id$=upReport]").unblock();
        }

        function Init() {
            SelectType();
            $('#datatable').on('init.dt', function () {
                $('.right_section').hide();
                $('.left_section').width("100%");
                $('.rightbar').unblock();
            }).on('draw.dt', function () {
                $('.right_section').hide();
                $('.left_section').width("100%");
            }).DataTable({
                "order": [[ 0, "desc" ]],
                dom: "<'select_task_main'<'toolbar'><'lalign' l>B<'ralign' f>>" + "<'listtable'tr>" + "<ip>",
                buttons: [
                    { extend: 'copy', exportOptions: { columns: [0, 1, 2, 3, 4, 5, 6] } },
                    { extend: 'csv', exportOptions: { columns: [0, 1, 2, 3, 4, 5, 6] } },
                    { extend: 'excel', title: 'DoctorRegistrationReport', exportOptions: { columns: [0, 1, 2, 3, 4, 5, 6] } },
                    {
                        extend: 'pdf', title: 'DoctorRegistrationReport', exportOptions: { columns: [0, 1, 2, 3, 4, 5, 6] },
                        customize: function (doc) {
                            // Splice the image in after the header, but before the table
                            doc.content.splice(0, 0, {
                                margin: [0, 0, 0, 5],
                                alignment: 'left',
                                image: 'data:image/png;base64,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',
                                width: 80,
                                height: 80
                            });
                        }
                    },
                    {
                        extend: 'print', exportOptions: { columns: [0, 1, 2, 3, 4, 5, 6] },
                        customize: function (win) {
                            $(win.document.body).addClass('white-bg');
                            $(win.document.body).css('font-size', '10px');
                            $(win.document.body).find('table')
                                    .addClass('compact')
                                    .css('font-size', 'inherit');
                        }
                    }
                ]
            });
            $('.From-Date').datepicker({
                showOn: "button",
                buttonText: "SelectDate",
                buttonImage: "/Admin/Images/bgi/calendar.png",
                buttonImageOnly: true,
                disabled: false,
                changeMonth: true,
                changeYear: true,
                onSelect: function (selectedDate) {
                    jQuery(".To-Date").datepicker("option", "minDate", selectedDate);
                }
            });
            $('.To-Date').datepicker({
                showOn: "button",
                buttonText: "SelectDate",
                buttonImage: "/Admin/Images/bgi/calendar.png",
                buttonImageOnly: true,
                disabled: false,
                changeMonth: true,
                changeYear: true,
                onSelect: function (selectedDate) {
                    jQuery(".From-Date").datepicker("option", "maxDate", selectedDate);
                }
            });
        }

        function SelectType() {
            if ($('[id$=rbtnDateRange]').is(':checked')) {
                $('[id$=divDate]').show(); $('[id$=divMonth]').hide();
            }
            else {
                $('[id$=divDate]').hide(); $('[id$=divMonth]').show();
            }
        }

        function validate() {
            if ($('[id$=rbtnDateRange]').is(':checked')) {
                if ($('[id$=txtFromDate]').val() == '' || $('[id$=txtToDate]').val() == '') {
                    $('[id$=lblErrorMessage]').show();
                    return false;
                }
                else {
                    $('[id$=lblErrorMessage]').hide();
                    return true;
                }
            } else {
                $('[id$=lblErrorMessage]').hide();
                return true;
            }
        }

        function blockui(divClassOrId) {
            $(divClassOrId).block({
                message: '<img src="content/images/loading.gif" width="32" heigth="32px" alt="Processing..."></img>',
                css: { border: 'none', background: 'none' },
                overlayCSS: { backgroundColor: '#FFF', opacity: 0.5 }
            });
        }
    </script>        
</asp:Content>
