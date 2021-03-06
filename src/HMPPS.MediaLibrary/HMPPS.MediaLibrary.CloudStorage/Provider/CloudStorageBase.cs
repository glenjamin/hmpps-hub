using System.Text.RegularExpressions;
using HMPPS.MediaLibrary.CloudStorage.Interface;
using Sitecore.Configuration;
using Sitecore.Data.Items;
using Sitecore.Resources.Media;
using Sitecore.StringExtensions;

namespace HMPPS.MediaLibrary.CloudStorage.Provider
{
    public abstract class CloudStorageBase : ICloudStorage
    {
        public abstract string Put(MediaItem media, string containerName);

        public abstract string Update(MediaItem media);

        public abstract bool Delete(string filename);

        public abstract void Move(Item item, string fromPath);

        public abstract string GetUrlWithSasToken(MediaItem media, int expiryMinutes);

        #region Helper
        /// <summary>
        /// Extracts the filepath after the prefix link of the media item from the media url
        /// </summary>
        /// <param name="media">MediaItem</param>
        /// <returns>string filepath</returns>
        protected string ParseMediaFileName(MediaItem media)
        {
            string filename = MediaManager.GetMediaUrl(media);

            Regex regex = new Regex(@"(?<={0}/).+".FormatWith(Settings.Media.MediaLinkPrefix));
            Match match = regex.Match(filename);
            if (match.Success)
                filename = match.Value;

            return filename;
        }

        #endregion
    }
}
