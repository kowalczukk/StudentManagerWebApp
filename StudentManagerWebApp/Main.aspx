<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="StudentManagerWebApp.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Students Manager</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <div>
            <asp:Label ID="lastNameLabel" runat ="server" Text="Nazwisko:" Width="80"/>
            <asp:Textbox ID="lastNameBox" runat ="server" Width="200"/>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="firstNameBox" ErrorMessage="Proszę podać nazwisko ucznia." Font-Bold="True" ForeColor="Red" ValidationGroup="valMain"></asp:RequiredFieldValidator>
        </div>
        <div>
            <asp:Label ID="firstNameLabel" runat ="server" Text="Imię ucznia:" Width="80"/>
            <asp:Textbox ID="firstNameBox" runat ="server" Width="200" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="lastNameBox" ErrorMessage="Proszę podać imię ucznia. " Font-Bold="True" ForeColor="Red" ValidationGroup="valMain"></asp:RequiredFieldValidator>
        </div>
        <div>
            <asp:Button ID="addButton" runat="server" Text="DODAJ NOWEGO STUDENTA" OnClick="addButton_Click" ValidationGroup="valMain" />
        </div>
        <div>
            &nbsp
        </div>
        <asp:GridView ID="dataGridView" runat ="server" AutoGenerateColumns="False" OnRowEditing="dataGridView_RowEditing" OnRowCancelingEdit="dataGridView_RowCancelingEdit"
            OnRowUpdating="dataGridView_RowUpdating" OnRowDeleting="dataGridView_RowDeleting" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="dataGridView_RowCommand" >
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hiddenStudentID" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "ID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Lp.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                <asp:BoundField DataField="LastName" HeaderText ="Nazwisko"/>
                <asp:BoundField DataField="FirstName" HeaderText ="Imię" />
                <asp:ButtonField CommandName="goToSD" Text="Szczegóły" />
                <asp:CommandField ShowEditButton ="true" />
                <asp:CommandField ShowDeleteButton ="true" />
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>               
        <div>
        </div>
    </div>
        <asp:Button ID="logoutButton" runat="server" Height="39px" OnClick="logoutButton_Click" Text="WYLOGUJ" Width="100px" />
    </form>
</body>
</html>
