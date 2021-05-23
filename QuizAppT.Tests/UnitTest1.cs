using QuizApp.UserWindows;
using QuizApp;
using System;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace QuizAppT.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var gc = App.ServiceProvider.GetRequiredService<NewGameConfig>();
            MethodInfo methodInfo = typeof(NewGameConfig).GetMethod("GetQuestions", BindingFlags.NonPublic | BindingFlags.Instance);
            
        }
    }
}
