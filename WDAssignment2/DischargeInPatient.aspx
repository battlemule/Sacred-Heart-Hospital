<%--*****************************************************************
    * DischargeInPatient.aspx                            v1.0 11/2014
    * Sacred Heart Hospital                             Robert Willis
    *
    * Webform allowing user to discharge a current inpatient.
    *****************************************************************--%>
<%@ Page Title="Discharge Inpatient" Language="C#" 
    MasterPageFile="~/WDAssignment2.Master" AutoEventWireup="true" 
    CodeBehind="DischargeInPatient.aspx.cs" Inherits="WDAssignment2.DischargeInPatient" 
    EnableEventValidation="false"%>

<%-- Content Placeholder --%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <%-- Page Header --%>
    <h1>Discharge Inpatient</h1>

    <p>

        <%-- As User fills out text box's panels will hide and show based
             on which information is provided --%>
        <%-- Patient ID Textbox --%>
        <label for="PatientId">Patient ID:</label>
        <asp:TextBox runat="server" ID="PatientId" ClientIDMode="Static" />
        <%-- Validate text entered --%>
        <asp:RequiredFieldValidator ID="PatientRequiredValidator" runat="server" ForeColor="#B6121B" Width="100"
                                    ErrorMessage="Patient Required" ControlToValidate="PatientId" Display="dynamic"/>
        <%-- Validate string is of form "111" (at least 1 number, max 3) --%>
        <asp:RegularExpressionValidator ID="PatientRegexValidator" runat="server" ForeColor="#B6121B" Width="100"
                                        ErrorMessage="Input Incorrect. Numbers only, 3 Numbers Max." ControlToValidate="PatientId"
                                        ValidationExpression="^[1-9]{1,3}$" Display="Dynamic" />
        <%-- Validate Patient Exists and is current inpatient--%>
        <asp:CustomValidator ID="PatientIdValidator" runat="server" ForeColor="#B6121B" Width="100"
                             ErrorMessage="Patient ID does not exist." ControlToValidate="PatientId"
                             OnServerValidate="PatientIdValidate" Display="Dynamic"/>
        <br />

        <%-- Submit Button --%>
        <asp:Button runat="server" ID="InfoSubmitButton" OnClick="InfoClick" Text="Search" />

        <%-- Error Message --%>
        <asp:Literal runat="server" ID="InfoErrorMessage" Text="No Patients Found. </br>" Visible="false"/>


        <%-- If successfull show results panel with gridview --%>
        <%-- Results Gridview --%>
        <asp:GridView runat="server" ID="InpatientGridView" Visible="false" AutoGenerateColumns="false">
            <Columns>
                <%-- Visit ID --%>
                <asp:BoundField ShowHeader="true" DataField="id" HeaderText="Visit ID" />
                <%-- Patient Name --%>
                <asp:TemplateField HeaderText="Patient Name">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# GetPatientName(Eval("patientId")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <%-- Patient Type --%>
                <asp:TemplateField HeaderText="Patient Type">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# GetPatientType(Eval("type")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <%-- Doctor --%>
                <asp:TemplateField HeaderText="Doctor">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# GetDoctorName(Eval("doctor")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <%-- Visit Date --%>
                <asp:BoundField ShowHeader="true" DataField="date" HeaderText="Visit Date" />
                <%-- Discharge Date --%>
                <asp:TemplateField HeaderText="Discharge Date">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# GetDischargeDate(Eval("discharge")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <%-- Bed ID --%>
                <asp:TemplateField HeaderText="Bed Name">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# GetBedId(Eval("bed")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <%-- Price per day --%>
                <asp:TemplateField HeaderText="Price Per Day">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# GetPrice(Eval("bed")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <br/>

        <%-- Amount owing label --%>
        <asp:Label runat="server" ID="AmountOwingLabel" Visible="false"/>

        <br />
        <br />

        <%-- Panel with Pay button --%>
        <asp:Panel runat="server" ID="DischargePanel" Visible="false">

            <%-- Credit Card Number --%>
            <label for="CCNumber">Credit Card Number:</label>
            <asp:TextBox runat="server" ID="CCNumber" ClientIDMode="Static" />
            <br />

            <%-- Credit Card Name --%>
            <label for="CCName">Credit Card Name:</label>
            <asp:TextBox runat="server" ID="CCName" ClientIDMode="Static" />
            <br />

            <%-- Credit Card Expiry --%>
            <label for="CCExpiry">Credit Card Expiry:</label>
            <asp:TextBox runat="server" ID="CCExpiry" ClientIDMode="Static" />
            <br />

            <%-- Credit Card CSV --%>
            <label for="CSV">Credit Card CSV:</label>
            <asp:TextBox runat="server" ID="CSV" ClientIDMode="Static" />
            <br />

        </asp:Panel>

        <br />


        <%-- Pay Now Button --%>
        <asp:Button runat="server" ID="PayButton" Text="Pay Now" ClientIDMode="Static" OnClick="PayClick" Visible="false"/>
        <%-- Error Message --%>
        <asp:Literal runat="server" ID="PayErrorMessage" Text="Payment Failed. </br>" Visible="false"/>
        <%-- Success Message --%>
        <asp:Literal runat="server" ID="PaySuccessMessage" Text="Payment Successfull. </br>" Visible="false"/>

    </p>

</asp:Content>
