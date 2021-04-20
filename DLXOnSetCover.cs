using System;
using System.Collections.Generic;
using System.Linq;
using DlxLib;

namespace DanceDanceSudokulution
{
    class DLXOnSetCover
    {
        public static void CompletedSudoku(int [,] setCoverBoard)
        {
            SolvedSetCoverBoard(setCoverBoard);
        }

        private static void SolvedSetCoverBoard(int[,] setCoverBoard)
        {
            var dlx = new Dlx();

            var set = new int[,]
            {
                {0,0,0,0 },
                {0,1,0,1 },
                {1,0,1,0 }
            };

            var solvedSetCoverBoard = dlx.Solve(set).Take(1);

            PrintSolutions(setCoverBoard,solvedSetCoverBoard);
        }

        private static void PrintSolutions(int[,] matrix, IEnumerable<Solution> solutions)
          {
            // ReSharper disable ReturnValueOfPureMethodIsNotUsed
            solutions.Select((solution, index) =>
            {
                PrintSolution(matrix, solution, index);
                return 0;
            }).ToList();
            // ReSharper restore ReturnValueOfPureMethodIsNotUsed
        }
        private static void PrintSolution(int[,] matrix, Solution solution, int index)
        {
            var rowIndexes = "[" + string.Join(", ", solution.RowIndexes) + "]";
            Console.WriteLine("Solution number {0} ({1}):", index + 1, rowIndexes);

            var numRows = matrix.GetLength(0);
            var numCols = matrix.GetLength(1);

            for (var rowIndex = 0; rowIndex < numRows; rowIndex++)
            {
                Console.Write("matrix[");
                var localRowIndexForCapture = rowIndex;
                ChangeConsoleForegroundColorIf(
                    solution.RowIndexes.Contains(rowIndex),
                    ConsoleColor.Yellow,
                    () => Console.Write("{0}", localRowIndexForCapture));
                Console.Write("]: {");

                for (var colIndex = 0; colIndex < numCols; colIndex++)
                {
                    var value = matrix[rowIndex, colIndex];

                    ChangeConsoleForegroundColorIf(
                        solution.RowIndexes.Contains(rowIndex) && value != 0,
                        ConsoleColor.Yellow,
                        () => Console.Write("{0}", value));

                    if (colIndex + 1 < numCols)
                    {
                        Console.Write(", ");
                    }
                }
                Console.WriteLine("}");
            }

            Console.WriteLine();
        }
        private static void ChangeConsoleForegroundColorIf(bool condition, ConsoleColor consoleColor, Action action)
        {
            var oldForegroundColor = Console.ForegroundColor;

            if (condition)
            {
                Console.ForegroundColor = consoleColor;
            }

            action();

            if (condition)
            {
                Console.ForegroundColor = oldForegroundColor;
            }
        }
    }
}
