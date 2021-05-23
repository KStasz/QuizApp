using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;

namespace DataAccess.Repositories
{
    public class ResultRepository : IResultsRepository
    {
        private readonly ISqlDataAccess _sqlDataAccess;

        public ResultRepository(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }

        public List<UserResultsModel> GetUserResult(int userId)
        {
            string sql = $"SELECT GQ.Id, GH.name as 'GameName', Q.Name AS 'QuestionName', A.AnswerContent, A.IsCorrect FROM GameQuestions GQ " +
                $"INNER JOIN Answers A ON GQ.UserAnswerId = A.Id " +
                $"INNER JOIN GameHeaders GH ON GQ.GameId = GH.Id " +
                $"INNER JOIN Questions Q ON Q.Id = GQ.QuestionId " +
                $"WHERE GH.UserId = @uId";
            return _sqlDataAccess.LoadData<UserResultsModel, dynamic>(sql, new { uId = userId});
        }
    }
}
