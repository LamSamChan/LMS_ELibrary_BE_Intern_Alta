using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace LMS_Library_API.Models.AboutUser
{
    public class QnALikes
    {
        [Key]
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [Column(TypeName ="varchar")]
        [AllowNull]
        public string QuestionsLikedID { get; set; }

        [Column(TypeName ="varchar")]
        [AllowNull]
        public string AnswersLikedID { get; set; }

        //navigation property
        public virtual User User { get; set; }
    }

    public class QnALikeID
    {
        public int Id { get; set; }
    }
}
