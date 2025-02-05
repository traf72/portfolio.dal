//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Portfolio.Dal.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class PhotoGallery
    {
        public PhotoGallery()
        {
            this.Images = new HashSet<PhotoGalleriesFile>();
            this.News = new HashSet<News>();
            OnInit();
        }
        partial void OnInit();
        public int Id { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public string Name { get; set; }
        public bool IsPublished { get; set; }
        public Nullable<System.DateTime> PublishDate { get; set; }
        public int AuthorId { get; set; }
        public string ContainerId { get; set; }
        public Nullable<int> MainImageId { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual User User { get; set; }
        public virtual ICollection<PhotoGalleriesFile> Images { get; set; }
        public virtual File MainImage { get; set; }
        public virtual ICollection<News> News { get; set; }
    }
}
