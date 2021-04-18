using System;
using System.IO;
using System.Linq;

namespace DanceDanceSudokulution
{
    class SudokuImport
    {
        public static int[,] CsvImporter(string inputLoc)
        {
            var rows = 0;
            var cols = 0;

            // utilize the user input and attempt to load in the CSV via the path provided
            // This will allow us to get a count of rows and cols to then pass into another attempt at getting the full board into a 2d array
            try
            {
                var csvCols = File.ReadAllLines(inputLoc);
                var csvInputTemp = File.ReadAllLines(inputLoc).Select(l => l.Split(',').ToArray()).ToArray();

                rows = csvInputTemp.Count();
                cols = csvCols[0].Split(',').Count();
            }
            catch
            {
                // No code required here as next exception will output message
            }

            // Now we have the rows and cols we can move onto importing the full board in an array of the fixed length
            var csvInputFinal = new string[rows][];

            try
            {
                csvInputFinal = File.ReadAllLines(inputLoc).Select(l => l.Split(',').ToArray()).ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            // Create the new board array 
            var boardArray = new int[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    boardArray[i, j] = Convert.ToInt32(csvInputFinal[i][j]);
                }
            }
            return boardArray;
        }
    }
}
