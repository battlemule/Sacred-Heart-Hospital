<%--*****************************************************************
    * Doctors.aspx                                       v1.2 09/2016
    * Sacred Heart Hospital                             Robert Willis
    *
    * Webform showing grid view of all doctors in hospital database.
    *****************************************************************--%>
<%@ Page Title="Doctors List" Language="C#" 
    MasterPageFile="~/SacredHeartHospital.Master" AutoEventWireup="true" 
    CodeBehind="Doctors.aspx.cs" Inherits="SacredHeartHospital.Doctors" %>

<%-- Content Placeholder --%>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
   
     <%-- Page Header --%>
    <h1>Doctors List</h1>
    
    <%-- Page Content --%>

    <%-- Grid View --%>
    <div id="gridViewTable">
        <asp:GridView runat="server" ID="DoctorGridView" AutoGenerateColumns="false">
            <Columns>
                <%-- Doctor ID --%>
                <asp:BoundField ShowHeader="true" DataField="id" HeaderText="ID" />
                <%-- Doctor Name --%>
                <asp:BoundField ShowHeader="true" DataField="name" HeaderText="Name" />
                <%-- Doctor Address --%>
                <asp:BoundField ShowHeader="true" DataField="address" HeaderText="Address" />
                <%-- Doctor Phone --%>
                <asp:BoundField ShowHeader="true" DataField="phone" HeaderText="Phone" />
            </Columns>
        </asp:GridView>
    </div>
    
    <p>
        <%-- Database error message --%>
        <asp:Literal runat="server" ID="DataBaseError" Text="Database Connection Error </br>" Visible="false"/>
    </p>

<%-- End Content --%>
</asp:Content>
