using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Policy;
using System.Web.Mvc;
using System.Web.Routing;

namespace VinayakSuleyDotCom.Models
{
    public class DisplayPhoto
    {
        private UrlHelper _urlHelper;

        private string _pathToOriginal;
        private string _pathToMedium;
        private string _pathToThumb;

        // Properties from original photo.
        public int Id { get; set; }
        public float Aspect { get; set; }
        public string Tags { get; set; }
        public string Title { get; set; }
        public DateTime DateUploaded;

        // View specific properties.
        public int DisplayWidth { get; set; }
        public int DisplayHeight { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public string PathToOriginal 
        { 
            get { return _pathToOriginal; }

            set { _pathToOriginal = _urlHelper.Content(value); }
        }
        
        public string PathToMedium 
        {
            get { return _pathToMedium; }

            set { _pathToMedium = _urlHelper.Content(value); }
        }
        
        public string PathToThumb 
        {
            get { return _pathToThumb; }

            set { _pathToThumb = _urlHelper.Content(value); }
        }

        /// <summary>
        /// Public constructor. Basically just initializes the urlHelper object
        /// </summary>
        public DisplayPhoto()
        {
            HttpContextBase currentContext = new HttpContextWrapper(HttpContext.Current);
            RouteData routeData = RouteTable.Routes.GetRouteData(currentContext);
            _urlHelper = new UrlHelper(new RequestContext(currentContext, routeData));
        }

        public void TranslatePhotoToDisplayPhoto(Photo photo)
        {
            this.Id = photo.PhotoId;
            this.Aspect = (float)photo.Aspect;
            this.Tags = photo.Tags;
            this.PathToOriginal = photo.PathToOriginalFile;
            this.PathToMedium = photo.PathToMediumThumb;
            this.Title = photo.Title;
            this.DateUploaded = photo.DateUploaded;

            // Best effort initialization. These should be changed by code.
            this.DisplayHeight = (int)photo.Height;
            this.DisplayWidth = (int)photo.Width;
            this.X = 0;
            this.Y = 0;
            this.PathToThumb = photo.PathToSmallThumb;
        }
    }
}