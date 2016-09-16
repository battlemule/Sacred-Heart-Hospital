<%--*****************************************************************
    * Beds.aspx                                          v1.0 11/2014
    * Sacred Heart Hospital                             Robert Willis
    *
    * Webform showing grid view of all beds in hospital database.
    *****************************************************************--%>
<%@ Page Title="Beds List" Language="C#" 
    MasterPageFile="~/WDAssignment2.Master" AutoEventWireup="true" 
    CodeBehind="Beds.aspx.cs" Inherits="WDAssignment2.Beds" %>

<%-- Content Placeholder --%>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <%-- Page Heading --%>
    <h1>Beds List</h1>

    <%-- Page Content --%>
    <%-- Grid View --%>
    <asp:GridView id="GridView1" runat="server" />

<%-- End Content --%>
</asp:Content>