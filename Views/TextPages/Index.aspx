<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Welcome to Vinayak Suley's website.
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <div class="textBG">
    <div class="textContent">
      <p class="textContent">
        Hello, and welcome!
      </p>
      <p class="textContent">
        I am a Seattle based fine-art photographer and software engineer. This website, however, is meant specifically to showcase my photographer self. I hope you enjoy 
        looking at the pictures and that they inspire or move you in some way. If not, then I obviously have some work left to do. 
      </p>
      <p class="textContent">
        I love hearing from people, so feel free to drop me a note with your comments and critique. I'm also always on the lookout for great people to collaborate and work with 
        so if you're a model, photographer, make-up artist, hair stylist or a designer looking to work together, don't think twice; fire up your email and send a message to 
        vinayak [at] vinayaksuley.com!
      </p>
      <p class="textContent">
        <strong>Note:</strong> I am in the process of overhauling my website from it's previous design. Turns out its not a trivial task :). I am constantly making improvements to it but 
        I apologize for any buggy or slow behavior it may show in the meantime. Performance, layout and interaction upgrades are rolling in soon! 
      </p>
    </div>
  </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageScripts" runat="server">
</asp:Content>
