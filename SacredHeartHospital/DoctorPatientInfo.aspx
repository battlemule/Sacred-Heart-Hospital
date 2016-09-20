<%--*****************************************************************
    * DoctorPatientInfo.aspx                             v1.2 09/2016
    * Sacred Heart Hospital                             Robert Willis
    *
    * Webform allowing user to assign a doctor to a patient
    * or revieve the treatment history of a patient. Each option is
    * on a different panel that hides/shows based on radio button
    * input.
    *****************************************************************--%>
<%@ Page Title="" Language="C#" 
    MasterPageFile="~/SacredHeartHospital.Master" AutoEventWireup="true" 
    CodeBehind="DoctorPatientInfo.aspx.cs" Inherits="SacredHeartHospital.DoctorPatientInfo" %>

<%-- Content Placeholder --%>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <%-- Page Header --%>
    <h2>Doctor Patient Info</h2>
    <p>
        <%-- Radio button list toggles which panels display for forms--%>
        <asp:RadioButtonList ID="SelectionRadioButtonList" runat="server" AutoPostBack="true" 
                             OnSelectedIndexChanged="SelectionToggle" RepeatDirection="horizontal"
                             CssClass="radioButtonList">
            <asp:ListItem>Assign Doctor</asp:ListItem>
            <asp:ListItem>Treatment History</asp:ListItem>
        </asp:RadioButtonList>
    </p>
    <p>
        <%-- Assign Doctor Panel --%>
        <asp:panel ID="AssignDoctorPanel" runat="server" Visible="false">

            <%-- Doctor ID Textbox --%>
            <label for="DoctorId">Doctor ID:</label>
            <asp:TextBox runat="server" ID="DoctorId" ClientIDMode="Static" />
            <%-- Validate text entered --%>
            <asp:RequiredFieldValidator ID="DoctorRequiredValidator" runat="server" ForeColor="#B6121B" Width="100"
                                        ErrorMessage="Doctor ID Required" ControlToValidate="DoctorId" Display="dynamic"/>
            <%-- Validate string is of form "111" (at least 1 number, max 3) --%>
            <asp:RegularExpressionValidator ID="DoctorRegexValidator" runat="server" ForeColor="#B6121B" Width="100"
                                            ErrorMessage="Input Incorrect. Numbers only, 3 Numbers Max." ControlToValidate="DoctorId"
                                            ValidationExpression="^[1-9]{1,3}$" Display="Dynamic" />
            <%-- Validate provided doctor ID exists in database --%>
            <asp:CustomValidator ID="DoctorIdValidator" runat="server" ForeColor="#B6121B" Width="100"
                                 ErrorMessage="Doctor ID does not exist." ControlToValidate="DoctorId"
                                 OnServerValidate="DoctorIdValidate" Display="Dynamic"/>
            <br />
            <br />

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
            <%-- Validate Patient Exists --%>
            <asp:CustomValidator ID="PatientIdValidator" runat="server" ForeColor="#B6121B" Width="100"
                                 ErrorMessage="Patient ID does not exist." ControlToValidate="PatientId"
                                 OnServerValidate="PatientIdValidate" Display="Dynamic"/>
            <br />
            <%-- Validate Patient no current invisits --%>
            <asp:CustomValidator ID="PatientBusyValidator" runat="server" ForeColor="#B6121B" Width="100"
                                 ErrorMessage="Patient has current InVisit." ControlToValidate="PatientId"
                                 OnServerValidate="InpatientValidate" Display="Dynamic"/>
            <br />
            <br />

            <%-- Radio button toggles display for inpatient fields --%>
            <asp:RadioButtonList ID="PatientTypeRadioButtonList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="InpatientToggle"
                                 RepeatDirection="horizontal" CssClass="radioButtonList">
                <asp:ListItem Selected="True">Outpatient</asp:ListItem>
                <asp:ListItem>Inpatient</asp:ListItem>
            </asp:RadioButtonList>
            <br />
            <br />
            
            <%-- InPatient Panel --%>
            <asp:panel ID="InpatientPanel" runat="server" Visible="false">

                <%-- Bed Textbox --%>
                <label for="Bed">Bed:</label>
                <asp:TextBox runat="server" ID="Bed" ClientIDMode="Static" />
                <%-- Validate Text Entered --%>
                <asp:RequiredFieldValidator ID="BedRequiredValidator" runat="server" ForeColor="#B6121B" Width="100"
                                            ErrorMessage="Bed ID Required" ControlToValidate="Bed" Display="Dynamic"/>
                <%-- Validate string is of form "111" (at least 1 number, max 3) --%>
                <asp:RegularExpressionValidator ID="BedRegexValidator" runat="server" ForeColor="#B6121B" Width="100"
                                                ErrorMessage="Input Incorrect. Numbers only, 3 Numbers Max." ControlToValidate="Bed"
                                                ValidationExpression="^[1-9]{1,3}$" Display="Dynamic" />
                <%-- Validate Bed Exists --%>
                <asp:CustomValidator ID="BedIdValidator" runat="server" ForeColor="#B6121B" Width="100"
                                     ErrorMessage="Provided Bed ID Does Not Exist" ControlToValidate="Bed"
                                     OnServerValidate="BedIdValidate" Display="Dynamic"/>
                <br />
                <br />

            </asp:panel>

            <%-- Submit --%>
            <asp:Button runat="server" ID="AssignSubmit" OnClick="AssignClick" Text="Assign Doctor" />
            <br />
            <br />

            <%-- Success Message --%>
            <asp:Literal runat="server" ID="AssignSuccess" Text="Assignment Successfull. </br>" Visible="false"/>

            <%-- Error Message --%>
            <asp:Literal runat="server" ID="AssignFail" Text="Assignment Failed. </br>" Visible="false"/>

        </asp:panel>
    </p>

    <p>
        <%-- Doctor Patient Info Panel --%>
        <asp:panel ID="DoctorInfoPanel" runat="server" Visible="false">

            <%-- Doctor ID Textbox --%>
            <label for="DoctorId1">Doctor ID:</label>
            <asp:TextBox runat="server" ID="DoctorId1" ClientIDMode="Static" />
            <br />
            <%-- Validate text entered --%>
            <asp:RequiredFieldValidator ID="DoctorIdRequiredValidator1" runat="server" ForeColor="#B6121B" Width="200"
                                        ErrorMessage="Doctor ID Required" ControlToValidate="DoctorId1" Display="dynamic"/>
            <%-- Validate string is of form "111" (at least 1 number, max 3) --%>
            <asp:RegularExpressionValidator ID="DoctorRegexValidator1" runat="server" ForeColor="#B6121B" Width="200"
                                            ErrorMessage="Input Incorrect. Numbers only, 3 Numbers Max." ControlToValidate="DoctorId1"
                                            ValidationExpression="^[1-9]{1,3}$" Display="Dynamic" />
            <%-- Validate provided doctor ID exists in database --%>
            <asp:CustomValidator ID="DoctorIdValidator1" runat="server" ForeColor="#B6121B" Width="200"
                                 ErrorMessage="Doctor ID does not exist." ControlToValidate="DoctorId1"
                                 OnServerValidate="DoctorIdValidate" Display="Dynamic"/>
            <br />

            <%-- Submit --%>
            <asp:Button runat="server" ID="InfoSubmitButton" OnClick="InfoClick" Text="Search" />
            <br />
            <br />

            <%-- Error Message --%>
            <asp:Literal runat="server" ID="InfoErrorMessage" Text="No Patients Found. </br>" Visible="false"/>
        </asp:panel>
    </p>

    <%-- Error Message --%>
    <asp:Literal runat="server" ID="DatabaseError" Text="Database Error. </br>" Visible="false"/>

    <div id="gridViewTable">
        <%-- Search results grid view --%>
        <asp:GridView runat="server" ID="DoctorPatientGridView" Visible="false" AutoGenerateColumns="false">
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
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>
