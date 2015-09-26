using System;

namespace Portfolio.Dal.Model
{
    public partial class PhotoGallery
    {
        partial void OnInit()
        {
            var utcDate = DateTime.UtcNow;
            CreateDate = utcDate;
            UpdateDate = utcDate;
        }
    }
}