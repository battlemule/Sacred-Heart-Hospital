<%--*****************************************************************
    * Default.aspx                                       v1.2 09/2016
    * Sacred Heart Hospital                             Robert Willis
    *
    * Landing Page for Sacred Heart Hospital. Requests User logs in.
    *****************************************************************--%>
<%@ Page Title="Home" Language="C#" 
    MasterPageFile="~/SacredHeartHospital.Master" AutoEventWireup="true" 
    CodeBehind="Default.aspx.cs" Inherits="SacredHeartHospital.Default" %>

<%-- Content Placeholder --%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <%-- Page Header --%>
    <%-- Sacred given css id for targeted font colour change --%>
    <div id="contentHeading">
        <h1><p id="sacred">Sacred</p> Heart Hospital</h1>
        <h2>Patient Booking System</h2>
    </div>

    <%-- Page Content --%>
    <%-- Login Request --%>
    <div id="contentBody">
        <p>Please login to begin</p>
    </div>

    <%-- Stethoscope Image --%>
    <div id="stethoscope">
        <asp:Image ImageUrl="Images/stethoscope.png" 
            AlternateText="stethoscope" runat="server" />
    </div>

<%-- End Content --%>
</asp:Content>
