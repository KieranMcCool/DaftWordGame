using DaftWordGame.Models;
using System.Net.Http.Json;

namespace DaftWordGame.Services
{
    public class WordGameService
    {
        private const int NUMBER_OF_WORDS = 5;
        private IEnumerable<string>? _lexicon = null;
        private readonly HttpClient _httpClient;
        private readonly IFreeDictionaryApiService _freeDictionaryApiService;
        private Random rng = new Random();

        public WordGameService(HttpClient httpClient, IFreeDictionaryApiService freeDictionaryApiService)
        {
            _httpClient = httpClient;
            _freeDictionaryApiService = freeDictionaryApiService;
        }

        public async Task<GameState> GetNewGame()
        {
            await GetWordList();

            int[] pickedIndexes = new int[NUMBER_OF_WORDS];
            Word[] pickedWords = new Word[NUMBER_OF_WORDS];

            for (int i = 0; i < NUMBER_OF_WORDS; i++)
            {
                var word = await PickWord(pickedIndexes);

                while (!word.validWord)
                    word = await PickWord(pickedIndexes);

                pickedWords[i] = word.word;
                pickedIndexes[i] = word.index;
            }

            var gameState = new GameState(pickedWords);

            return gameState;
        }

        private async Task<(bool validWord, Word word, int index)> PickWord(int[] pickedIndexes)
        {
            var index = RandomWordIndex(pickedIndexes);
            var word = _lexicon.ElementAt(index);

            var definitions = await GetDefinitions(word);

            if (definitions is null)
                return (false, null, index);

            var wordModel = new Word()
            {
                Text = word,
                Definitions = definitions.ToArray()
            };

            return (true, wordModel, index);
        }

        private async Task<IList<string>> GetDefinitions(string word)
        {
            var definitions = new List<string>();

            try
            {
                var definition = (await _freeDictionaryApiService.GetWordDefintion(word))?.FirstOrDefault();

                foreach (var d in definition?.meanings)
                {
                    foreach (var v in d.definitions)
                    {
                        definitions.Add(v.definition);
                    }
                }
            }
            catch (Refit.ApiException _)
            {
                return null;
            }

            return definitions;
        }

        private int RandomWordIndex(int[] pickedIndexes)
        {
            int index;

            do
            {
                index = rng.Next(0, _lexicon.Count());
            }
            while (pickedIndexes.Contains(index));

            return index;
        }

        private async Task GetWordList()
        {
            if (_lexicon != null)
                return;

            var words = await _httpClient.GetStringAsync("sample-data/WordList.txt");
            _lexicon = words.ToUpper().Split("\n");
        }
    }
}
