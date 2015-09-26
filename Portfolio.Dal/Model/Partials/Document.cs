using System;

namespace Portfolio.Dal.Model
{
    public partial class Document
    {
        partial void OnInit()
        {
            var utcDate = DateTime.UtcNow;
            CreateDate = utcDate;
            UpdateDate = utcDate;
        }
    }
}