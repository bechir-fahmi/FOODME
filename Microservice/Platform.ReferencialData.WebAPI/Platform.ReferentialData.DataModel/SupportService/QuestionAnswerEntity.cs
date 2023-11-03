using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ReferentialData.DataModel.SupportService
{
    public class QuestionAnswerEntity: ReferentialDataBase
    {
        public int Id { get; set; }
        public Guid QuestionLabelCode { get; set; }
        public Guid AnswerLabelCode { get; set; }

        public  int SuportCategoryId { get; set; }

        [ForeignKey("SuportCategoryId")]
        public virtual SuportCategoryEntity SuportCategoryFk { get; set; }
    }
}
