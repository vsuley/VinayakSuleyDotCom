<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    LogOn
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <% using (Html.BeginForm())
           { %>
        <div>
            <p>
                <label for="username">
                    Username:</label>
                <%: Html.TextBox("username") %>
                <%: Html.ValidationMessage("username", "*") %>
            </p>
            <p>
                <label for="password">
                    Password:</label>
                <%: Html.Password("password") %>
                <%: Html.ValidationMessage("password", "*")%>
            </p>
            <p>
                <%: Html.CheckBox("rememberMe") %>
                <label class="inline" for="rememberMe">
                    Remember me?</label>
            </p>
            <p>
                <input class="classiclogon" type="submit" value="Log On" />
            </p>
        </div>
    </div>
    <% } %>
</asp:Content>
