namespace Repository.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class QuestionEntity
    {
        public int Id { get; set; }
        [Display(Name="Question Title")]
        public string Title { get; set; }
        public string Answer { get; set; }

        [Display(Name = "Level of difficulty")]
        public int DifficultyLevel { get; set; }

        [Required(ErrorMessage = "Please select a questiontype")]
        public int QuestionTypeEntityId { get; set; }

        [ForeignKey("QuestionTypeEntityId")]
        public virtual QuestionTypeEntity QuestionType { get; set; }
    }
}