using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VinayakSuleyDotCom.Models;
using VinayakSuleyDotCom.Properties;

namespace VinayakSuleyDotCom.Controllers
{
    public class PortfolioController : Controller
    {
        //
        // GET: /Portfolio/

        public ActionResult TaggedPhotos(string tag)
        {
            var photosFound = _photoRepository.FindWithTag(tag);

            if (photosFound.Count<Photo>() == 0)
                return View("NoPhotosForTag", (object)tag);

            return View("Gallery", ProcessPhotosForDisplay(photosFound));
        }

        public ActionResult RecentPhotos()
        {
            var photosFound = _photoRepository.FindRecent(Settings.Default.RecentCount);

            return View("Gallery", ProcessPhotosForDisplay(photosFound));
        }

        private DisplayPhotoCollection ProcessPhotosForDisplay(IQueryable<Photo> photosFound)
        {
            // 1. Go through photos, create a parallel list of viewmodel photos and jitter sizes.
            List<DisplayPhoto> photosForDisplay = new List<DisplayPhoto>();
            CreateDisplayPhotosAndJitter(photosFound, photosForDisplay);

            // 2. Now lay the photos out.
            //    Ideally this would be done in JS but for ease of coding doing it here now. 
            CalculatePhotoPositions(photosForDisplay, Settings.Default.GalleryMaxHeight);

            return new DisplayPhotoCollection(photosForDisplay.AsEnumerable<DisplayPhoto>());
        }

        /// <summary>
        /// This method takes the list of original photos and creates a parallel copy of it for sending to view. 
        /// It also comes up with a display size based on prefernce and some random jitter.
        /// </summary>
        private void CreateDisplayPhotosAndJitter(IQueryable<Photo> originalPhotos, List<DisplayPhoto> photosForDisplay)
        {
            Random jitter = new Random();
            
            foreach (Photo originalPhoto in originalPhotos)
            {
                DisplayPhoto displayPhoto = new DisplayPhoto();
                displayPhoto.TranslatePhotoToDisplayPhoto(originalPhoto);

                float maxEdge = (float)Settings.Default.SmallEdge / (float)(originalPhoto.Preference + jitter.NextDouble());
                displayPhoto.DisplayWidth = originalPhoto.Aspect > 1 ? (int)maxEdge : (int) (maxEdge * originalPhoto.Aspect);
                displayPhoto.DisplayHeight = originalPhoto.Aspect > 1 ? (int)(maxEdge / originalPhoto.Aspect) : (int)maxEdge;
                
                displayPhoto.PathToThumb = originalPhoto.PathToSmallThumb;

                photosForDisplay.Add(displayPhoto);
                // X and Y will be calculated by Layout method.
            }
        }

        private void CalculatePhotoPositions(List<DisplayPhoto> displayPhotos, int maxHeight)
        {
            // For now, arrange them in a simple line, aligned at the top.
            int xCounter = 0;
            int photoCount = 0;
            int smallestY = maxHeight;
            Random random = new Random();

            foreach (DisplayPhoto displayPhoto in displayPhotos)
            {
                displayPhoto.X = xCounter;
                
                // Stagger every other photo vertically
                // Instead of having the photos strewn along the horizontal very precisely,
                // wel'll introduce a small offset to make it look cool and introduce some overlap in the vertical
                // space.
                int offset = Math.Min(random.Next(10, 50), displayPhoto.DisplayHeight);
                displayPhoto.Y =
                    (photoCount % 2 == 0) ?
                    maxHeight / 2 - offset:
                    maxHeight / 2 - displayPhoto.DisplayHeight + offset;

                photoCount++;
                xCounter += displayPhoto.DisplayWidth + Settings.Default.GalleryInterPhotoGap;
                smallestY = (displayPhoto.Y < smallestY) ? displayPhoto.Y : smallestY;
            }

            // Now normalize the positions so that we don't have excess space up top.
            foreach (DisplayPhoto displayPhoto in displayPhotos)
            {
                displayPhoto.Y -= smallestY;
            }   
        }

        PhotoRepository _photoRepository = new PhotoRepository();
    }
}
