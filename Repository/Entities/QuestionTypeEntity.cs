namespace Repository.Entities
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class QuestionTypeEntity
    {
        public int Id { get; set; }

        [Display(Name = "Question Type")]
        public string Type { get; set; }

        public virtual IList<QuestionEntity> QuestionEntities { get; set; }
    }
}