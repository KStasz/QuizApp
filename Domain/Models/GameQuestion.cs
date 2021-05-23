using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class GameQuestion
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int QuestionId { get; set; }
        public int UserAnswerId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CretionDate { get; set; }
        public QuestionModel Question { get; set; }
        public GameHeader Game { get; set; }
        public AnswerModel Answer { get; set; }
    }
}
