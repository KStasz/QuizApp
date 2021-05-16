using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public List<QuestionModel> assignedQuestions { get; set; }
    }
}
