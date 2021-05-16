using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class QuestionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string QuestionContent { get; set; }
        public int CategoryId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public CategoryModel assignedCategory { get; set; }

        public void AssignQuestion(CategoryModel model)
        {
            assignedCategory = model;
            CategoryId = model.Id;
        }
    }
}
