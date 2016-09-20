<%--*****************************************************************
    * Login.aspx                                         v1.2 09/2016
    * Sacred Heart Hospital                             Robert Willis
    *
    * Login page for Sacred Heart Hospital.
    *****************************************************************--%>
<%@ Page Title="" Language="C#" 
    MasterPageFile="~/WDAssignment2.Master" AutoEventWireup="true" 
    CodeBehind="Login.aspx.cs" Inherits="WDAssignment2.Login" %>

<%-- Content Placeholder --%>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <%-- Page Heading --%>
    <h2>Login</h2>

    <p>
        <%-- Username textbox --%>
        <label for="Username">Username:</label>
        <asp:TextBox runat="server" ID="Username" ClientIDMode="Static" />
        <br />
        <%-- Vaidate input given --%>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="#B6121B" Width="300"
                                        ErrorMessage="Username Required" ControlToValidate="Username" />
        <br />
        <%-- Password textbox --%>
        <label for="Password">Password:</label>
        <asp:TextBox runat="server" ID="Password" TextMode="Password" ClientIDMode="Static" />
        <br />
        <%-- Vaidate input given --%>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="#B6121B" Width="300"
                                    ErrorMessage="Password Required" ControlToValidate="Password" />
    </p>

    <p>
        <%-- Invalid credentials error message --%>
        <asp:Literal runat="server" ID="InvalidLoginError" Text="Invalid Login Credentials </br>" Visible="false"/>
    </p>

    <p>
        <%-- Database error message --%>
        <asp:Literal runat="server" ID="DataBaseError" Text="Database Connection Error </br>" Visible="false"/>
    </p>
     
    <%-- Submit button--%>
    <asp:Button runat="server" ID="SubmitButton" OnClick="LoginClick" Text="Login" />

<%-- End Content --%>
</asp:Content>
