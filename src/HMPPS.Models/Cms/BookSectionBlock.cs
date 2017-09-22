﻿using HMPPS.Models.Common;

namespace HMPPS.Models.Cms
{
    public class BookSectionBlock
    {
        public string Title { get; set; }
        public Link Link { get; set; }
        public Image Image { get; set; }
        public bool IsBookPage { get; set; }
        public File BookFile { get; set; }

        public BookSectionBlock()
        {
            Link = new Link();
            Image = new Image();
            BookFile = new File();
        }
    }
}