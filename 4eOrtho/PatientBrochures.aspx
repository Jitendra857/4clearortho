<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PatientBrochures.aspx.cs" Inherits="_4eOrtho.PatientBrochures" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd">
<html>
<head>
    <title>4ClearOrtho - Patient Brochure</title>
    <link href="Styles/brochure.css" rel="stylesheet" />
    <link rel="shortcut icon" type="image/x-icon" href="Content/Images/logo.ico" />
    <%--<script language="javascript" type="text/javascript">
        function printpage() {
            window.print();
        }             
    </script>--%>
    <script language="javascript" type="text/javascript">
        function pageLoad() {
            jQuery("#aPatientBrochure").colorbox({
                iframe: true,
                width: "800px",
                height: "350px",
                overlayClose: false,
                escKey: true
            });
        }
        function printDiv(printablediv) {
            //Get the HTML of div
            var divElements = document.getElementById(printablediv).innerHTML;
            //Get the HTML of whole page
            var oldPage = document.body.innerHTML;
            //Reset the page's HTML with div's HTML only
            document.body.innerHTML =
              "<html><head><title></title></head><body>" +
              divElements + "</body>";
            //Print Page
            window.print();
            //Restore orignal HTML
            document.body.innerHTML = oldPage;
        }
    </script>
    <style type="text/css">
        .p2 {
            text-align: left;
            padding-left: 85px;
            margin-top: 30px;
            margin-bottom: 0px;
            color: whitesmoke;
        }

        .p4 {
            text-align: left;
            padding-left: 8px;
            padding-right: 84px;
            margin-top: 45px;
            margin-bottom: 0px;
        }

        .p14 {
            text-align: left;
            padding-left: 2px;
            padding-right: 161px;
            margin-top: 80px;
            margin-bottom: 0px;
            text-indent: 14px;
            font-size: 16px;
            font-family: HELVETICA;
        }

        .p16 {
            margin-bottom: 0;
            margin-top: 3px;
            padding-left: 17px;
            text-align: left;
            font-size: 16px;
            color: #585747;
            line-height: 18px;
        }

        .p17 {
            margin-bottom: 0;
            margin-top: 2px;
            padding-left: 17px;
            text-align: left;
            font-size: 16px;
            color: #585747;
            line-height: 18px;
        }

        .p7 {
            text-align: left;
            padding-right: 96px;
            margin-top: 45px;
            margin-bottom: 0px;
        }

        .p18 {
            text-align: left;
            padding-left: 18px;
            margin-top: 0px;
            margin-bottom: 0px;
            font-size: 16px;
            color: #585747;
            line-height: 18px;
        }

        .p19 {
            text-align: left;
            padding-left: 20px;
            padding-right: 76px;
            margin-top: 3px;
            margin-bottom: 0px;
        }

        .p24 {
            padding-right: 0px!important;
        }

        .p27 {
            text-align: left;
            padding-right: 55px;
            margin-top: 25px;
            margin-bottom: 0px;
        }

        .p28 {
            text-align: left;
            margin-top: 10px;
            margin-left: 100px;
            margin-bottom: 0px;
        }

        .p32 {
            text-align: left;
            padding-left: 95px;
            margin-top: 70px;
            margin-bottom: 0px;
        }

        .p50 {
            font-weight: bold;
            color: #19519C;
            text-align: left;
            padding-left: 20px;
            margin-top: 27px;
            margin-bottom: 0px;
        }

        .p51 {
            margin-right: 60px;
            margin-top: 190px;
            text-align: center;
        }

        .p52 {
            margin-right: 60px;
            margin-top: 45px;
            /*text-align: center;*/
        }

        .p53 {
            margin-right: 60px;
            margin-top: 15px;
            /*text-align: center;*/
        }

        .p55 {
            text-align: left;
            padding-left: 148px;
            margin-top: 1px;
            margin-bottom: 0px;
        }

        .page {
            page-break-after: always;
        }
    </style>

</head>

<body>
    <form id="Form1" runat="server">
        <div class="alignleft app_popup" id="intailcolorboxContent">
            <asp:Panel ID="pnlContents" runat="server" meta:resourcekey="pnlContentsResource1">
                <div class="Search_button">
                    <input type="image" class="btn" onclick="javascript: printDiv('printablediv')" id="Submit1" value="Print" src="Content/images/print.png" title='<%=this.GetLocalResourceObject("btnprintmeta") %>' />
                    <%--<input type="submit" class="btn" title="Print" onclick="printpage()" id="btnPrint" value="Print" />
                    --%>
                    <asp:ImageButton ID="btnDownload" class="btn" runat="server" Text="Download" ImageUrl="~/Content/images/download.png" OnClick="btnDownload_Click" meta:resourcekey="btndownloadmeta" />
                </div>
                <div id="printablediv">
                    <div class="page">
                        <div>
                            <table id="patientInfo" style="margin: 10px 0 0 68px; color: #000; font: 13px/15px Arial;" cellpadding="5" cellspacing="5">
                                <tr>
                                    <td style="font-weight: bold">
                                        <asp:Label ID="lblPatientNameTitle" runat="server" Text="Patient Name" meta:resourcekey="lblPatientNameTitleResource1"></asp:Label>
                                        :</td>
                                    <td>
                                        <asp:Label ID="lblPatientName" runat="server" meta:resourcekey="lblPatientNameResource1"></asp:Label></td>
                                    <td width="160px"></td>
                                    <td style="font-weight: bold">
                                        <asp:Label ID="lblEmailAddressTitle" runat="server" Text="Email Address" meta:resourcekey="lblEmailAddressTitleResource1" />
                                        :</td>
                                    <td>
                                        <asp:Label ID="lblEmailAddress" runat="server" meta:resourcekey="lblEmailAddressResource1"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold">
                                        <asp:Label ID="lblAmountTitle" runat="server" Text="Amount" meta:resourcekey="lblAmountTitleResource1" />
                                        :</td>
                                    <td>
                                        <asp:Label ID="lblAmount" runat="server" meta:resourcekey="lblAmountResource1"></asp:Label></td>
                                    <td width="160px"></td>
                                    <td style="font-weight: bold">
                                        <asp:Label ID="lblBrochureDateTitle" runat="server" Text="Date" meta:resourcekey="lblBrochureDateTitleResource1" />
                                        :</td>
                                    <td>
                                        <asp:Label ID="lblBrochureDate" runat="server" meta:resourcekey="lblBrochureDateResource1"></asp:Label></td>
                                </tr>
                            </table>
                        </div>
                        <div id="page_1" style="margin-left: 50px">
                            <div id="dimg1">
                                <img src="Content/images/brochure3.jpg" id="img1">
                            </div>
                            <div class="dclr"></div>
                            <div>
                                <div id="id_1">
                                    <div class="p0 ft0" id="dvName" runat="server">
                                        &nbsp;
                                    </div>
                                    <div class="p1 ft1" id="dvContact" runat="server">
                                        &nbsp;
                                    </div>
                                    <div class="p1 ft1" id="dvMobilepage1" runat="server">
                                        &nbsp;
                                    </div>
                                    <div class="p1 ft1" id="divaddress" runat="server">
                                        &nbsp;
                                    </div>
                                    <div class="p1 ft1" id="divaddress2" runat="server">
                                        &nbsp;
                                    </div>
                                    <div class="p1 ft1" id="divaddress3" runat="server">
                                        &nbsp;
                                    </div>
                                    <p class="p2 ft2">Made in America</p>
                                    <p class="p3 ft3">We are proud to offer clear ortho solutions. Clear trays or clear brackets, this is the patient’s complete orthodontic solution. Straight teeth promote confidence, success, and most importantly a healthier body. Completed in a short amount of time, let us improve your smile. Ask your doctor for a Clear ortho solution.</p>
                                    <p class="p4 ft4">CLEAR TRAYS AND CLEAR BRACKETS FOR THE SAME PRICE.</p>
                                    <p class="p5 ft5">“I’ve been serving patients in Virginia for 20 years, and I am truly impressed with 4clear ortho. Thanks 4edental for taking care of the doctors.</p>
                                    <p class="p6 ft6"><span class="ft6">-</span><span class="ft7">Dr. Ali Modarres</span></p>
                                </div>
                                <div id="id_2">
                                    <p class="p7 ft8">CLEAR BRACKET BENFITS:</p>
                                    <p class="p8 ft2"><span class="ft9">&bull;</span><span class="ft10">Clear brackets that are virtually invisible</span></p>
                                    <p class="p9 ft2"><span class="ft9">&bull;</span><span class="ft10">White wire to camouflage treatment</span></p>
                                    <p class="p9 ft2"><span class="ft9">&bull;</span><span class="ft10">No compliance issues</span></p>
                                    <p class="p10 ft2"><span class="ft9">&bull;</span><span class="ft10">Affordable payment plans</span></p>
                                    <p class="p11 ft12"><span class="ft9">&bull;</span><span class="ft11">Your dentist is certified by the American Academy of Dentistry</span></p>
                                    <p class="p12 ft12"><span class="ft9">&bull;</span><span class="ft11">Shorter treatment times</span></p>
                                    <p class="p13 ft12"><span class="ft9">&bull;</span><span class="ft11">More complete treatment options while staying fashionable</span></p>
                                    <div class="p14 ft13" id="dvNameDoctor" runat="server" style="font-family: HELVETICA; font-size: 16px; font-weight: bold"></div>
                                    <p class="p15 ft13">
                                        <asp:Label ID="lblAddress" runat="server" meta:resourcekey="lblAddressResource1"></asp:Label>
                                    </p>
                                    <p class="p16 ft13" id="dvContactdoctor" runat="server">
                                        <asp:Label ID="lblDoctorPhone" runat="server" meta:resourcekey="lblDoctorPhoneResource1"></asp:Label>
                                    </p>
                                    <p class="p17 ft14" id="dvmobile" runat="server"></p>
                                    <p class="p18 ft15" id="dvadressmid" runat="server"></p>
                                    <p class="p18 ft15" id="dvaddressmid2" runat="server"></p>
                                    <p class="p18 ft15" id="dvaddressmid3" runat="server"></p>
                                </div>
                                <div id="id_3">
                                    <p class="p55 ft14">4clearortho.com</p>
                                    <p class="p50 ft14">Clear Brackets</p>
                                    <p class="p19 ft15">Traditional brackets provide a more comprehensive orthodontic treatment. Compliance is not an issue.</p>
                                    <p class="p20 ft14">General Orthodontic Benefits</p>
                                    <p class="p21 ft15">There is a proven correlation between your tooth health and your overall health. The straighter your teeth, the easier for you to care for them. Some benefits include: </p>
                                    <p class="p22 ft18"><span class="ft16">&bull;</span><span class="ft17">Heart health</span></p>
                                    <p class="p22 ft18"><span class="ft16">&bull;</span><span class="ft17">Lower cancer incidents</span></p>
                                    <p class="p23 ft18"><span class="ft16">&bull;</span><span class="ft17">Healthier pregnancies</span></p>
                                    <p class="p24 ft20"><span class="ft16">&bull;</span><span class="ft19">Better blood sugar control</span></p>
                                    <p class="p23 ft18"><span class="ft16">&bull;</span><span class="ft17">Less stroke occurrence</span></p>
                                    <p class="p22 ft18">
                                        <span class="ft16">&bull;</span><span class="ft17">Less periodontal disease
                                        <br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(gum disease)</span>
                                    </p>
                                    <p class="p25 ft20"><span class="ft16">&bull;</span><span class="ft19">Less dental decay</span></p>
                                    <p class="p23 ft18"><span class="ft16">&bull;</span><span class="ft17">Happier psychology</span></p>
                                    <%--<p class="p26 ft18"><span class="ft16">&bull;</span><span class="ft17">Whitening included with each case</span></p>--%>
                                    <p class="p27 ft5">FINALLY A COMPANY THAT GIVES A COMPLETE SOLUTION FOR MY ORTHODONTIC NEEDS. CLEAR TRAYS AND CLEAR BRACKETS FOR ALL MY ORTHO CASES IN ONE COMPANY.</p>
                                    <p class="p28 ft6">- DR. SUNG HAN DDS</p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="page_2" style="margin-left: 50px">
                        <div id="dimg1">
                            <img src="Content/images/brochure4.jpg" id="img2">
                        </div>
                        <div class="dclr"></div>
                        <div>
                            <div id="id_1">
                                <div class="p29 ft0" id="dvNamePage2" runat="server">4CLEAR ORTHO</div>
                                <div runat="server" id="dvdoctorEmpailpage2" class="p30 ft1">&nbsp;</div>
                                <div class="p49 ft1" id="dvmobile2" runat="server">
                                    &nbsp;
                                </div>
                                <p class="p32 ft22">Clear Trays</p>
                                <p class="p51 ft26">CLEAR TRAYS ARE VIRTUALLY INVISIBLE AND CONVENIENT FOR YOU BUSY LIFESTYLE</p>
                                <p class="p52 ft27">The Clear tray system allows you to straighten your teeth with a series of clear plastic trays. You visit your dentist only once a month to verify that your treatment is progressing smoothly.</p>
                                <p class="p53 ft27">Once completed, usually less then 6 months, you can whiten your teeth using your 4clear ortho trays. You will then wear retainers to protect your teeth and their alignment. All this is virtually hassel free.</p>
                                <%--   <p class="p33 ft23">our 4ClearOrtho Referral Plan. Each time</p>--%>
                            </div>
                            <div id="id_2">
                                <p class="p34 ft13">4CLEAR ORTHO’S COMMITMENT:</p>
                                <p class="p35 ft14">Certification</p>
                                <p class="p36 ft15">Your dentist has been trained and certified by The American Academy of Dentistry. They will have online access to continuing education. This allows you to have access to the most complete orthodontic solution.</p>
                                <p class="p35 ft14">Made in America</p>
                                <p class="p37 ft24">Your cases are analyzed and made in America. Your doctor will mail your impressions or models with a prescription to the 4clear ortho facility. In as little as 3 weeks you will be ready to begin treatment.</p>
                                <p class="p38 ft14">Affordable Payment Plans</p>
                                <p class="p39 ft15">Your doctor can offer affordable payment plans. This creates access for everyone to have that smile they have always wanted.</p>
                                <p class="p40 ft14">Expert Assistance</p>
                                <p class="p41 ft15">We take the confusion out of orthodontics. After your doctor analyzes your case, we fabricate your case to achieve his goals in the shortest amount of time.</p>
                            </div>
                            <div id="id_3">
                                <p class="p42 ft13">4clearortho.com</p>
                                <p class="p43 ft14">CLEAR TRAY BENEFITS</p>
                                <p class="p44 ft15">There are no food restrictions You can brush and floss normally so you are healthier. You can remove the trays for important occasions. Comparable prices to traditional braces.</p>
                                <p class="p45 ft14">Teen Ortho</p>
                                <p class="p46 ft15">Most cases can be completed in a 5-6 month time period. If your teen is reliable, 4clear ortho trays are a healthier way to straighten your teeth.</p>
                                <p class="p47 ft14">Grinding and Clinching</p>
                                <p class="p48 ft24">Did you know that the clear trays have been know to lessen and even prevent headaches?. Your doctor will can show you that not only is it healthier for straight teeth, but the trays also protect your teeth and muscles of mastication.</p>
                                <p class="p38 ft14">QUESTIONS?</p>
                                <p class="p49 ft25">Do you have questions about orthodontics in general? Your dentist can help you decide which system is ideal for you. 4Clear ortho is your partner in your complete health.</p>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </form>
    <script>
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
            m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', 'https://www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-100101373-1', 'auto');
        ga('send', 'pageview');

    </script>
</body>
</html>
