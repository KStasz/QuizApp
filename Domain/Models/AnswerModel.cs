using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AnswerModel
    {
        public int Id { get; set; }
        public string AnswerContent { get; set; }
        public int QuestionId { get; set; }
        public bool IsCorrect { get; set; }
        public bool IsActive { get; set; }
        public bool IsSelected { get; set; } = false;
        public DateTime CreationDate { get; set; }
    }
}
