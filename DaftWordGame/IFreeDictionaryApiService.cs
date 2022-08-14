using DaftWordGame.Models;
using Refit;

public interface IFreeDictionaryApiService
{
    [Get("/entries/en/{word}")]
    Task<WordData[]> GetWordDefintion(string word);
}