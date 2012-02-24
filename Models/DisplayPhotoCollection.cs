using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Collections;

namespace VinayakSuleyDotCom.Models
{
    public class DisplayPhotoCollection
    {
      public IEnumerable<DisplayPhoto> DisplayPhotos { get; private set; }

      public DisplayPhotoCollection(IEnumerable<DisplayPhoto> displayPhotos)
      {
        DisplayPhotos = displayPhotos;
      }

      public string ToJson()
      {
        JavaScriptSerializer jsonConverter = new JavaScriptSerializer();

        return jsonConverter.Serialize(DisplayPhotos);
      }
    }
}
