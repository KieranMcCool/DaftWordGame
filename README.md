# Daft Word Game

This is just a super basic word game where you guess the letters of words based on their definitions. 

Definitions are retrieved via the `https://api.dictionaryapi.dev/api/v2` API and words are selected from a dictionary file that started off life as the default Ubuntu dictionary file, however I stripped out all words containing non-ASCII characters.

## Disclaimer

There's probably a nicer way to do a lot of this. This is my first time using Blazor and this project was simply a means of evaluating whether or not I want to learn more.

I'm definitely impresseed with how seamlessly backend C# experience translates into Blazor WASM, there was very little in the way of googling to get this up and running. 