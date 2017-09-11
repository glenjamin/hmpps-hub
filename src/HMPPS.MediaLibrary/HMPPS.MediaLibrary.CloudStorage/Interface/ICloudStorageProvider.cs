﻿using Sitecore.Data.Items;

namespace HMPPS.MediaLibrary.CloudStorage.Interface
{
    interface ICloudStorageProvider
    {
        string Put(MediaItem media);
        string Update(MediaItem media);
        bool Delete(MediaItem media);
    }
}
