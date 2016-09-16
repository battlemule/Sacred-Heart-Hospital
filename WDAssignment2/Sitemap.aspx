<%--*****************************************************************
    * Sitemap.aspx                                       v1.0 11/2014
    * Sacred Heart Hospital                             Robert Willis
    *
    * Webform showing tree view of all pages in hospital system.
    *****************************************************************--%>
<%@ Page Title="Site Map" Language="C#" 
    MasterPageFile="~/WDAssignment2.Master" AutoEventWireup="true" 
    CodeBehind="Sitemap.aspx.cs" Inherits="WDAssignment2.Sitemap" %>

<%-- Content Placeholder --%>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <%-- Page Heading --%>
    <h1>Site Map</h1>
    <p>
        <%-- Site Map Tree View --%>
        <asp:TreeView ID="TreeView1" runat="server" DataSourceID="SiteMapDataSource1" ShowLines="True" />
        <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" />
    </p>

<%-- End Content --%>
</asp:Content>
