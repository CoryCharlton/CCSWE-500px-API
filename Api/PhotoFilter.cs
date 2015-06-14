using System.Collections.Generic;
using CCSWE.FiveHundredPx.Interfaces;

namespace CCSWE.FiveHundredPx
{
    public class PhotoFilter : IPhotoFilter
    {
        #region Constructor
        public PhotoFilter()
        {
            Feature = Feature.FreshToday;
            Sizes = GetAllSizes();
        }

        public PhotoFilter(long userId)
        {
            Feature = Feature.User;
            Sizes = GetAllSizes();
            UserId = userId;
        }

        public PhotoFilter(Feature feature, Categories exclude)
        {
            Categories = exclude;
            Feature = feature;
            Sizes = GetAllSizes();
        }
        #endregion
        
        #region Public Properties
        public Categories Categories { get; set; }
        public Feature Feature { get; set; }
        public FilterMode FilterMode { get; set; }
        public List<int> Sizes { get; set; }
        public Sort Sort { get; set; }
        public SortDirection SortDirection { get; set; }
        public long UserId { get; set; }
        #endregion

        #region Public Properties
        public override string ToString()
        {
            return UrlBuilder.GetPhotos(this);
        }
        #endregion

        #region Private Methods
        private static List<int> GetAllSizes()
        {
            return new List<int>
            {
                //Cropped
                1,
                2,
                3,
                100,
                200,
                440,
                600,
                //Uncropped
                4,
                5,
                20,
                21,
                30,
                1080,
                1600,
                2048,
            };
        }
        #endregion
    }
}
