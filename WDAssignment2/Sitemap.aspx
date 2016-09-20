<%--*****************************************************************
    * Sitemap.aspx                                       v1.2 09/2016
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
    <h2>Site Map</h2>

    <%-- Site Map --%>
    <div id="gridViewTable">
        <%-- Site Map Tree View --%>
        <asp:TreeView ID="TreeView1" runat="server" DataSourceID="SiteMapDataSource1" ShowLines="True" />
        <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" />
    </div>

<%-- End Content --%>
</asp:Content>
