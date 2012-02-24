<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ContactInfo
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <div class="textBG">
    <div class="textContent">
      <p class="textContent">
        I live in Seattle and work in a studio located in the <a href="http://inscapearts.org" class="suggestedLinks" target="_blank">"Inscape"</a> building close to international district. 
        The best way to reach me is via email at vinayak [at] vinayaksuley [dot] com.
      </p>
      <p class="textContent">
        If you like this website, you'll probably also really like my facebook page <a href="http://www.facebook.com/vinphoto" class="suggestedLinks">here</a>, check it out!
      </p>
    </div>
  </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageScripts" runat="server">
</asp:Content>
