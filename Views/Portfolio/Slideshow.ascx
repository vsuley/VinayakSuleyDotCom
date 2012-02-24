<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<VinayakSuleyDotCom.Models.DisplayPhotoCollection>" %>
<%@ Import Namespace="VinayakSuleyDotCom.Models" %>
<%@ Import Namespace="System.Web.Script.Serialization" %>

<div id="slideshowContainer" class="slideshowContainer">

  <div id="leftScrollZone"    class="scrollZone topAlign1 alignWindowLeft"">&nbsp;</div>
  <div id="rightScrollZone"   class="scrollZone topAlign1 alignWindowRight"">&nbsp;</div>

      
  <!-- calculate total width of panel including offscreen photos-->
  <%
    int totalWidth = 0; 
    foreach (DisplayPhoto photo in Model.DisplayPhotos) {
      totalWidth += photo.DisplayWidth + 2; // 2 pixels for spacer.
    }
  %>
  <div id="panelSlider" class="panelSlider topAlign1" style="width:<%:totalWidth%>px;">

    <%
    int index = 0; 
    foreach (DisplayPhoto photo in Model.DisplayPhotos) { 
    %>
    
      <div 
        class="displayCell" 
        index="<%:index %>"
        style="position:relative; float:left; width:<%:photo.DisplayWidth%>px; height:500px; overflow:hidden;">

        <div class="displayCellBG">
          <img 
          src="<%:Url.Content(photo.PathToMedium)%>" 
          alt="Alternate Text" 
          class="sliderBG"
          />
        </div>
        
        <div class="displayCellImage">
          <img 
          src="<%:photo.PathToThumb%>" 
          alt="<%:photo.Title%>" 
          style="position:absolute; z-index:2; top:<%: photo.Y %>px; left:0px; width:<%:photo.DisplayWidth%>px; height:<%:photo.DisplayHeight%>px;"
          />
        </div>
      </div>
      
      <div class="spacer2px">&nbsp;</div>
      
      <% index ++;

    } %>
    
  </div>
</div>

<div id="fullImageClickTarget" class="fullScreenClickArea toggleHidden" />
<div id="fullImageBG" class="fullImageBG toggleHidden" ></div>
<img src="#" id="fullImage" class="fullImage toggleHidden" alt="" />

<div id="fullModeHelpText" class="fullModeHelpText">
  Use left/right cursor keys to navigate.
</div>
<%
  var pageData = Model.ToJson();
  
  string formattedResponse = string.Format(
    "<script type=\"text/javascript\"> var photoCollectionJSON = '{0}'; </script>", 
    pageData
  );
  
  Response.Write(formattedResponse);
%>