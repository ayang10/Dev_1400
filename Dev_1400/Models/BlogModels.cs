﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dev_1400.Models
{
    public class Post
    {

        public Post()
        {
            this.Comments = new HashSet<Comment>();
        }
        public int Id { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset? UpdateDate { get; set; }
        public string Title { get; set; }


        [AllowHtml]
        public string BodyText { get; set; }

        public string MediaUrl { get; set; }
        
        //joined table //add virtual for lazy loading
        public virtual ICollection<Comment> Comments { get; set; }

        private int BodyTextLimit = 900;
        
        public string BodyTextTrimmed
        {
            get
            {
                if (this.BodyText.Length > this.BodyTextLimit)
                    return this.BodyText.Substring(0, this.BodyTextLimit) + "...";
                else
                    return this.BodyText;
            }
        }


        public static class ImageUploadValidator
        {
            public static bool IsWebFriendlyImage(HttpPostedFileBase fileUpload)
            {
                //check for actual object
                if (fileUpload == null)
                    return false;

                //check size - file must be  less than 2 MB and greater than 1 KB
                if (fileUpload.ContentLength > 2 * 1024 * 1024)
                    return false;

                try
                {
                    using (var img = Image.FromStream(fileUpload.InputStream))
                    {
                        return ImageFormat.Jpeg.Equals(img.RawFormat) ||
                               ImageFormat.Png.Equals(img.RawFormat) ||
                               ImageFormat.Gif.Equals(img.RawFormat);
                    }
                }

                catch
                {

                    return false;
                }
            }
        }

    }



    public class Comment
    {
        public Comment()
        {
            this.Comments = new HashSet<Comment>();
        }
        public int Id { get; set; }
        public int PostId { get; set; }
        public string AuthorId { get; set; } //author
        public string EditorId { get; set; }//editior 
        [AllowHtml]
        public string BodyText { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
        public int? ParentCommentId { get; set; }

        //virtual means navigation
        public virtual Post Post { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public virtual ApplicationUser Editor { get; set; }
        public virtual Comment ParentComment { get; set; }

        //each comments under another comments //add virtual for lazy loading
        public virtual ICollection<Comment> Comments { get; set; }
    }
    
}