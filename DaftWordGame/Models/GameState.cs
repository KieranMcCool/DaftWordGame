using DaftWordGame.Extensions;
using System.Text;

namespace DaftWordGame.Models
{
    public class GameState
    {
        public IEnumerable<Word> Words { get; private set; }
        public IDictionary<char, (int number, bool revealed)> CharMappings { get; set; }
        public bool GameComplete => CharMappings.Values.Select(x => x.revealed).All(x => x.Equals(true));

        public GameState(IEnumerable<Word> words)
        {
            Words = words;
            CharMappings = new Dictionary<char, (int number, bool revealed)>();
            PopulateCharMappings();
        }

        private void PopulateCharMappings()
        {
            var stringBuilder = new StringBuilder();

            foreach (var word in Words)
                stringBuilder.Append(word.Text);

            var allCharacters = stringBuilder.ToString()
                .Distinct().ToList();

            allCharacters.Shuffle();

            for (int i = 0; i < allCharacters.Count(); i++)
            {
                CharMappings.Add(allCharacters[i], (i, false));
            }
        }
    }
}
