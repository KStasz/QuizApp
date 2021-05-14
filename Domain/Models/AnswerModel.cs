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
        public string Content { get; set; }
        public bool IsCorrectAnswer { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
