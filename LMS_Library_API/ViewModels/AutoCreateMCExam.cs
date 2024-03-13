using LMS_Library_API.ModelsDTO;
using System.ComponentModel.DataAnnotations;

namespace LMS_Library_API.ViewModels
{
    public class AutoCreateMCExam
    {
        [Required]
        public AutoExamDTO AutoExamDTO { get; set; }

        [Required]
        public int NumberOfExams { get; set; }

        [Required]
        public int NumberOfQuestions { get; set; }

        [Required]
        public int QuestionsEasy { get; set; }

        [Required]
        public int QuestionsMedium { get; set; }

        [Required]
        public int QuestionsHard { get; set; }

    }
}
