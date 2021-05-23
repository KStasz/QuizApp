using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;

namespace DataAccess.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly ISqlDataAccess _sqlDataAccess;
        private readonly IQuestionsRepository _questionsRepository;

        public GameRepository(ISqlDataAccess sqlDataAccess, IQuestionsRepository questionsRepository)
        {
            _sqlDataAccess = sqlDataAccess;
            _questionsRepository = questionsRepository;
        }

        public GameHeader CreateGame(GameHeader header)
        {
            string sql = "INSERT INTO GameHeaders (Name, UserId) VALUES (@name, @userId);SELECT last_insert_rowid();";
            int rowId = _sqlDataAccess.LoadData<int, dynamic>(sql, new { name = header.Name, userId = header.UserId }).FirstOrDefault();
            sql = "SELECT Id, Name, UserId, IsFinished, IsActive, CreationDate FROM GameHeaders WHERE Id = @id";
            return _sqlDataAccess.LoadData<GameHeader, dynamic>(sql, new { id = rowId }).FirstOrDefault();
        }

        public void SetGameQuestions(GameHeader header)
        {
            foreach (var item in header.Qustions)
            {
                string sql = "INSERT INTO GameQuestions(GameId, QuestionId) VALUES (@gId, @qId)";
                _sqlDataAccess.SaveData(sql, new { gId = item.GameId, qId = item.QuestionId });
            }
        }

        public void FinishGame(GameHeader header)
        {
            string sql = "UPDATE GameHeaders SET IsFinished = 1 WHERE Id = @hId";
            _sqlDataAccess.SaveData(sql, new { hId = header.Id });
        }

        public GameHeader GetGameHeader(int gameId)
        {
            string sql = "SELECT Id, Name, UserId, IsFinished, IsActive, CreationDate FROM GameHeaders WHERE Id = @gId";
            var game = _sqlDataAccess.LoadData<GameHeader, dynamic>(sql, new { gId = gameId }).FirstOrDefault();
            game.Qustions = GetGameQuestionsList(game);
            return game;
        }

        public List<GameHeader> GetGameHeadersList()
        {
            string sql = "SELECT Id, Name, UserId, IsFinished, IsActive CreationDate FROM GameHeaders WHERE IsActive = 1";
            var games = _sqlDataAccess.LoadData<GameHeader, dynamic>(sql, new { });
            foreach (var item in games)
            {
                item.Qustions = GetGameQuestionsList(item);
            }
            return games;
        }

        public List<GameQuestion> GetGameQuestionsList(GameHeader header)
        {
            string sql = "SELECT Id, GameId, QuestionId, UserAnswerId, IsActive, CreationDate FROM GameQuestions WHERE GameId = @gId AND IsActive = 1";
            var result = _sqlDataAccess.LoadData<GameQuestion, dynamic>(sql, new { gId = header.Id });
            foreach (var item in result)
            {
                item.Game = header;
                item.Question = _questionsRepository.GetQuestion(item.QuestionId);
            }
            return result;
        }

        public GameQuestion GetQuestion(int questionId)
        {
            string sql = "SELECT Id, GameId, QuestionId, UserAnswerId, IsActive, CreationDate WHERE IsActive = 1";
            return _sqlDataAccess.LoadData<GameQuestion, dynamic>(sql, new { }).FirstOrDefault();
        }

        public void SendAnswer(GameQuestion question)
        {
            string sql = "UPDATE GameQuestions SET UserAnswerId = @answerId WHERE Id = @qId";
            _sqlDataAccess.SaveData(sql, new { answerId = question.Answer.Id, qId = question.Id });
        }
    }
}
