using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VinayakSuleyDotCom.Models
{
    public class PhotoRepository
    {
        VSDotComEntities _entities = new VSDotComEntities();

        #region Query Methods

        /// <summary>
        /// Returns a Queryable collection of all photos in database.
        /// </summary>
        public IQueryable<Photo> GetAllPhotos()
        {
            return _entities.Photos;
        }

        /// <summary>
        /// Takes a string as input and returns all the photos associated with that tag. The results are ordered 
        /// by Date Uploaded and sorted in Descending order.
        /// </summary>
        public IQueryable<Photo> FindWithTag(string tag)
        {
            var requestedPhotos = from photo in _entities.Photos
                                  where photo.Tags.Contains(tag)
                                  orderby photo.DateUploaded descending
                                  select photo;

            return requestedPhotos;
        }

        /// <summary>
        /// This returns a list of photos arranged in descending order of DateUploaded, capped to a max number 
        /// that is passed in as input.
        /// </summary>
        public IQueryable<Photo> FindRecent(int cap)
        {
            // Grab photos arranged by date uploaded.
            var allPhotos = from photo in _entities.Photos
                            orderby photo.DateUploaded descending
                            select photo;

            // Now take only the fifteen most recent ones.
            int i = 0;

            List<Photo> fifteenRecentPhotos = new List<Photo>();
            foreach (Photo photo in allPhotos)
            {
                fifteenRecentPhotos.Add(photo);
                if (++i == cap) break;
            }

            // return this List.
            return fifteenRecentPhotos.AsQueryable<Photo>();
        }

        /// <summary>
        /// Find the photo with the specified ID and returns it.
        /// </summary>
        public Photo GetPhoto(int photoId)
        {
            return _entities.Photos.FirstOrDefault(photo => photo.PhotoId == photoId);
        }

        #endregion 
        
        #region Insert/Delete methods

        public void Add(Photo photo)
        {
            _entities.Photos.AddObject(photo);
        }

        public void Delete(Photo photo)
        {
            foreach (var comment in photo.Comments)
            {
                _entities.Comments.DeleteObject(comment);
            }

            _entities.Photos.DeleteObject(photo);
        }

        #endregion

        #region Persistence

        public void Save()
        {
            _entities.SaveChanges();
        }

        #endregion 
    }
}