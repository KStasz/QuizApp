using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class UserResultsModel
    {
        public int Id { get; set; }
        public string GameName { get; set; }
        public string QuestionName { get; set; }
        public string AnswerContent { get; set; }
        public bool IsCorrect { get; set; }
    }
}
