<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<VinayakSuleyDotCom.Models.Photo>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Photo catalog
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

  <div style="font-weight:bold; padding-left:25px; padding-bottom:50px;">
    <%: Html.ActionLink("Upload photo", "UploadPhoto") %>
  </div>

  <div style="Padding-left:20px">
    <% foreach (var item in Model) { %>
      <div style="float:left; width:700px; Padding-bottom:40px;">
        
        <div style="Padding-bottom:15px; padding-left:3px;">
          <span style="font-weight:bold; padding-right:10px;"><%: Html.ActionLink("Edit Metadata", "EditPhotoData", new { photoId=item.PhotoId }) %></span>
          <span style="font-weight:bold; padding-right:10px;"><%: Html.ActionLink("Delete", "DeletePhoto", new { photoId=item.PhotoId })%></span>
        </div>
        
        <div style="Padding-bottom:15px;">
          <table>
            <tr style="padding-left:0px;">
              <td rowspan="10" style="width:335px;"><img src="<%:Url.Content(item.PathToSmallThumb)%>" alt="<%:item.Title %>"/></td>
              <td class="style1">Photo Id</td>
              <td><%: item.PhotoId %></td>
            </tr>
            <tr style="padding-left:0px;">
              <td class="style1">Title</td>
              <td><%: item.Title %></td>
            </tr>
            <tr style="padding-left:0px;">
              <td class="style1">Tags</td>
              <td><%: item.Tags %></td>
            </tr>
            <tr style="padding-left:0px;">
              <td class="style1">Description</td>
              <td><%: item.Description %></td>
            </tr>
            <tr style="padding-left:0px;">
              <td class="style1">Date Taken</td>
              <td><%: item.DateTaken.ToString("MMMM dd, yyyy") %></td>
            </tr>
            <tr style="padding-left:0px;">
              <td class="style1">Date uploaded</td>
              <td><%: item.DateUploaded.ToString("MMMM dd, yyyy") %></td>
            </tr>
            <tr style="padding-left:0px;">
              <td class="style1">Worksafe</td>
              <td><%: item.Worksafe %></td>
            </tr>
            <tr style="padding-left:0px;">
              <td class="style1">Preference</td>
              <td><%: item.Preference %></td>
            </tr>
            <tr style="padding-left:0px;">
              <td class="style1">Dimensions</td>
              <td><%: item.Width %> px wide, <%: item.Height%> px tall</td>
            </tr>
          </table>
        </div>

        <div style="Padding-bottom:15px;">
          <table>
            <tr>
              <td style="width: 180px">Path to small thumb:</td>
              <td><%: item.PathToSmallThumb %></td>
            </tr>
            <tr>
              <td style="width: 180px">Path to medium thumb:</td>
              <td><%: item.PathToMediumThumb%></td>
            </tr>
            <tr>
              <td style="width: 180px">Path to original file: </td>
              <td><%: item.PathToOriginalFile%></td>
            </tr>
          </table>
        </div>
      </div>
    <% } %>
  </div>

</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="PageScripts">
    <style type="text/css">
        .style1
        {
            width: 110px;
        }
    </style>
</asp:Content>


