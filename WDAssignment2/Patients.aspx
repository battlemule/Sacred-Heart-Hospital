<%--*****************************************************************
    * Patients.aspx                                      v1.0 11/2014
    * Sacred Heart Hospital                             Robert Willis
    *
    * Webform showing grid view of all patients in hospital database.
    *****************************************************************--%>
<%@ Page Title="Patients List" Language="C#" 
    MasterPageFile="~/WDAssignment2.Master" AutoEventWireup="true" 
    CodeBehind="Patients.aspx.cs" Inherits="WDAssignment2.Patients" %>

<%-- Content Placeholder --%>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <%-- Page Heading --%>
    <h1>Patients List</h1>

    <p>
        <%-- Search textbox --%>
        <label for="Search">Search:</label>
        <asp:TextBox runat="server" ID="Search" ClientIDMode="Static" />
        <%-- Vaidate input given --%>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="#B6121B" Width="100"
                                        ErrorMessage="Name Required" ControlToValidate="Search" />
    </p>

    <p>
        <%-- No results found error message --%>
        <asp:Literal runat="server" ID="ErrorMessage" Text="No Results Found. </br>" Visible="false"/>
    </p> 

    <%-- Submit button--%>
    <asp:Button runat="server" ID="SubmitButton" OnClick="SearchClick" Text="Search" />

    <%-- Grid View --%>
    <asp:GridView id="GridView1" runat="server" />

<%-- End Content --%>
</asp:Content>
