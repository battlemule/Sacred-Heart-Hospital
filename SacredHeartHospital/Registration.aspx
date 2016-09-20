<%--*****************************************************************
    * Registration.aspx                                  v1.2 09/2016
    * Sacred Heart Hospital                             Robert Willis
    *
    * Webform allowing registration of new patient and visit to
    * hospital database.
    *****************************************************************--%>
<%@ Page Title="Patient Registration" Language="C#" 
    MasterPageFile="~/SacredHeartHospital.Master" AutoEventWireup="true" 
    CodeBehind="Registration.aspx.cs" Inherits="SacredHeartHospital.Registration" %>

<%-- Content Placeholder --%>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <%-- Page Heading --%>
    <h2>Patient Registration</h2>
    <p>

        <%-- Name Textbox --%>
        <label for="name">Name:</label>
        <br />
        <asp:TextBox runat="server" ID="name" ClientIDMode="Static" />
        <br />
        <%-- Validate text entered --%>
        <asp:RequiredFieldValidator ID="nameRequiredValidator" runat="server" ForeColor="#B6121B" Width="300"
                                    ErrorMessage="Name Required" ControlToValidate="name" Display="dynamic"/>
        <%-- Validate string is of form "First Surname" (at least 1 char for either name, max 50 chars total) --%>
        <asp:RegularExpressionValidator ID="nameValidator" runat="server" ForeColor="#B6121B" Width="300"
                                        ErrorMessage="Input Incorrect. 'Name Surname'." ControlToValidate="name"
                                        ValidationExpression="^[a-zA-Z]{1,25}\s{1}[a-zA-Z]{1,25}" Display="Dynamic" />
        <br />

        <%-- Address Textbox --%>
        <label for="address">Address:</label>
        <br />
        <asp:TextBox runat="server" ID="address" ClientIDMode="Static" />
        <br />
        <%-- Validate text entered --%>
        <asp:RequiredFieldValidator ID="addressRequiredValidator" runat="server" ForeColor="#B6121B" Width="300"
                                    ErrorMessage="Address Required" ControlToValidate="address" Display="dynamic"/>
        <%-- Validate string is of form "1 Example St" (1 int, space, word, space, word, anything, end. Max 247 chars.) --%>
        <asp:RegularExpressionValidator ID="addressValidator" runat="server" ForeColor="#B6121B" Width="300"
                                        ErrorMessage="Input Incorrect. '1 Example St'." ControlToValidate="address"
                                        ValidationExpression="^[1-9]{1,5}\s{1}[a-zA-z]{1,100}\s{1}[a-zA-Z]{1,100}(.?){0,30}$" 
                                        Display="Dynamic" />
        <br />

        <%-- Date of Birth Textbox --%>
        <label for="dob">Date of Birth (MM/DD/YYYY):</label>
        <br />
        <asp:TextBox runat="server" ID="dob" ClientIDMode="Static" />
        <br />
        <%-- Validate text entered --%>
        <asp:RequiredFieldValidator ID="dobRequiredValidator" runat="server" ForeColor="#B6121B" Width="300"
                                    ErrorMessage="DOB Required" ControlToValidate="dob" Display="dynamic"/>
        <%-- Validate string is of form MM/DD/YYYY --%>
        <asp:RegularExpressionValidator ID="dobValidator" runat="server" ForeColor="#B6121B" Width="300"
                                        ErrorMessage="Input Incorrect. MM/DD/YYYY." ControlToValidate="dob"
                                        ValidationExpression="^[0-9]{2}\/{1}[0-9]{2}\/{1}([0-9]{4})$" Display="Dynamic" />
        <%-- Validate Date is 1900-2014 with correct dates and months --%>
        <asp:CustomValidator ID="dobValidator1" runat="server" ForeColor="#B6121B" Width="300"
                             ErrorMessage="Incorrect Date. MM/DD/YYYY." ControlToValidate="dob"
                             OnServerValidate="dateValidate" Display="Dynamic"/>
        <br />

        <%-- Phone Textbox --%>
        <label for="phone">Phone:</label>
        <br />
        <asp:TextBox runat="server" ID="phone" ClientIDMode="Static" />
        <br />
        <%-- Validate text entered --%>
        <asp:RequiredFieldValidator ID="phoneRequiredValidator" runat="server" ForeColor="#B6121B" Width="300"
                                    ErrorMessage="Phone Required" ControlToValidate="phone" Display="dynamic"/>
        <%-- Validate string is of form "0123456789" (10 digits) --%>
        <asp:RegularExpressionValidator ID="phoneValidator" runat="server" ForeColor="#B6121B" Width="300"
                                        ErrorMessage="Input Incorrect. 10 digits." ControlToValidate="phone"
                                        ValidationExpression="^[0-9]{10}$" Display="Dynamic" />
        <br />

        <%-- Emergency Contact Textbox --%>
        <label for="econtact">Emergency Contact:</label>
        <br />
        <asp:TextBox runat="server" ID="econtact" ClientIDMode="Static" />
        <br />
        <%-- Validate text entered --%>
        <asp:RequiredFieldValidator ID="emergencyRequiredValidator" runat="server" ForeColor="#B6121B" Width="300"
                                    ErrorMessage="Emergency Contact Required" ControlToValidate="econtact" Display="dynamic"/>
        <%-- Validate string is less than 255 chars --%>
        <asp:RegularExpressionValidator ID="emergencyValidator" runat="server" ForeColor="#B6121B" Width="300"
                                        ErrorMessage="Input Incorrect. Max 255 chars." ControlToValidate="econtact"
                                        ValidationExpression=".{1,255}" Display="Dynamic" />
        <br />

        <%-- Submit --%>
        <asp:Button runat="server" ID="PatientSubmit" OnClick="PatientClick" Text="Register Patient" />
        <br />

        <%-- Success Message --%>
        <asp:Literal runat="server" ID="PatientSuccessMessage" Text="Patient Registered. </br>" Visible="false"/>

        <%-- Error Message --%>
        <asp:Literal runat="server" ID="PatientErrorMessage" Text="Patient Registration Failed. </br>" Visible="false"/>

        <%-- Error Message --%>
        <asp:Literal runat="server" ID="DatabaseErrorMessage" Text="Database Error. </br>" Visible="false"/>

    </p>

</asp:Content>
