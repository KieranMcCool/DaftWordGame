using DaftWordGame.Models;

namespace DaftWordGame.Services
{
    public class WordGameService
    {
        private const int NUMBER_OF_WORDS = 5;
        private readonly IEnumerable<string> _wordList;

        public WordGameService(IEnumerable<string> wordList)
        {
            _wordList = wordList;
        }

        public GameState GetNewGame()
        {
            
            var gameState = new GameState()
        }
    }
}
