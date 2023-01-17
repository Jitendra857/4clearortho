<%@ Page Title="" Language="C#" MasterPageFile="~/Ortho.Master" AutoEventWireup="true" CodeBehind="ClientTestimonials.aspx.cs" Inherits="_4eOrtho.ClientTestimonials" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upClientTestimonials" runat="server">
        <ContentTemplate>
            <div class="left_title">
                <div class="title">
                    <div class="supply-button2 back">
                        <asp:Button ID="btnAddTestimonail" runat="server" Text="Add Testimonial" OnClientClick="loadingoverlayShow()" meta:resourcekey="btnAddTestimonail" />
                    </div>
                    <h2><%= this.GetLocalResourceObject("Testimonial") %></h2>
                </div>
            </div>
            <asp:Repeater ID="rptClientTestimonial" runat="server">
                <ItemTemplate>
                    <div style="font-style: italic; font-size: 16px;">
                        "<%# Eval("PageContent")%>"
                    </div>
                    <div style="float: right">
                        <%# String.Format("{0} {1}", System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Eval("FirstName").ToString()), System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Eval("LastName").ToString())) %>
                    </div>
                </ItemTemplate>
                <SeparatorTemplate>
                    <br />
                    <br />
                </SeparatorTemplate>
            </asp:Repeater>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
