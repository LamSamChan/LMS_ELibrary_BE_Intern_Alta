using LMS_Library_API.Models.Exams;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Library_API.ModelsDTO
{
    public class MC_QuestionExamDTO
    {
        public virtual MC_QuestionBankDTO QuestionBanks { get; set; }
    }
}
