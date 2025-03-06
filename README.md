# WordFinder
Challenge for WordFinder
![Diagrama del WordFinder de Expresión](backgroung.png)

# Word Search in Character Matrix

## Description
This project provides a C# class that efficiently searches for words from a large word stream within a given character matrix. The class is designed to handle high-performance lookups and can be used in various applications such as word games and text analysis.

## Features
- Efficient searching of words in a character matrix
- Supports horizontal, vertical, and diagonal word searches
- Optimized for handling large streams of words

## Usage
### Example
```csharp
char[,] matrix = {
    {'T', 'H', 'I', 'S'},
    {'I', 'S', 'A', 'T'},
    {'E', 'S', 'T', 'W'},
    {'O', 'R', 'D', 'S'}
};

List<string> wordsToFind = new List<string> {"THIS", "TEST", "WORDS"};

var searcher = new WordFinder(matrix);
List<string> foundWords = searcher.Find(wordsToFind);

foreach (var word in foundWords)
{
    Console.WriteLine("Found: " + word);
}
```

## Implementation
```csharp
public class WordFinder
{
    public WordFinder(IEnumerable<string> matrix)
    {
        ...
    }

    public IEnumerable<string> Find(IEnumerable<string> wordstream)
    {
        ...
    }
}
```

## Installation
1. Clone the repository:
   ```sh
   git clone https://github.com/yourusername/word-search-matrix.git
   ```
2. Open the project in Visual Studio
3. Build and run the solution

## License
This project is licensed under the MIT License.

## Contributions
Feel free to submit pull requests or open issues to improve this project.

## Author
lgrion

