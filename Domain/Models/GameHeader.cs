using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class GameHeader
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public bool IsFinished { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public List<GameQuestion> Qustions { get; set; }
    }
}
