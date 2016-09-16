<%--*****************************************************************
    * Registration.aspx                                  v1.0 11/2014
    * Sacred Heart Hospital                             Robert Willis
    *
    * Webform allowing registration of new patient and visit to
    * hospital database.
    *****************************************************************--%>
<%@ Page Title="Patient Registration" Language="C#" 
    MasterPageFile="~/WDAssignment2.Master" AutoEventWireup="true" 
    CodeBehind="Registration.aspx.cs" Inherits="WDAssignment2.Registration" %>

<%-- Content Placeholder --%>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <%-- Page Heading --%>
    <h1>Patient Registration</h1>
    <p>

        <%-- New of Visit radio button list toggles panel display for forms--%>
        <asp:RadioButtonList ID="SelectionRadioButtonList" runat="server" AutoPostBack="true" 
            OnSelectedIndexChanged="SelectionToggle" RepeatDirection="horizontal">
            <asp:ListItem>New Patient</asp:ListItem>
            <asp:ListItem>New Visit</asp:ListItem>
        </asp:RadioButtonList>


        <%-- Add New Patient Panel --%>
        <asp:Panel ID="NewPatientPanel" runat="server" Visible="false">

            <%-- Name Textbox --%>
            <label for="name">Name:</label>
            <asp:TextBox runat="server" ID="name" ClientIDMode="Static" />
            <%-- Validate text entered --%>
            <asp:RequiredFieldValidator ID="nameRequiredValidator" runat="server" ForeColor="#B6121B" Width="100"
                                        ErrorMessage="Name Required" ControlToValidate="name" Display="dynamic"/>
            <%-- Validate string is of form "First Surname" (at least 1 char for either name, max 50 chars total) --%>
            <asp:RegularExpressionValidator ID="nameValidator" runat="server" ForeColor="#B6121B" Width="100"
                                            ErrorMessage="Input Incorrect. 'Name Surname'." ControlToValidate="name"
                                            ValidationExpression="^[a-zA-Z]{1,25}\s{1}[a-zA-Z]{1,25}" Display="Dynamic" />
            <br />

            <%-- Address Textbox --%>
            <label for="address">Address:</label>
            <asp:TextBox runat="server" ID="address" ClientIDMode="Static" />
            <%-- Validate text entered --%>
            <asp:RequiredFieldValidator ID="addressRequiredValidator" runat="server" ForeColor="#B6121B" Width="100"
                                        ErrorMessage="Address Required" ControlToValidate="address" Display="dynamic"/>
            <%-- Validate string is of form "1 Example St" (1 int, space, word, space, word, anything, end. Max 247 chars.) --%>
            <asp:RegularExpressionValidator ID="addressValidator" runat="server" ForeColor="#B6121B" Width="100"
                                            ErrorMessage="Input Incorrect. '1 Example St'." ControlToValidate="address"
                                            ValidationExpression="^[1-9]{1,5}\s{1}[a-zA-z]{1,100}\s{1}[a-zA-Z]{1,100}(.?){0,30}$" 
                                            Display="Dynamic" />
            <br />

            <%-- Date of Birth Textbox --%>
            <label for="dob">Date of Birth:</label>
            <asp:TextBox runat="server" ID="dob" ClientIDMode="Static" />
            <%-- Validate text entered --%>
            <asp:RequiredFieldValidator ID="dobRequiredValidator" runat="server" ForeColor="#B6121B" Width="100"
                                        ErrorMessage="DOB Required" ControlToValidate="dob" Display="dynamic"/>
            <%-- Validate string is of form MM/DD/YYYY --%>
            <asp:RegularExpressionValidator ID="dobValidator" runat="server" ForeColor="#B6121B" Width="100"
                                            ErrorMessage="Input Incorrect. MM/DD/YYYY." ControlToValidate="dob"
                                            ValidationExpression="^[0-9]{2}\/{1}[0-9]{2}\/{1}([0-9]{4})$" Display="Dynamic" />
            <%-- Validate Date is 1900-2014 with correct dates and months --%>
            <asp:CustomValidator ID="dobValidator1" runat="server" ForeColor="#B6121B" Width="100"
                                 ErrorMessage="Incorrect Date. MM/DD/YYYY." ControlToValidate="dob"
                                 OnServerValidate="dateValidate" Display="Dynamic"/>
            <br />

            <%-- Phone Textbox --%>
            <label for="phone">Phone:</label>
            <asp:TextBox runat="server" ID="phone" ClientIDMode="Static" />
            <%-- Validate text entered --%>
            <asp:RequiredFieldValidator ID="phoneRequiredValidator" runat="server" ForeColor="#B6121B" Width="100"
                                        ErrorMessage="Phone Required" ControlToValidate="phone" Display="dynamic"/>
            <%-- Validate string is of form "0123456789" (10 digits) --%>
            <asp:RegularExpressionValidator ID="phoneValidator" runat="server" ForeColor="#B6121B" Width="100"
                                            ErrorMessage="Input Incorrect. 10 digits." ControlToValidate="phone"
                                            ValidationExpression="^[0-9]{10}$" Display="Dynamic" />
            <br />

            <%-- Emergency Contact Textbox --%>
            <label for="econtact">Emergency Contact:</label>
            <asp:TextBox runat="server" ID="econtact" ClientIDMode="Static" />
            <%-- Validate text entered --%>
            <asp:RequiredFieldValidator ID="emergencyRequiredValidator" runat="server" ForeColor="#B6121B" Width="100"
                                        ErrorMessage="Emergency Contact Required" ControlToValidate="econtact" Display="dynamic"/>
            <%-- Validate string is less than 255 chars --%>
            <asp:RegularExpressionValidator ID="emergencyValidator" runat="server" ForeColor="#B6121B" Width="100"
                                            ErrorMessage="Input Incorrect. Max 255 chars." ControlToValidate="econtact"
                                            ValidationExpression=".{1,255}" Display="Dynamic" />
            <br />

            <%-- Registration Date Textbox --%>
            <label for="registration">Registration Date:</label>
            <asp:TextBox runat="server" ID="registration" ClientIDMode="Static" />
            <%-- Validate text entered --%>
            <asp:RequiredFieldValidator ID="registrationRequiredValidator" runat="server" ForeColor="#B6121B" Width="100"
                                        ErrorMessage="Registration Date Required" ControlToValidate="registration" Display="dynamic"/>
            <%-- Validate string is of form 11/11/1111 --%>
            <asp:RegularExpressionValidator ID="registrationValidator" runat="server" ForeColor="#B6121B" Width="100"
                                            ErrorMessage="Input Incorrect. 11/11/1111." ControlToValidate="registration"
                                            ValidationExpression="^[0-9]{2}\/{1}[0-9]{2}\/{1}([0-9]{4})$" Display="Dynamic" />
            <%-- Validate Date is 1900-2014 with correct dates and months --%>
            <asp:CustomValidator ID="registrationValidator1" runat="server" ForeColor="#B6121B" Width="100"
                                 ErrorMessage="Incorrect Date. MM/DD/YYYY." ControlToValidate="registration"
                                 OnServerValidate="dateValidate" Display="Dynamic"/>
            <br />

            <%-- Submit --%>
            <asp:Button runat="server" ID="PatientSubmit" OnClick="PatientClick" Text="Register Patient" />

            <%-- Success Message --%>
            <asp:Literal runat="server" ID="PatientSuccessMessage" Text="Patient Registered. </br>" Visible="false"/>

            <%-- Error Message --%>
            <asp:Literal runat="server" ID="PatientErrorMessage" Text="Patient Registration Failed. </br>" Visible="false"/>

        </asp:Panel>

        <%-- Add Visit Panel --%>
        <asp:Panel ID="NewVisitPanel" runat="server" Visible="false">

            <%-- Get Patient Id --%>
            <label for="id">Patient ID:</label>
            <asp:TextBox runat="server" ID="id" ClientIDMode="Static" />
            <%-- Validate Text Entered --%>
            <asp:RequiredFieldValidator ID="idRequiredValidator" runat="server" ForeColor="#B6121B" Width="100"
                                        ErrorMessage="ID Required" ControlToValidate="id" Display="Dynamic"/>
            <%-- Validate ID Exists --%>
            <asp:CustomValidator ID="idValidator" runat="server" ForeColor="#B6121B" Width="100"
                                 ErrorMessage="Provided ID Does Not Exist" ControlToValidate="id"
                                 OnServerValidate="idValidate" Display="Dynamic"/>
            <br />

            <%-- Doctor Textbox --%>
            <label for="doctor">Doctors ID:</label>
            <asp:TextBox runat="server" ID="doctor" ClientIDMode="Static" />
            <%-- Validate Text Entered --%>
            <asp:RequiredFieldValidator ID="doctorRequiredValidator" runat="server" ForeColor="#B6121B" Width="100"
                                        ErrorMessage="Doctor ID Required" ControlToValidate="doctor" Display="Dynamic"/>
            <%-- Validate Doctor Exists --%>
            <asp:CustomValidator ID="doctorValidator" runat="server" ForeColor="#B6121B" Width="100"
                                 ErrorMessage="Provided Doctor ID Does Not Exist" ControlToValidate="doctor"
                                 OnServerValidate="doctorValidate" Display="Dynamic"/>
            <br />

            <%-- Visit Date Textbox --%>
            <label for="visit">Date of Visit:</label>
            <asp:TextBox runat="server" ID="visit" ClientIDMode="Static" />
            <%-- Validate text entered --%>
            <asp:RequiredFieldValidator ID="visitRequiredValidator" runat="server" ForeColor="#B6121B" Width="100"
                                        ErrorMessage="Visit Date Required" ControlToValidate="visit" Display="dynamic"/>
            <%-- Validate string is of form 11/11/1111 --%>
            <asp:RegularExpressionValidator ID="visitValidator" runat="server" ForeColor="#B6121B" Width="100"
                                            ErrorMessage="Input Incorrect. 11/11/1111." ControlToValidate="visit"
                                            ValidationExpression="^[0-9]{2}\/{1}[0-9]{2}\/{1}([0-9]{4})$" Display="Dynamic" />
            <%-- Validate Date is 1900-2014 with correct dates and months --%>
            <asp:CustomValidator ID="visitValidator1" runat="server" ForeColor="#B6121B" Width="100"
                                 ErrorMessage="Incorrect Date. MM/DD/YYYY." ControlToValidate="visit"
                                 OnServerValidate="dateValidate" Display="Dynamic"/>
            <br />

            <%-- Radio button toggles display for inpatient fields --%>
            <asp:RadioButtonList ID="RadioButtonList2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="typeToggle" 
                                 RepeatDirection="horizontal">
                <asp:ListItem Selected="True">OutPatient</asp:ListItem>
                <asp:ListItem>InPatient</asp:ListItem>
            </asp:RadioButtonList>

            <%-- inpatientPanel --%>
            <asp:Panel ID="inpatientPanel" runat="server" Visible="false">

                <%-- Bed Textbox --%>
                <label for="bed">Bed:</label>
                <asp:TextBox runat="server" ID="bed" ClientIDMode="Static" />
                <%-- Validate Text Entered --%>
                <asp:RequiredFieldValidator ID="bedRequiredValidator" runat="server" ForeColor="#B6121B" Width="100"
                                            ErrorMessage="Bed ID Required" ControlToValidate="bed" Display="Dynamic"/>
                <%-- Validate Bed Exists --%>
                <asp:CustomValidator ID="bedValidator" runat="server" ForeColor="#B6121B" Width="100"
                                     ErrorMessage="Provided Bed ID Does Not Exist" ControlToValidate="bed"
                                     OnServerValidate="bedValidate" Display="Dynamic"/>
                <br />

                <%-- Discharge Textbox --%>
                <label for="discharge">Date of Discharge:</label>
                <asp:TextBox runat="server" ID="discharge" ClientIDMode="Static" />
                <%-- Validate text entered --%>
                <asp:RequiredFieldValidator ID="dischargeRequiredValidator" runat="server" ForeColor="#B6121B" Width="100"
                                            ErrorMessage="Discharge Date Required" ControlToValidate="discharge" Display="dynamic"/>
                <%-- Validate string is of form 11/11/1111 --%>
                <asp:RegularExpressionValidator ID="dischargeValidator" runat="server" ForeColor="#B6121B" Width="100"
                                                    ErrorMessage="Input Incorrect. 11/11/1111." ControlToValidate="discharge"
                                                    ValidationExpression="^[0-9]{2}\/{1}[0-9]{2}\/{1}([0-9]{4})$" Display="Dynamic" />
            <%-- Validate Date is 1900-2014 with correct dates and months --%>
            <asp:CustomValidator ID="dischargeValidator1" runat="server" ForeColor="#B6121B" Width="100"
                                 ErrorMessage="Incorrect Date. MM/DD/YYYY." ControlToValidate="discharge"
                                 OnServerValidate="dateValidate" Display="Dynamic"/>
                <br />
            </asp:Panel>

            <%-- Submit --%>
            <asp:Button runat="server" ID="VisitSubmitButton" OnClick="VisitClick" Text="Register Visit" />

            <%-- Success Message --%>
            <asp:Literal runat="server" ID="VisitSuccessMessage" Text="Visit Registered. </br>" Visible="false"/>

            <%-- Error Message --%>
            <asp:Literal runat="server" ID="VisitErrorMessage" Text="Visit Registration Failed. </br>" Visible="false"/>

        </asp:Panel>

    </p>

</asp:Content>
