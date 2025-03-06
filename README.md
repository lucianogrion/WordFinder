# WordFinder
Challenge for WordFinder
![Diagrama del WordFinder de Expresión](backgroung.png)

# Word Search in Character Matrix


## Description
This project provides a C# class that efficiently searches for words from a large word stream within a given character matrix. The class is designed to handle high-performance lookups and can be used in various applications such as word games and text analysis.

## Project Structure
- **MatrixFinder**: Main project containing the word search logic.
- **TestingFinder**: Unit testing project for verifying functionality.

## Features
- Efficient searching of words in a character matrix
- Supports horizontal, vertical, and diagonal word searches
- Optimized for handling large streams of words

## Usage
### Example
```csharp
string[] matrix = { "abcdc", "fgwio", "chill", "pqnsd", "uvdxy" };
string[] wordStream = { "cold", "wind", "snow", "chill" };

WordFinder wordFinder = new WordFinder(matrix);

var result = wordFinder.Find(wordStream);


foreach (var item in result)
{
    Console.WriteLine(item);
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
Lgrion


