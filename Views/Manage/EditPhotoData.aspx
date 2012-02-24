<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<VinayakSuleyDotCom.Models.Photo>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit photo data
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit photo data</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <div style="width:1000px;">
          <div style="Padding-bottom:15px;">
            <table style="width:100%;">
              <tr style="padding-left:0px;">
                <td rowspan="10" style="width:335px;"><img src="<%:Url.Content(Model.PathToSmallThumb)%>" alt="<%:Model.Title %>"/></td>
                <td style="width: 180px"><%: Html.LabelFor(model => model.PhotoId) %></td>
                <td>
                  <%: Html.TextBoxFor(model => model.PhotoId, new { style="Width:100%;"})%>
                  <%: Html.ValidationMessageFor(model => model.PhotoId) %>
                </td>
              </tr>
              <tr style="padding-left:0px;">
                <td style="width: 180px"><%: Html.LabelFor(model => model.Title) %></td>
                <td>
                  <%: Html.TextBoxFor(model => model.Title, new { style = "Width:100%;" })%>
                  <%: Html.ValidationMessageFor(model => model.Title) %>
                </td>
              </tr>
              <tr style="padding-left:0px;">
                <td style="width: 180px"><%: Html.LabelFor(model => model.Tags) %></td>
                <td>
                  <%: Html.TextBoxFor(model => model.Tags, new { style = "Width:100%;" })%>
                  <%: Html.ValidationMessageFor(model => model.Tags) %>
                </td>
              </tr>
              <tr style="padding-left:0px;">
                <td style="width: 180px"><%: Html.LabelFor(model => model.Description) %></td>
                <td>
                  <%: Html.TextBoxFor(model => model.Description, new { style = "Width:100%;" })%>
                  <%: Html.ValidationMessageFor(model => model.Description) %>
                </td>
              </tr>
              <tr style="padding-left:0px;">
                <td style="width: 180px"><%: Html.LabelFor(model => model.DateTaken) %></td>
                <td>
                  <%: Html.TextBoxFor(model => model.DateTaken, new { style="Width:100%;"}) %>
                  <%: Html.ValidationMessageFor(model => model.DateTaken) %>
                </td>
              </tr>
              <tr style="padding-left:0px;">
                <td style="width: 180px"><%: Html.LabelFor(model => model.DateUploaded) %></td>
                <td>
                  <%: Html.TextBoxFor(model => model.DateUploaded, new { style = "Width:100%;" })%>
                  <%: Html.ValidationMessageFor(model => model.DateUploaded) %>
                </td>
              </tr>
              <tr style="padding-left:0px;">
                <td style="width: 180px"><%: Html.LabelFor(model => model.Worksafe) %></td>
                <td>
                  <%: Html.TextBoxFor(model => model.Worksafe, new { style = "Width:100%;" })%>
                  <%: Html.ValidationMessageFor(model => model.Worksafe) %>
                </td>
              </tr>
              <tr style="padding-left:0px;">
                <td style="width: 180px"><%: Html.LabelFor(model => model.Preference) %></td>
                <td>
                  <%: Html.TextBoxFor(model => model.Preference, new { style = "Width:100%;" })%>
                  <%: Html.ValidationMessageFor(model => model.Preference) %>
                </td>
              </tr>
              <tr style="padding-left:0px;">
                <td style="width: 180px">Dimensions</td>
                <td>
                  <%: Model.Width%> px wide, <%: Model.Height%> px tall (aspect ratio: <%: Model.Aspect%>)
                </td>
              </tr>
            </table>
          </div>

          <div style="Padding-bottom:15px;">
            <table style="width:100%">
              <tr>
                <td style="width:180px"><%: Html.LabelFor(model => model.PathToSmallThumb) %></td>
                <td>
                  <%: Html.TextBoxFor(model => model.PathToSmallThumb, new { style = "Width:100%;" })%>
                  <%: Html.ValidationMessageFor(model => model.PathToSmallThumb) %>
                </td>
              </tr>
              <tr>
                <td style="width: 180px"><%: Html.LabelFor(model => model.PathToMediumThumb) %></td>
                <td>
                  <%: Html.TextBoxFor(model => model.PathToMediumThumb, new { style = "Width:100%;" })%>
                  <%: Html.ValidationMessageFor(model => model.PathToMediumThumb) %>
                </td>
              </tr>
              <tr>
                <td style="width: 180px"><%: Html.LabelFor(model => model.PathToOriginalFile) %></td>
                <td>
                  <%: Html.TextBoxFor(model => model.PathToOriginalFile, new { style = "Width:100%;" })%>
                  <%: Html.ValidationMessageFor(model => model.PathToOriginalFile) %>
                </td>
              </tr>
            </table>
          </div>
        </div>
                
        <p>
          <input type="submit" value="Save" />
        </p>

    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

