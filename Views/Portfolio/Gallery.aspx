<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<VinayakSuleyDotCom.Models.DisplayPhotoCollection>" %>

<%@ Register Src="Slideshow.ascx" TagName="Slideshow" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Vinayak Suley's photo portfolio
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
  <% Html.RenderPartial("Slideshow", Model); %>
</asp:Content>

<asp:Content ID="PortfolioViewerScript" ContentPlaceHolderID="PageScripts" runat="server">
  
  <script src="../../Scripts/PortfolioViewer.js" type="text/javascript"></script>
</asp:Content>
