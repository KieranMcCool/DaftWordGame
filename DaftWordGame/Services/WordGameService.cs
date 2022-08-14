using DaftWordGame.Models;
using System.Net.Http.Json;

namespace DaftWordGame.Services
{
    public class WordGameService
    {
        private const int NUMBER_OF_WORDS = 5;
        private IEnumerable<string>? _wordList = null;
        private readonly HttpClient _httpClient;
        private Random rng = new Random();

        public WordGameService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GameState> GetNewGame()
        {
            await GetWordList();

            int[] pickedIndexes = new int[NUMBER_OF_WORDS];
            string[] pickedWords = new string[NUMBER_OF_WORDS];

            for (int i = 0; i < NUMBER_OF_WORDS; i++)
            {
                var index = RandomWordIndex(pickedIndexes);

                pickedIndexes[i] = index;
                pickedWords[i] = _wordList.ElementAt(index);
            }

            var gameState = new GameState(pickedWords);

            return gameState;
        }

        private int RandomWordIndex(int[] pickedIndexes)
        {
            int index;

            do
            {
                index = rng.Next(0, _wordList.Count());
            }
            while (pickedIndexes.Contains(index));

            return index;
        }

        private async Task GetWordList()
        {
            if (_wordList != null)
                return;

            var words = await _httpClient.GetStringAsync("sample-data/WordList.txt");
            _wordList = words.ToUpper().Split("\n");
        }
    }
}
