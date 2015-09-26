using System;

namespace Portfolio.Dal.Model
{
    public partial class News
    {
        partial void OnInit()
        {
            var utcDate = DateTime.UtcNow;
            CreateDate = utcDate;
            UpdateDate = utcDate;
        }
    }
}