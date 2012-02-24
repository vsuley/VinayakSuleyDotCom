using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VinayakSuleyDotCom.Models;
using System.Drawing;
using System.IO;
using VinayakSuleyDotCom.Properties;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace VinayakSuleyDotCom.Controllers
{
  public class ManageController : Controller
  {
    PhotoRepository _photoRepository = new PhotoRepository();

    [Authorize]
    public ActionResult Index()
    {
      return View("PhotoCatalog", _photoRepository.GetAllPhotos());
    }

    [Authorize]
    public ActionResult UploadPhoto()
    {
      return View("UploadPhoto");
    }

    [Authorize]
    [HttpPost]
    public ActionResult UploadPhoto(HttpPostedFileBase file)
    {
      // If no content was recieved then just return the same view again.
      if (file.ContentLength == 0)
      {
        return View("UploadPhoto");
      }

      PathSet pathSet = new PathSet();
      PopulatePaths(pathSet, file.FileName);

      file.SaveAs(pathSet.originalFileLocal);

      // Create thumbnails.
      CreateThumbnailForImage(pathSet.mediumThumbLocal, pathSet.originalFileLocal, Settings.Default.MediumEdge, CompositingQuality.HighSpeed);
      CreateThumbnailForImage(pathSet.smallThumbLocal, pathSet.originalFileLocal, Settings.Default.SmallEdge, CompositingQuality.HighQuality);

      // Create a photo object, prepopulate it with some default values and save to DB. This will prevent us from creating orphan 
      // image files in case something goes wrong with the edit photo data page later.
      Photo photo = new Photo();

      // prepopulate photo with public paths, which are what we need to use later.
      photo.PathToOriginalFile = pathSet.originalFilePublic;
      photo.PathToMediumThumb = pathSet.mediumThumbPublic;
      photo.PathToSmallThumb = pathSet.smallThumbPublic;

      // Find out size and store the details.
      Image original = Image.FromFile(pathSet.originalFileLocal);
      photo.Width = original.Width;
      photo.Height = original.Height;
      photo.Aspect = (float)photo.Width / (float)photo.Height;

      // Dates and preference.
      TimeSpan typicalProcessingTime = TimeSpan.FromDays(Properties.Settings.Default.TypicalProcessingTimeInDays);
      photo.DateTaken = DateTime.Today.Subtract(typicalProcessingTime);
      photo.DateUploaded = DateTime.Today;
      photo.Preference = Settings.Default.DefaultPreference;

      // TODO: run a validation.
      ValidateModel(photo);

      // save
      _photoRepository.Add(photo);
      _photoRepository.Save();

      // Now to user-supplied metadata.
      return RedirectToAction("EditPhotoData", new { photoId = photo.PhotoId });
    }

    [Authorize]
    public ActionResult EditPhotoData(int photoId)
    {
      return View("EditPhotoData", _photoRepository.GetPhoto(photoId));
    }

    [Authorize]
    [HttpPost]
    public ActionResult EditPhotoData(int photoId, FormCollection formValues)
    {
      Photo photo = _photoRepository.GetPhoto(photoId);

      if (TryUpdateModel(photo))
      {
        _photoRepository.Save();

        return RedirectToAction("Index");
      }

      return View(photo);
    }

    [Authorize]
    public ActionResult DeletePhoto(int photoId)
    {
      return View(_photoRepository.GetPhoto(photoId));
    }

    [Authorize]
    [HttpPost]
    public ActionResult DeletePhoto(int photoId, FormCollection formValues)
    {
      // retrieve the photo object to be deleted.
      Photo photoToDelete = _photoRepository.GetPhoto(photoId);

      // capture files name and file paths.
      string filename = Path.GetFileName(photoToDelete.PathToOriginalFile);
      PathSet pathset = new PathSet();
      PopulatePaths(pathset, filename);

      // Delete from database.
      _photoRepository.Delete(photoToDelete);
      _photoRepository.Save();

      // Todo Delete from file system.
      System.IO.File.Delete(pathset.originalFileLocal);
      System.IO.File.Delete(pathset.mediumThumbLocal);
      System.IO.File.Delete(pathset.smallThumbLocal);

      return RedirectToAction("Index");
    }

    /// <summary>
    /// Populates the passed in object with the correct set of paths.
    /// </summary>
    private void PopulatePaths(PathSet pathset, string fileName)
    {
      pathset.originalFilePublic = Path.Combine("~/" + Settings.Default.PhotoCatalogFolder, Settings.Default.OriginalFileFolder, fileName);
      pathset.originalFileLocal = Server.MapPath(pathset.originalFilePublic);

      pathset.mediumThumbPublic = Path.Combine("~/" + Settings.Default.PhotoCatalogFolder, Settings.Default.MediumThumbsFolder, fileName);
      pathset.mediumThumbLocal = Server.MapPath(pathset.mediumThumbPublic);

      pathset.smallThumbPublic = Path.Combine("~/" + Settings.Default.PhotoCatalogFolder, Settings.Default.SmallThumbsFolder, fileName);
      pathset.smallThumbLocal = Server.MapPath(pathset.smallThumbPublic);
    }

    /// <summary>
    /// Private method that takes an image file name and rescales it so that it's largest edge is equal 
    /// to the one supplied.
    /// </summary>
    private void CreateThumbnailForImage(string thumbnailFilePath, string originalFilePath, int maxEdge, CompositingQuality compositingQuality)
    {
      // Load the existing image.
      Image original = Image.FromFile(originalFilePath);

      // The math for calculating the correct sizes.
      float aspect = (float)original.Width / (float)original.Height;
      int width = (original.Width > original.Height) ? maxEdge : (int)(maxEdge * aspect);
      int height = (original.Height > original.Width) ? maxEdge : (int)(maxEdge / aspect);

      // Code to rescale the image.        
      Bitmap thumbnail = new Bitmap(width, height);
      using (Graphics graphics = Graphics.FromImage(thumbnail))
      {
        graphics.CompositingQuality = compositingQuality;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.CompositingMode = CompositingMode.SourceCopy;
        graphics.DrawImage(original, 0, 0, width, height);
        thumbnail.Save(thumbnailFilePath, ImageFormat.Jpeg);
      }
    }

    internal class PathSet
    {
      public string smallThumbPublic { get; set; }
      public string smallThumbLocal { get; set; }
      public string mediumThumbPublic { get; set; }
      public string mediumThumbLocal { get; set; }
      public string originalFilePublic { get; set; }
      public string originalFileLocal { get; set; }
    }
  }

  public enum OperationChoice
  {
    Upload,
    Edit,
    UpdateSettings
  };

  public enum ImageSize
  {
    Small,
    Medium,
    Original
  };
}
