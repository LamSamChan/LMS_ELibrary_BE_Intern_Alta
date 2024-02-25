using LMS_Library_API.Models;

namespace LMS_Library_API.ModelsDTO
{
    public class StudentQnALikesDTO
    {
        public Guid studentId { get; set; }
        public List<QnALikeID> QuestionsLikedID { get; set; }
        public List<QnALikeID> AnswersLikedID { get; set; }
    }
}
