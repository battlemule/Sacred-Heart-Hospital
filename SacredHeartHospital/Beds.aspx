<%--*****************************************************************
    * Beds.aspx                                          v1.2 11/2016
    * Sacred Heart Hospital                             Robert Willis
    *
    * Webform showing grid view of all beds in hospital database.
    *****************************************************************--%>
<%@ Page Title="Beds List" Language="C#" 
    MasterPageFile="~/SacredHeartHospital.Master" AutoEventWireup="true" 
    CodeBehind="Beds.aspx.cs" Inherits="SacredHeartHospital.Beds" %>

<%-- Content Placeholder --%>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <%-- Page Heading --%>
    <h2>Beds List</h2>

    <%-- Page Content --%>
    <%-- Grid View --%>
    <div id="gridViewTable">
        <asp:GridView runat="server" ID="BedGridView" AutoGenerateColumns="false">
            <Columns>
                <%-- Bed ID --%>
                <asp:BoundField ShowHeader="true" DataField="id" HeaderText="ID" />
                <%-- Bed Name --%>
                <asp:BoundField ShowHeader="true" DataField="name" HeaderText="Name" />
                <%-- Bed Rate --%>
                <asp:BoundField ShowHeader="true" DataField="rate" HeaderText="Rate Per Day" />
                <%-- Bed Type --%>
                <asp:BoundField ShowHeader="true" DataField="type" HeaderText="Type" />
            </Columns>
        </asp:GridView>
    </div>

    <p>
        <%-- Database error message --%>
        <asp:Literal runat="server" ID="DataBaseError" Text="Database Connection Error </br>" Visible="false"/>
    </p>

<%-- End Content --%>
</asp:Content>