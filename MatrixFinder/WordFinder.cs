using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixFinder
{ 

    public class Position
    {
        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int x;
        public int y;
    }

    public class WordFinder
    {
        private char[][] _matrix;
        private Dictionary<char, List<Position>> _dictionaryPosition = new Dictionary<char,List<Position>>();
        private ConcurrentDictionary<string, int> _wordsOccurrencies = new ConcurrentDictionary<string, int>();

        /// <summary>
        /// Constructor to load the matrix on memory
        /// </summary>
        /// <param name="matrix"></param>
        public WordFinder(IEnumerable<string> matrix)
        {
            int lineNumber = 0;

            var lineas = new List<char[]>();
            //Check the dimension of the Matrix
            CheckMatrix(matrix);

            foreach (var item in matrix)
            {
                char[] line = item.ToCharArray();
                
                int nroCol = 0;
                foreach (char c in line)
                {
                    if (!_dictionaryPosition.TryGetValue(c, out var positions))
                    {
                        _dictionaryPosition.Add(c, []);
                    }
                    var list= _dictionaryPosition[c];
                    list.Add(new Position(lineNumber, nroCol));
                    nroCol++;
                }
                lineas.Add(line);

                lineNumber++;
            } 

            _matrix = lineas.ToArray();
        
        }

        /// <summary>
        /// Check the dimension of the Matrix
        /// </summary>
        /// <param name="matrix"></param>
        /// <exception cref="Exception"></exception>
        private void CheckMatrix(IEnumerable<string> matrix)
        {
            int rows = matrix.Count();
            int cols = matrix.FirstOrDefault()!.Length;
            if (rows!= cols)
            {
                throw new Exception("Error on Matrix dimmension");
            }


        }

        /// <summary>
        /// The method will find some words 
        /// </summary>
        /// <param name="wordstream">The words to seach</param>
        /// <returns>Ordered list of found words</returns>
        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            //Count/Find each word in parallel
            Parallel.ForEach(wordstream, word =>
            {
                int CountWords = CountWordsInMatrix(word);
                _wordsOccurrencies.TryAdd(word, CountWords);
                
            });
            
            return _wordsOccurrencies.OrderByDescending(x => x.Value).Select(s=> s.Key).ToList();

        }

        /// <summary>
        /// CountWordsInMatrix
        /// </summary>
        /// <param name="word">the selected word </param>
        /// <returns>the number of the word appeareances on the matrix</returns>
        private int CountWordsInMatrix(string word)
        {
            int acumulatorCount =0;
            //get the first letter of the word
            char FirstLetter = word[0]; 
            
            if (!_dictionaryPosition.TryGetValue(FirstLetter,out var value))
            {
                //If There is no letter on the _dictionaryPosition
                //should be no word at all text
                return 0;
            }

            foreach (Position position in _dictionaryPosition[FirstLetter])
            {
                int countPosition= CountInPosition(word,position);
                acumulatorCount += countPosition;
            }
            return acumulatorCount;
        }

        /// <summary>
        /// CountInPosition
        /// </summary>
        /// <param name="word"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        private int CountInPosition(string word, Position position)
        {
            int count = 0 ;

            //search for the word horizontally
            count += SearchForWord(word,true, position);
            count += SearchForWord(word,false, position);

            return count;
        }

        /// <summary>
        /// SearchForWord
        /// </summary>
        /// <param name="word"></param>
        /// <param name="Horizontal"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        private int SearchForWord(string word,bool Horizontal, Position position)
        {

            int Offsetx = 0;
            int Offsety = 0;
            bool founded = true;
            
            foreach (char letter in word)
            {
                //Check limits of the matrix
                if ((position.x + Offsetx > _matrix[0].Length - 1)||
                    (position.y + Offsety > _matrix[0].Length - 1)
                    )
                {
                    founded = false; break;
                }


                if (_matrix[position.x + Offsetx][position.y+Offsety] != letter)
                {
                    founded = false; break;
                }
                if (Horizontal)
                {
                    Offsetx++;
                } else
                {
                    Offsety++;
                }
                
            }
            //TODO: add vertically and horizontally backwards

            return founded ? 1 : 0;
        }
    }
}
