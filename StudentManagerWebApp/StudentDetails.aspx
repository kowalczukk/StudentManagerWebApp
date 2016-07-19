<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentDetails.aspx.cs" Inherits="StudentManagerWebApp.StudentDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .btn-large {}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div>
            <asp:Label ID="idLabel" runat ="server" Text="ID ucznia:" Width="80"/>
            <asp:Textbox ID="idBox" CssClass="form-control" runat ="server" Enabled = "false" Width="50"/>
        </div>
        <div>
            <asp:Label ID="lastNameLabel" runat ="server" Text="Nazwisko:" Width="80"/>
            <asp:Textbox ID="lastNameBox" runat ="server" Enabled = "false" Width="200"/>
        </div>
        <div>
            <asp:Label ID="firstNameLabel" runat ="server" Text="Imię ucznia:" Width="80"/>
            <asp:Textbox ID="firstNameBox" runat ="server" Enabled = "false" Width="200"/>
        </div>
        <div>
            &nbsp
        </div>

    </div>
        <asp:Label ID="Label1" runat="server" Text="Przemiot:"></asp:Label>
        <asp:DropDownList ID="DropDownList" AppendDataBoundItems="true" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_SelectedIndexChanged">
            <asp:ListItem Text="" Value="" />
        </asp:DropDownList>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DropDownList" ErrorMessage="Proszę wybrać przedmiot." Font-Bold="True" ForeColor="Red" ValidationGroup="valSD"></asp:RequiredFieldValidator>
         <div>
            &nbsp
        </div>
        <asp:GridView ID="GridView" runat="server" AutoGenerateColumns="False" OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit" 
            OnRowUpdating="GridView1_RowUpdating" OnRowDeleting="GridView1_RowDeleting" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hiddenMarkID" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "ID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="number" HeaderText ="Ocena" />
                <asp:BoundField DataField="whatFor" HeaderText ="Za co?" />
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
             <br />
&nbsp;<asp:Label ID="addMarkLabel" runat="server" Font-Bold="True" Text="DODAWANIE OCENY:"></asp:Label>
        </div>
        <div>
            <asp:Label ID="numberLabel" runat="server" Text="Ocena:" Width="52px" />
            <asp:TextBox ID="numberBox" runat="server" Width="50" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="numberBox" ErrorMessage="Proszę wpisać ocenę." Font-Bold="True" ForeColor="Red" ValidationGroup="valSD" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="numberBox" Display="Dynamic" ErrorMessage="Podana ocena jest niepoprawna." Font-Bold="True" ForeColor="Red" MaximumValue="6" MinimumValue="1" ValidationGroup="valSD" Type="Integer"></asp:RangeValidator>
        </div>
        <div>
            <asp:Label ID="typeLabel" runat="server" Text="Za co:" Width="52px"/>
            <asp:TextBox ID="typeBox" runat="server" Width="150" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="typeBox" ErrorMessage="Proszę wpisać za co jest ocena." Font-Bold="True" ForeColor="Red" ValidationGroup="valSD"></asp:RequiredFieldValidator>
        </div>
        <div>
            <asp:Button ID="addMarkButton" runat="server" CssClass="btn btn-primary btn-large" 
                Text="DODAJ OCENĘ" OnClick ="addMarkButton_Click" ValidationGroup="valSD" />
            <br />
        </div>
        <div>
            <asp:Button ID="returnButton" runat="server" CssClass="btn btn-primary btn-large" 
                Text="Powrót" OnClick ="returnButton_Click" Height="47px" Width="94px" />
        </div>
    </form>
</body>
</html>
