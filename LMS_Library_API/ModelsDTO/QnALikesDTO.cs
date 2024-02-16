using LMS_Library_API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace LMS_Library_API.ModelsDTO
{
    public class QnALikesDTO
    {
        public Guid UserId { get; set; }
        public List<QnALikeID> QuestionsLikedID { get; set; }
        public List<QnALikeID> AnswersLikedID { get; set; }
    }
}
