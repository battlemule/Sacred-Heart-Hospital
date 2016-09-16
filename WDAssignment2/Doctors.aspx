<%--*****************************************************************
    * Doctors.aspx                                       v1.0 11/2014
    * Sacred Heart Hospital                             Robert Willis
    *
    * Webform showing grid view of all doctors in hospital database.
    *****************************************************************--%>
<%@ Page Title="Doctors List" Language="C#" 
    MasterPageFile="~/WDAssignment2.Master" AutoEventWireup="true" 
    CodeBehind="Doctors.aspx.cs" Inherits="WDAssignment2.Doctors" %>

<%-- Content Placeholder --%>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
   
     <%-- Page Header --%>
    <h1>Doctors List</h1>
    
    <%-- Page Content --%>
    <%-- Grid View --%>
    <asp:GridView id="GridView1" runat="server" />

<%-- End Content --%>
</asp:Content>
