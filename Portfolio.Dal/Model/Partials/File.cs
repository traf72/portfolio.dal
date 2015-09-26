using System;
using System.IO;

namespace Portfolio.Dal.Model
{
    public partial class File
    {
        partial void OnInit()
        {
            var utcDate = DateTime.UtcNow;
            CreateDate = utcDate;
            UpdateDate = utcDate;
        }

        public string Extension
        {
            get { return Path.GetExtension(OriginalName); }
        }
    }
}