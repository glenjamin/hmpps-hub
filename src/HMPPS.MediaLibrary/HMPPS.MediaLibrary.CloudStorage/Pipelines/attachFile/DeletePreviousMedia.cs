using System;
using System.Linq;
using HMPPS.MediaLibrary.CloudStorage.Helpers;
using HMPPS.MediaLibrary.CloudStorage.Interface;
using HMPPS.MediaLibrary.CloudStorage.Provider;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.Attach;
using Sitecore.StringExtensions;
using HMPPS.ErrorReporting;
using HMPPS.Utilities.Helpers;

namespace HMPPS.MediaLibrary.CloudStorage.Pipelines.AttachFile
{
    /// <summary>
    /// Deletes media from Cloud storage that was previously associated with item
    /// </summary>
    public class DeletePreviousMedia
    {
        private ICloudStorageProvider _cloudStorage;
        private ILogManager _logManager;

        public DeletePreviousMedia(ILogManager logManager)
        {
            _cloudStorage = new CloudStorageProvider();
            _logManager = logManager;
        }

        /// <summary>
        /// Deletes media from Cloud storage that was previously associated with item
        /// </summary>
        /// <param name="args"></param>
        public void Process(AttachArgs args)
        {
            Assert.ArgumentNotNull(args, "args");

            if (!args.MediaItem.FileBased)
                return;

            _logManager.LogAudit("Deleting '{0}' from Cloud storage".FormatWith(args.MediaItem.FilePath), GetType());

            PipelineHelper.AddContainerNameToArgs(args,  GetContainerNameFromFilePath(args.MediaItem.FilePath));
            _cloudStorage.Delete(args.MediaItem);
        }

        private string GetContainerNameFromFilePath(string filePath)
        {
            return filePath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
        }
    }
}
