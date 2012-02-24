<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<VinayakSuleyDotCom.Models.Photo>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Delete photo
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Delete photo</h2>

    <h3>Are you sure you want to delete this?</h3>
    <fieldset>
        <legend>Fields</legend>
        
        <div class="display-label">PhotoId</div>
        <div class="display-field"><%: Model.PhotoId %></div>
        
        <div class="display-label">Title</div>
        <div class="display-field"><%: Model.Title %></div>
        
        <div class="display-label">Description</div>
        <div class="display-field"><%: Model.Description %></div>
        
        <div class="display-label">Tags</div>
        <div class="display-field"><%: Model.Tags %></div>
        
        <div class="display-label">DateUploaded</div>
        <div class="display-field"><%: String.Format("{0:g}", Model.DateUploaded) %></div>
        
        <div class="display-label">DateTaken</div>
        <div class="display-field"><%: String.Format("{0:g}", Model.DateTaken) %></div>
        
        <div class="display-label">Preference</div>
        <div class="display-field"><%: Model.Preference %></div>
        
        <div class="display-label">PathToSmallThumb</div>
        <div class="display-field"><%: Model.PathToSmallThumb %></div>
        
        <div class="display-label">PathToMediumThumb</div>
        <div class="display-field"><%: Model.PathToMediumThumb %></div>
        
        <div class="display-label">PathToOriginalFile</div>
        <div class="display-field"><%: Model.PathToOriginalFile %></div>
        
        <div class="display-label">TextField1</div>
        <div class="display-field"><%: Model.TextField1 %></div>
        
        <div class="display-label">TextField2</div>
        <div class="display-field"><%: Model.TextField2 %></div>
        
        <div class="display-label">TextField3</div>
        <div class="display-field"><%: Model.TextField3 %></div>
        
        <div class="display-label">TextField4</div>
        <div class="display-field"><%: Model.TextField4 %></div>
        
        <div class="display-label">Worksafe</div>
        <div class="display-field"><%: Model.Worksafe %></div>
        
    </fieldset>
    <% using (Html.BeginForm()) { %>
        <p>
		    <input type="submit" value="Delete" /> |
		    <%: Html.ActionLink("Back to List", "Index") %>
        </p>
    <% } %>

</asp:Content>

