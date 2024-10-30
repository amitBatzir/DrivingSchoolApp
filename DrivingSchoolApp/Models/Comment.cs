using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingSchoolApp.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int StudentId { get; set; }
        public int TeacherId { get; set; }
        public string TheText { get; set; } = null!;

        public Comment() { }
        public Comment(Models.Comment c)
        {
            CommentId = c.CommentId;
            StudentId = c.StudentId;
            TeacherId = c.TeacherId;
            TheText = c.TheText;         
        }

        public Models.Comment GetModel()
        {
            Models.Comment c = new Models.Comment();
            c.CommentId = CommentId;
            c.StudentId = StudentId;
            c.TeacherId = TeacherId;
            c.TheText = TheText;
            return c;
        }
    }
}

