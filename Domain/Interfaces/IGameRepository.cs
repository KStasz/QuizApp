using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGameRepository
    {
        List<GameHeader> GetGameHeadersList();
        GameHeader GetGameHeader(int gameId);
        List<GameQuestion> GetGameQuestionsList(GameHeader header);
        GameQuestion GetQuestion(int questionId);
        GameHeader CreateGame(GameHeader header);
        void SetGameQuestions(GameHeader header);
        void FinishGame(GameHeader header);
        void SendAnswer(GameQuestion question);
    }
}
