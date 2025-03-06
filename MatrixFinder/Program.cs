// See https://aka.ms/new-console-template for more information
using MatrixFinder;

Console.WriteLine("Hello, Testing WordFinder with console!");
string[] matrix = { "abcdc", "fgwio", "chill", "pqnsd", "uvdxy" };
string[] wordStream = { "cold", "wind", "snow", "chill" };

WordFinder wordFinder = new WordFinder(matrix);

var result = wordFinder.Find(wordStream);


foreach (var item in result)
{
    Console.WriteLine(item);
}
