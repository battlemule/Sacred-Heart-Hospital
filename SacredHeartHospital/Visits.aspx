<%--*****************************************************************
    * Visits.aspx                                        v1.2 09/2016
    * Sacred Heart Hospital                             Robert Willis
    *
    * Webform showing grid view of all visits in hospital database.
    * Searchable using user input.
    *****************************************************************--%>
<%@ Page Title="Patient Visits" Language="C#" 
    MasterPageFile="~/SacredHeartHospital.Master" AutoEventWireup="true" 
    CodeBehind="Visits.aspx.cs" Inherits="SacredHeartHospital.Visits" %>

<%-- Content Placeholder --%>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <%-- Page Heading --%>
    <h2>Patient Visits</h2>
    
    <p>
        <%-- Search textbox --%>
        <label for="Search">Search</label>
        <br />
        <br />
        <asp:TextBox runat="server" ID="Search" ClientIDMode="Static" />

        <%-- Type of search radio list --%>
        <asp:RadioButtonList ID="radiolist1" CssClass="radioButtonList" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem Selected="True">Name</asp:ListItem>
            <asp:ListItem>Date of Visit</asp:ListItem>
            <asp:ListItem>Date of Discharge</asp:ListItem>
        </asp:RadioButtonList>
        <br />

        <%-- Submit button --%>
        <asp:Button runat="server" ID="SubmitButton" OnClick="SearchClick" Text="Search" />
        <br />

        <%-- Vaidate input given --%>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="#B6121B" Width="300"
                                        ErrorMessage="Search Query Required" ControlToValidate="Search" />
        <br />
        <%-- No Results Error Message --%>
        <asp:Literal runat="server" ID="NotFoundError" Text="No Results Found. </br>" Visible="false"/>
    </p> 

    <%-- Grid View --%>
    <div id="gridViewTable">
        <%-- Search results grid view --%>
        <asp:GridView runat="server" ID="VisitGridView" AutoGenerateColumns="false">
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
            </Columns>
        </asp:GridView>
    </div>

    <p>
        <%-- Database error message --%>
        <asp:Literal runat="server" ID="DataBaseError" Text="Database Connection Error </br>" Visible="false"/>
    </p>

<%-- End Content --%>
</asp:Content>