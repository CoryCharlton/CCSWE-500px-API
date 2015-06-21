using System;
using System.Collections.Generic;
using System.Linq;
using CCSWE.FiveHundredPx.Models;

namespace CCSWE.FiveHundredPx.Collections
{
    public class ImageCollection: List<Image>
    {
        #region Private Static Fields
        private static readonly List<int> CroppedIds = new List<int>
        {
            1, // 70px x 70px
            2, // 140px x 140px
            3, // 280px x 280px
            100, // 100px x 100px
            200, // 200px x 200px
            440, // 440px x 440px
            600, // 600px x 600px
        };

        private static readonly Dictionary<int, double> Sizes = new Dictionary<int, double>
	    {
            // Cropped
            {600, 600},
            {440, 440},
            {3, 280},
            {200, 200},
            {2, 140},
            {100, 100},
            {1, 70},

            // Uncropped
	        {2048, 2048},
	        {1600, 1600},
	        {5, 1170},
	        {1080, 1080},
	        {4,900},
	        {21,600},
	        {20,300},
	        {30,256},
	    };

        private static readonly List<int> UncroppedIds = new List<int>
        {
            4, // 900px on the longest edge
            5, // 1170px on the longest edge
            20, // 300px high
            21, // 600px high
            30, // 256px on the longest edge
            1080, // 1080px on the longest edge
            1600, // 1600px on the longest edge
            2048, // 2048px on the longest edge
        };
        #endregion

        #region Private Methods
        private void CheckEmpty()
        {
            if (Count <= 0)
            {
                throw new InvalidOperationException("ImageCollection is empty.");
            }            
        }

        private Image GetCroppedImage(double desiredSize)
        {
            CheckEmpty();

            Image nextLargerImage = null;
            double nextLargerSize = 0;

            Image nextSmallerImage = null;
            double nextSmallerSize = 0;

            foreach (var image in this)
            {
                if (!CroppedIds.Contains(image.Size))
                {
                    continue;
                }

                var targetSize = Sizes[image.Size];
                if (targetSize.Equals(desiredSize))
                {
                    return image;
                }

                if (targetSize < desiredSize && (nextSmallerImage == null || targetSize > nextSmallerSize))
                {
                    nextSmallerImage = image;
                    nextSmallerSize = targetSize;
                }
                else if (targetSize > desiredSize && (nextLargerImage == null || targetSize < nextLargerSize))
                {
                    nextLargerImage = image;
                    nextLargerSize = targetSize;
                }
            }

            return nextLargerImage ?? nextSmallerImage;
        }

        private Image GetUncroppedImage(double desiredSize)
        {
            CheckEmpty();

            Image nextLargerImage = null;
            double nextLargerSize = 0;

            Image nextSmallerImage = null;
            double nextSmallerSize = 0;

            foreach (var image in this)
            {
                if (!UncroppedIds.Contains(image.Size))
                {
                    continue;
                }

                var targetSize = Sizes[image.Size];
                if (targetSize.Equals(desiredSize))
                {
                    return image;
                }

                if (targetSize < desiredSize && (nextSmallerImage == null || targetSize > nextSmallerSize))
                {
                    nextSmallerImage = image;
                    nextSmallerSize = targetSize;
                }
                else if (targetSize > desiredSize && (nextLargerImage == null || targetSize < nextLargerSize))
                {
                    nextLargerImage = image;
                    nextLargerSize = targetSize;
                }
            }

            return nextLargerImage ?? nextSmallerImage;
        }
        #endregion

        #region Public Methods
        public Image GetImage(int sizeId)
        {
            if (!CroppedIds.Contains(sizeId) && !UncroppedIds.Contains(sizeId))
            {
                throw new ArgumentException("Invalid 'sizeId'.", "sizeId");
            }

            return GetImage(Sizes[sizeId], CroppedIds.Contains(sizeId));
        }

        public Image GetImage(double desiredSize, bool preferCropped)
        {
            return preferCropped
                ? GetCroppedImage(desiredSize) ?? GetUncroppedImage(desiredSize)
                : GetUncroppedImage(desiredSize) ?? GetCroppedImage(desiredSize);
        }
        #endregion

        #region Public Static Methods
        public static List<int> GetAllSizeIds()
        {
            var imageSizes = new List<int>();

            imageSizes.AddRange(CroppedIds);
            imageSizes.AddRange(UncroppedIds);

            return imageSizes;
        }        
        #endregion
    }
}
