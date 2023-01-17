<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Brochure.aspx.cs" Inherits="_4eOrtho.Brochure" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd">

<html>
<head>
    <title>4ClearOrtho - Brochure</title>
    <link href="Styles/brochure.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="screen.css" media="print" />
    <link rel="shortcut icon" type="image/x-icon" href="Content/Images/logo.ico" />
    <style type="text/css" media="print">
        #btnPrint {
            display: none;
        }
    </style>
    <style type="text/css">
        .page {
            page-break-after: always;
        }

        .print {
            page-break-after: always;
        }
    </style>
    <%-- <script type="text/javascript">
        function printpage() {
            var html = "<html>";
            html += document.getElementById("bill_print").innerHTML;
            html += "</html>";

            var printWin = window.open('', '', 'width=650,height=600,left=0,top=0,toolbar=0,scrollbars=0,status  =0');
            printWin.document.write(html);
            printWin.document.close();
            printWin.focus();
            printWin.print();
            printWin.close();
            //window.print();
        }
    </script>
    <script language="javascript" type="text/javascript">
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
    </script>--%>
</head>
<body>
    <form runat="server">
        <asp:Panel ID="pnlContents" runat="server">
            <div class="Search_button_Brochure">
                <input type="submit" class="btn" title="Print" onclick="javascript: window.print()" id="btnPrint" value="Print" />
                <%--<input type="submit" class="btn" title="Print" onclick="javascript: printDiv('printablediv')" id="btnPrint" value="Print" />--%>
                <%--<input type="submit" class="btn" title="Print" onclick="printpage()" id="btnPrint" value="Print" />--%>
            </div>
            <div id="printablediv">
                <div id="page_1" style="margin-left: 50px" class="page">
                    <div id="dimg1">
                        <img src="Content/images/brochure1.jpg" id="img1">
                    </div>
                    <div class="dclr"></div>
                    <div>
                        <div id="id_1">
                            <%--<font color="white">--%>
                            <div class="p0 ft0" id="dvName" runat="server">4CLEAR ORTHO</div>
                            <div class="p1 ft1" id="dvContact" runat="server">(540) 657-4333 </div>
                            <p class="p1 ft1">www.4clearortho.com </p>
                            <p class="p2 ft2">Made in American</p>
                            <p class="p3 ft3">We are proud to offer clear ortho solutions. Clear trays or clear brackets, this is the patient’s complete orthodontic solution. Straight teeth promote confidence, success, and most importantly a healthier body. Completed in a short amount of time, let us improve your smile. Ask your doctor for a Clear ortho solution.</p>
                            <p class="p4 ft4">FINALLY CLEAR TRAYS OR CLEAR BRACKETS FOR AN AFFORDABLE PRICE.</p>
                            <p class="p5 ft5">"I didn't think orthodontics could be so simple, Thanks 4clear ortho for helping me care for my teeth."</p>
                            <p class="p6 ft6"><span class="ft7">- Robert Smith Washington DC</span></p>
                            <%--</font>--%>
                        </div>
                        <div id="id_2">
                            <p class="p7 ft8">CLEAR BRACKET BENEFITS:</p>
                            <br />
                            <p class="p8 ft2"><span class="ft9">&bull;</span><span class="ft10">Clear brackets that are virtually invisible</span></p>
                            <p class="p9 ft2"><span class="ft9">&bull;</span><span class="ft10">White wire to camouflage treatment</span></p>
                            <p class="p9 ft2"><span class="ft9">&bull;</span><span class="ft10">No compliance issues</span></p>
                            <p class="p10 ft2"><span class="ft9">&bull;</span><span class="ft10">Affordable payment plans</span></p>
                            <p class="p11 ft12"><span class="ft9">&bull;</span><span class="ft11">Your dentist is certified by the American Academy of Dentistry</span></p>
                            <p class="p12 ft12"><span class="ft9">&bull;</span><span class="ft11">Shorter treatment times</span></p>
                            <p class="p13 ft12"><span class="ft9">&bull;</span><span class="ft11">More complete treatment options while staying fashionable</span></p>
                            <div class="p14 ft13" id="dvNameDoctor" runat="server" style="font-family: HELVETICA; font-size: 16px; font-weight: bold; text-align: center; line-height: 25px;">
                                <asp:Literal ID="ltrAddress" runat="server"></asp:Literal>
                            </div>
                            <%--<div class="p14 ft13" id="dvNameDoctor" runat="server" style="font-family: HELVETICA; font-size: 16px; font-weight: bold">4CLEAR ORTHO</div>
                            <p class="p15 ft13">481 Garrisonville Rd Ste 101 Stafford, VA 22554 USA</p>
                            <p class="p16 ft13"><nobr>540.657-4333</nobr></p>
                            <p class="p17 ft14"><a href="http://www.4ClearOrtho.com">www.4ClearOrtho.com</a></p>--%>
                        </div>
                        <div id="id_3">
                            <p class="p18 ft14">Clear Brackets</p>
                            <p class="p19 ft15">Traditional brackets provide a more comprehensive orthodontic treatment. Compliance is not an issue.</p>
                            <p class="p20 ft14">General Orthodontic Benefits</p>
                            <p class="p21 ft15">There is a proven correlation between your tooth health and your overall health. The straighter your teeth, the easier for you to care for them. Some benefits include:</p>
                            <p class="p22 ft18"><span class="ft16">&bull;</span><span class="ft17">Heart health</span></p>
                            <p class="p22 ft18"><span class="ft16">&bull;</span><span class="ft17">Lower cancer incidents</span></p>
                            <p class="p23 ft18"><span class="ft16">&bull;</span><span class="ft17">Healthier pregnancies</span></p>
                            <p class="p24 ft20"><span class="ft16">&bull;</span><span class="ft19">Better blood sugar control</span></p>
                            <p class="p23 ft18"><span class="ft16">&bull;</span><span class="ft17">Less stroke occurrence</span></p>
                            <p class="p22 ft18">
                                <span class="ft16">&bull;</span><span class="ft17">Less periodontal disease (gum
                                <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;disease)</span>
                            </p>
                            <p class="p25 ft20"><span class="ft16">&bull;</span><span class="ft19">Less dental decay</span></p>
                            <p class="p23 ft18"><span class="ft16">&bull;</span><span class="ft17">Happier psychology</span></p>
                            <%--<p class="p26 ft18"><span class="ft16">&bull;</span><span class="ft17">Whitening included with each case</span></p>--%>
                            <p class="p27 ft5">I DIDN'T KNOW HOW IMPORTANT TO MY HEALTH HAVING STRAIGHT TEETH WAS. MY DOCTOR NOT ONLY GAVE ME A GREAT SMILE, BUT MADE IT EASY TO TAKE OF IT ALSO</p>
                            <p class="p28 ft6">- Marlon Egin Whittier. ca</p>
                        </div>
                    </div>
                </div>
                <br clear="all" style="page-break-before: always" />
                <div id="page_2" style="margin-left: 50px">
                    <div id="dimg1">
                        <img src="Content/images/brochure2.jpg" id="img2">
                    </div>
                    <div class="dclr"></div>
                    <div>
                        <div id="id_1">
                            <p class="p29 ft0">4CLEAR ORTHO</p>
                            <p class="p30 ft1">(540)657-4333</p>
                            <p class="p30 ft1">www.4clearortho.com</p>
                            <p class="p31 ft21"></p>
                            <p class="p32 ft22">Clear Trays</p>
                            <%--<p class="p33 ft23">our 4ClearOrtho Referral Plan. Each time</p>--%>
                            <p class="p50 ft26">CLEAR TRAYS ARE VIRTUALLY INVISIBLE AND CONVENIENT FOR YOU BUSY LIFESTYLE</p>
                            <p class="p51 ft27">The Clear tray system allows you to straighten your teeth with a series of clear plastic trays. You visit your dentist only once a month to verify that your treatment is progressing smoothly.</p>
                            <p class="p52 ft27">Once completed, usually less then 6 months, you can whiten your teeth using your 4clear ortho trays. You will then wear retainers to protect your teeth and their alignment. All this is virtually hassel free.</p>
                        </div>
                        <div id="id_2">
                            <p class="p34 ft13">4CLEAR ORTHO’S COMMITMENT</p>
                            <p class="p35 ft14">Certification</p>
                            <p class="p36 ft15">Your dentist has been trained and certified by  The American Academy of Dentistry.  They will have online access to continuing education. This allows you to have access to the most complete orthodontic solution.</p>
                            <p class="p35 ft14">Made in America</p>
                            <p class="p37 ft24">Your cases are analyzed and made in America.  Your doctor will mail your impressions or models with a prescription to the 4clear ortho facility. In as little as 3 weeks you will be ready to begin treatment</p>
                            <p class="p38 ft14">Affordable Payment Plans</p>
                            <p class="p39 ft15">Your doctor can offer affordable payment plans.  This creates access for everyone to have that smile they have always wanted.</p>
                            <p class="p40 ft14">Expert Assistance</p>
                            <p class="p41 ft15">We take the confusion out of orthodontics. After your doctor analyzes your case, we fabricate your case to achieve his goals in the shortest amount of time.</p>
                        </div>
                        <div id="id_3">
                            <p class="p42 ft13">www.4edental.com</p>
                            <p class="p43 ft14">CLEAR TRAY BENEFITS</p>
                            <p class="p44 ft15">There are no food restrictions You can brush and floss normally so you are healthier. You can remove the trays for important occasions. Comparable prices to traditional braces</p>
                            <p class="p45 ft14">Teen Ortho</p>
                            <p class="p46 ft15">Most cases  can be completed in a 5-6 month time period. If your teen is reliable, 4clear ortho trays are a healthier way to straighten your teeth.</p>
                            <p class="p47 ft14">Grinding and Clinching</p>
                            <p class="p48 ft24">Did you know that the clear trays have been known to lessen and even prevent headaches?. Your doctor will can show you that not only is it healthier for straight teeth, but the trays also protect your teeth and muscles of mastication.</p>
                            <p class="p38 ft14">QUESTIONS?</p>
                            <p class="p49 ft25">Do you have questions about orthodontics in general?  Your dentist can help you decide which system is ideal for you.  4Clear ortho is your partner in your complete health.</p>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
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
    </form>
</body>
</html>
