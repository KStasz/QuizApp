using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class QuestionsComparer : IEqualityComparer<QuestionModel>
    {
        public bool Equals(QuestionModel x, QuestionModel y)
        {
            return x.Name.Equals(y.Name, StringComparison.InvariantCultureIgnoreCase);
        }

        public int GetHashCode([DisallowNull] QuestionModel obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
