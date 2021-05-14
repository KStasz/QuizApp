using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISqlDataAccess
    {
        List<T> LoadData<T, U>(string sql, U parameters, CommandType type = CommandType.Text);
        void SaveData<T>(string sql, T parameters, CommandType type = CommandType.Text);
    }
}
