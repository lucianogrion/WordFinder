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

        public WordFinder(IEnumerable<string> matrix)
        {
            int nroLinea = 0;

            var lineas = new List<char[]>();

            CheckMatrix(matrix);

            foreach (var item in matrix)
            {
                char[] linea = item.ToCharArray();
                
                int nroCol = 0;
                foreach (char c in linea)
                {
                    if (!_dictionaryPosition.TryGetValue(c, out var positions))
                    {
                        _dictionaryPosition.Add(c, []);
                    }
                    var list= _dictionaryPosition[c];
                    list.Add(new Position(nroLinea, nroCol));
                    nroCol++;
                }
                lineas.Add(linea);

                nroLinea++;
            } 

            _matrix = lineas.ToArray();
        
        }

        private void CheckMatrix(IEnumerable<string> matrix)
        {
            int rows = matrix.Count();
            int cols = matrix.FirstOrDefault()!.Length;
            if (rows!= cols)
            {
                throw new Exception("Error on Matrix dimmension");
            }


        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {

            Parallel.ForEach(wordstream, word =>
            {
                int CountWords = CountWordsInMatrix(word);
                _wordsOccurrencies.TryAdd(word, CountWords);
                
            });
            
            return _wordsOccurrencies.OrderByDescending(x => x.Value).Select(s=> s.Key).ToList();

        }

        private int CountWordsInMatrix(string word)
        {
            int acumulatorCount =0;
            //get the first letter
            char FirstLetter = word[0]; 
            
            //If There is no letter should be no word at all text
            if (!_dictionaryPosition.TryGetValue(FirstLetter,out var value))
            {
                return 0;
            }

            foreach (Position position in _dictionaryPosition[FirstLetter])
            {
                int countPosition= CountInPosition(word,position);
                acumulatorCount += countPosition;
            }
            return acumulatorCount;
            
        }

        private int CountInPosition(string word, Position position)
        {
            int count = 0 ;
            //add limits to the matrix
            //add vertically and horizontally backwards
            //make a generic proc (sending parameters)

            //search for the word horizontally
            int offset = 0;
            bool founded = true;
            foreach (char letter in word)
            {
                if (position.x + offset > _matrix[0].Length - 1)
                {
                    founded = false; break;
                }

                if (_matrix[position.x+ offset][position.y] != letter)
                {
                    founded = false; break;
                }
                offset++;
            }

            if (founded)
            {
                count++;
            }
            
            //search for the word vertically
            offset = 0;
            founded = true;
            if (true)
            {
                foreach (char letter in word)
                {
                    if (position.y + offset > _matrix[0].Length-1)
                    {
                        founded = false; break;
                    }

                    if (_matrix[position.x][position.y + offset] != letter)
                    {
                        founded = false; break;
                    }
                    offset++;
                }
            }
            

            if (founded)
            {
                count++;
            }

            return count;
        }
    }
}
