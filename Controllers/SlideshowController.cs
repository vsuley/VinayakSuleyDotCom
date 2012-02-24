using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VinayakSuleyDotCom.Models;

namespace VinayakSuleyDotCom.Controllers
{
    public class SlideshowController : Controller
    {
        public JsonResult GetPhoto()
        {
            DisplayPhoto panelPhoto = new DisplayPhoto();
            Photo photo = _photoRepository.FindRecent(30).First();

            panelPhoto.DisplayWidth = 100;
            panelPhoto.DisplayHeight = 100;
            panelPhoto.X = 0;
            panelPhoto.Y = 0;
            panelPhoto.Id = photo.PhotoId;

            return Json(panelPhoto, JsonRequestBehavior.AllowGet);
        }

        public void LayoutPhotoPanel(List<DisplayPhoto> photos)
        {
            // Assumptions:
            // 1. Coordinate system is such that x increases from left to right and y increases from top to bottom.
            // 2. When specifying an image's position, the top-left corner is specified.

            int startingX = 0;
            int startingY = 0;

            DisplayPhoto mainImage = photos[0];
            mainImage.X = startingX;
            mainImage.Y = startingY;

            bool leftEdgeAvailable = true;
            bool rightEdgeAvailable = true;
            bool bottomEdgeAvailable = true;
            bool topEdgeAvailable = true;
            int edgeConsumed = 0;

            for (int i = 1; i < photos.Count; i++)
            {

                if (rightEdgeAvailable)
                {

                    // Assign X and Y values. The way the algorigthm is structured, this is a fairly safe operation
                    // and can be dove into head-first. Then we setup the correct values for the next iteration.
                    photos[i].X = mainImage.X + mainImage.DisplayWidth;
                    photos[i].Y = mainImage.Y + edgeConsumed;
                    edgeConsumed += photos[i].DisplayHeight;

                    // Set it up correctly for the next iteration.
                    if (edgeConsumed > mainImage.DisplayHeight)
                    {
                        rightEdgeAvailable = false;
                        edgeConsumed = 0;
                    }
                    continue;
                }

                if (bottomEdgeAvailable)
                {

                    photos[i].X = mainImage.X + mainImage.DisplayWidth - photos[i].DisplayWidth - edgeConsumed;
                    photos[i].Y = mainImage.Y + mainImage.DisplayHeight;
                    edgeConsumed += photos[i].DisplayWidth;

                    if (edgeConsumed > mainImage.DisplayWidth)
                    {
                        bottomEdgeAvailable = false;
                        edgeConsumed = 0;
                    }
                    continue;
                }

                if (leftEdgeAvailable)
                {

                    photos[i].X = mainImage.X - photos[i].DisplayWidth;
                    photos[i].Y = mainImage.Y + mainImage.DisplayHeight - photos[i].DisplayHeight - edgeConsumed;
                    edgeConsumed += photos[i].DisplayHeight;

                    if (edgeConsumed > mainImage.DisplayHeight)
                    {
                        leftEdgeAvailable = false;
                        edgeConsumed = 0;
                    }
                    continue;
                }

                if (topEdgeAvailable)
                {

                    photos[i].X = mainImage.X + edgeConsumed;
                    photos[i].Y = mainImage.Y - photos[i].DisplayHeight;
                    edgeConsumed += photos[i].DisplayWidth;

                    if (edgeConsumed > mainImage.DisplayWidth)
                    {
                        topEdgeAvailable = false;
                        edgeConsumed = 0;
                    }
                    break;
                    // If i is not the last photo, then that means there are photos going to be left over.
                    // TODO: find a way to communicate left-over photos back to caller.
                }
            } // end of for loop that walks through photos.

        }
        
        PhotoRepository _photoRepository = new PhotoRepository();
    }
}
