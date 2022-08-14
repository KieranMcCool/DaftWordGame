using DaftWordGame.Models;

namespace DaftWordGame.Pages
{
    public partial class Game
    {
        GameState? gameState;
        Dictionary<int, char?> guesses;

        private async Task NewGame()
        {
            gameState = await wordGameService.GetNewGame();
            guesses = GenerateGuesses();
        }

        private Dictionary<int, char?> GenerateGuesses()
        {
            var numbers = gameState.CharMappings.Values.Select(x => x.number);
            var guesses = new Dictionary<int, char?>();

            foreach (var number in numbers)
            {
                guesses.Add(number, null);
            }

            return guesses;
        }

        private int IndexFromChar(char c) => gameState.CharMappings[c].number;
        private bool GuessCorrect(int i) => HasGuess(i) && 
            gameState.CharMappings.TryGetValue(guesses[i].Value, out var charMapping) && 
            charMapping.number == i;

        private bool HasGuess(int i) => guesses[i].HasValue;

        private string LetterStyle(int i)
        {
            if (HasGuess(i))
            {
                return GuessCorrect(i) ?
                    "letter-box-correct" :
                    "letter-box-incorrect";
            }

            return string.Empty;
        }

        private string GetPlaceHolder(char c)
        {
            return gameState.CharMappings[c].revealed ?
                c.ToString() :
                gameState.CharMappings[c].number.ToString();
        }

        protected override async Task OnInitializedAsync() => await NewGame();
    }
}
