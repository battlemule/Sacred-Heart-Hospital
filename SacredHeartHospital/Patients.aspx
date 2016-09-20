<%--*****************************************************************
    * Patients.aspx                                      v1.2 09/2016
    * Sacred Heart Hospital                             Robert Willis
    *
    * Webform showing grid view of all patients in hospital database.
    *****************************************************************--%>
<%@ Page Title="Patients List" Language="C#" 
    MasterPageFile="~/SacredHeartHospital.Master" AutoEventWireup="true" 
    CodeBehind="Patients.aspx.cs" Inherits="SacredHeartHospital.Patients" %>

<%-- Content Placeholder --%>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <%-- Page Heading --%>
    <h2>Patients List</h2>

    <p>
        <%-- Search textbox label --%>
        <label for="Search">Search Name:</label>
        <br />
        <br />

        <%-- Search textbox --%>
        <asp:TextBox runat="server" ID="Search" ClientIDMode="Static" />
        <br />
        <br />

        <%-- Submit button --%>
        <asp:Button runat="server" ID="SubmitButton" OnClick="SearchClick" Text="Search" />
        <br />

        <%-- Vaidate input given --%>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="#B6121B" Width="200"
                                        ErrorMessage="Name Required" ControlToValidate="Search" />
        <br />

        <%-- No results found error message --%>
        <asp:Literal runat="server" ID="NotFoundError" Text="No Results Found. </br>" Visible="false"/>
    </p>

    <%-- Grid View --%>
    <div id="gridViewTable">
        <asp:GridView runat="server" ID="PatientGridView" AutoGenerateColumns="false">
            <Columns>
                <%-- Patient ID --%>
                <asp:BoundField ShowHeader="true" DataField="id" HeaderText="ID" />
                <%-- Patient Name --%>
                <asp:BoundField ShowHeader="true" DataField="name" HeaderText="Name" />
                <%-- Patient Address --%>
                <asp:BoundField ShowHeader="true" DataField="address" HeaderText="Address" />
                <%-- Patient Birthdate --%>
                <asp:BoundField ShowHeader="true" DataField="birthdate" HeaderText="Date of Birth" />
                <%-- Patient Phone --%>
                <asp:BoundField ShowHeader="true" DataField="phone" HeaderText="Phone" />
                <%-- Patient Emergency Contact --%>
                <asp:BoundField ShowHeader="true" DataField="emergency" HeaderText="Emergency Contact" />
                <%-- Patient Registration Date --%>
                <asp:BoundField ShowHeader="true" DataField="registration" HeaderText="Registration Date" />
            </Columns>
        </asp:GridView>
    </div>

    <p>
        <%-- Database error message --%>
        <asp:Literal runat="server" ID="DataBaseError" Text="Database Connection Error </br>" Visible="false"/>
    </p>

<%-- End Content --%>
</asp:Content>
