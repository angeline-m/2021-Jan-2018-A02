<%@ Page Title="ListView ODS CRUD" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListViewODSCRUD.aspx.cs" Inherits="WebApp.SamplePages.ListViewODSCRUD" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Single Record CRUD using ODS: ListView</h1>
    <div class="row">
        <div class="offset-2">
            <blockquote class="alert alert-info">
                This sample will use the asp:ListView control<br />
                This sample will use ODS for the ListView control<br />
                This sample will use minimal code behind<br />
                This sample will use the course MessageUserControl for error handling<br />
                This sample will use validation on the ListView control
            </blockquote>
        </div>
    </div>
    <div class="row">
        <div class="offset-2">
            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
            <asp:ValidationSummary ID="ValidationSummaryEdit" runat="server" HeaderText="Following are concerns with your data while editing" ValidationGroup="egroup" />
            <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" HeaderText="Following are concerns with your data while inserting" ValidationGroup="igroup" />
        </div>
    </div>
    <div class="row">
        <div class="offset-2">
            <%-- As a reminder to use the attribute DataKeyNames to get the DELETE function of your ListView CRUD to work
            
            The DataKeyNames attribute is set to your PK field
            --%>

            <asp:ListView ID="ListView1" runat="server" DataSourceID="AlbumListODS" InsertItemPosition="FirstItem"
                DataKeyNames="AlbumId">

                <AlternatingItemTemplate>
                    <tr style="background-color: #FFF8DC;">
                        <td>
                            <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" OnClientClick="return confirm('Are you sure you wish to remove this album?')" />
                            <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                        </td>
                        <td>
                            <asp:Label Text='<%# Eval("AlbumId") %>' runat="server" ID="AlbumIdLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("Title") %>' runat="server" ID="TitleLabel" /></td>
                        <td>
                            <%--<asp:Label Text='<%# Eval("ArtistId") %>' runat="server" ID="ArtistIdLabel" />--%>
                            <asp:DropDownList ID="ArtistList" runat="server" DataSourceID="ArtistListODS" DataTextField="DisplayField" DataValueField="ValueField" SelectedValue='<%# Eval("ArtistId") %>' Enabled="false" Width="300px"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label Text='<%# Eval("ReleaseYear") %>' runat="server" ID="ReleaseYearLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("ReleaseLabel") %>' runat="server" ID="ReleaseLabelLabel" /></td>
                    </tr>
                </AlternatingItemTemplate>
                <EditItemTemplate>
                    <%-- Validation controls will be plpaced inside the associated template. The ID of the validated control needs to be unique. *The validation controls for a particular template needs to be grouped* The validation executes only on the use of the template where the grouped is tied to the button--%>
                    <asp:RequiredFieldValidator ID="RequiredTitleE" runat="server" ErrorMessage="Album title is required when editing" ControlToValidate="TitleTextBoxE" Display="None" ValidationGroup="egroup"></asp:RequiredFieldValidator>
                    <tr style="background-color: #008A8C; color: #FFFFFF;">
                        <td>
                            <%-- Tie the button to the appropriate validation group. Failure to tie the button to the appropriate group will cause all validation on the page to execute --%>
                            <asp:Button runat="server" CommandName="Update" Text="Update" ID="UpdateButton" ValidationGroup="egroup"/>
                            <asp:Button runat="server" CommandName="Cancel" Text="Cancel" ID="CancelButton" />
                        </td>
                        <td>
                            <asp:TextBox Text='<%# Bind("AlbumId") %>' runat="server" ID="AlbumIdTextBox" /></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("Title") %>' runat="server" ID="TitleTextBoxE" /></td>
                        <td>
                            <%--<asp:Label Text='<%# Eval("ArtistId") %>' runat="server" ID="ArtistIdLabel" />--%>
                            <asp:DropDownList ID="ArtistList" runat="server" DataSourceID="ArtistListODS" DataTextField="DisplayField" DataValueField="ValueField" SelectedValue='<%# Bind("ArtistId") %>' Enabled="true" Width="300px"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox Text='<%# Bind("ReleaseYear") %>' runat="server" ID="ReleaseYearTextBox" /></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("ReleaseLabel") %>' runat="server" ID="ReleaseLabelTextBox" /></td>
                    </tr>
                </EditItemTemplate>
                <EmptyDataTemplate>
                    <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                        <tr>
                            <td>No data was returned.</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <InsertItemTemplate>
                    <asp:RequiredFieldValidator ID="RequiredTitleI" runat="server" ErrorMessage="Album title is required when inserting" ControlToValidate="TitleTextBoxI" Display="None" ValidationGroup="igroup"></asp:RequiredFieldValidator>
                    <tr style="">
                        <td>
                            <asp:Button runat="server" CommandName="Insert" Text="Insert" ID="InsertButton" ValidationGroup="igroup"/>
                            <asp:Button runat="server" CommandName="Cancel" Text="Clear" ID="CancelButton" />
                        </td>
                        <td>
                            <asp:TextBox Text='<%# Bind("AlbumId") %>' runat="server" ID="AlbumIdTextBox" /></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("Title") %>' runat="server" ID="TitleTextBoxI" /></td>
                        <td>
                            <%--<asp:Label Text='<%# Eval("ArtistId") %>' runat="server" ID="ArtistIdLabel" />--%>
                            <asp:DropDownList ID="ArtistList" runat="server" DataSourceID="ArtistListODS" DataTextField="DisplayField" DataValueField="ValueField" SelectedValue='<%# Bind("ArtistId") %>' Enabled="true" Width="300px"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox Text='<%# Bind("ReleaseYear") %>' runat="server" ID="ReleaseYearTextBox" /></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("ReleaseLabel") %>' runat="server" ID="ReleaseLabelTextBox" /></td>
                    </tr>
                </InsertItemTemplate>
                <ItemTemplate>
                    <tr style="background-color: #DCDCDC; color: #000000;">
                        <td>
                            <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" OnClientClick="return confirm('Are you sure you wish to remove this album?')" />
                            <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                        </td>
                        <td>
                            <asp:Label Text='<%# Eval("AlbumId") %>' runat="server" ID="AlbumIdLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("Title") %>' runat="server" ID="TitleLabel" /></td>
                        <td>
                            <%--<asp:Label Text='<%# Eval("ArtistId") %>' runat="server" ID="ArtistIdLabel" />--%>
                            <asp:DropDownList ID="ArtistList" runat="server" DataSourceID="ArtistListODS" DataTextField="DisplayField" DataValueField="ValueField" SelectedValue='<%# Eval("ArtistId") %>' Enabled="false" Width="300px"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label Text='<%# Eval("ReleaseYear") %>' runat="server" ID="ReleaseYearLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("ReleaseLabel") %>' runat="server" ID="ReleaseLabelLabel" /></td>
                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table runat="server">
                        <tr runat="server">
                            <td runat="server">
                                <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                                    <tr runat="server" style="background-color: #DCDCDC; color: #000000;">
                                        <th runat="server"></th>
                                        <th runat="server">Id</th>
                                        <th runat="server">Title</th>
                                        <th runat="server">Artist</th>
                                        <th runat="server">Year</th>
                                        <th runat="server">Label</th>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder"></tr>
                                </table>
                            </td>
                        </tr>
                        <tr runat="server">
                            <td runat="server" style="text-align: center; background-color: #CCCCCC; font-family: Verdana, Arial, Helvetica, sans-serif; color: #000000;">
                                <asp:DataPager runat="server" ID="DataPager1">
                                    <Fields>
                                        <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                        <asp:NumericPagerField></asp:NumericPagerField>
                                        <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                    </Fields>
                                </asp:DataPager>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
                <SelectedItemTemplate>
                    <tr style="background-color: #008A8C; font-weight: bold; color: #FFFFFF;">
                        <td>
                            <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" OnClientClick="return confirm('Are you sure you wish to remove this album?')" />
                            <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                        </td>
                        <td>
                            <asp:Label Text='<%# Eval("AlbumId") %>' runat="server" ID="AlbumIdLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("Title") %>' runat="server" ID="TitleLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("ArtistId") %>' runat="server" ID="ArtistIdLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("ReleaseYear") %>' runat="server" ID="ReleaseYearLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("ReleaseLabel") %>' runat="server" ID="ReleaseLabelLabel" /></td>
                    </tr>
                </SelectedItemTemplate>
            </asp:ListView>
            <asp:ObjectDataSource ID="AlbumListODS" runat="server" DataObjectTypeName="ChinookSystem.ViewModels.AlbumItem" DeleteMethod="Album_Delete" InsertMethod="Album_Add" OldValuesParameterFormatString="original_{0}" SelectMethod="Albums_List" TypeName="ChinookSystem.BLL.AlbumController" UpdateMethod="Album_Update" OnDeleted="DeleteCheckForException" OnInserted="InsertCheckForException" OnSelected="SelectCheckForException" OnUpdated="UpdateCheckForException"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ArtistListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Artists_DDLList" TypeName="ChinookSystem.BLL.ArtistController" OnSelected="SelectCheckForException"></asp:ObjectDataSource>
        </div>
    </div>
</asp:Content>
