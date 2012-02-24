<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<string>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Tag not found
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
  <div class="textBG">
    <div class="textContent">
      <p class="textContent">
        Sorry, there don't seem to be any photos in my portfolio with the tag "<%:Model.ToString() %>". Try 
        <a class="suggestedLinks" href="/portfolio/fine-art">fine-art</a> or <a class="suggestedLinks" href="/portfolio/fashion">fashion</a>
        instead.
      </p>
    </div>
  </div>
</asp:Content>
