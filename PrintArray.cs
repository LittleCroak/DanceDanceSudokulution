using System;
using System.IO;

namespace DanceDanceSudokulution
{
    class PrintArray
    {
        public static void Print2DArray<T>(T[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        public static void Print2DArrayToCsv<T>(T[,] matrix, int[,] setCoverGrid)
        {
            using (StreamWriter outfile = new StreamWriter(@"C:\Users\nick.dryden\OneDrive - Veritec Pty Ltd\Documents\Projects\WizBangSuperSudokuSlayer\output.csv"))
            {
                for (int i = 0; i < setCoverGrid.GetLength(0); i++)
                {
                    for (int j = 0; j < setCoverGrid.GetLength(1); j++)
                    {
                        outfile.Write(setCoverGrid[i, j]);

                        //it is comman and not a tab
                        outfile.Write(",");
                    }
                    //go to next line
                    outfile.Write("\n");

                }
            }
        }
    }
}
