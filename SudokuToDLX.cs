using System;
using System.Linq;

namespace DanceDanceSudokulution
{
    class SudokuToDLX
    {
        public static int[,] CreateDLXGrid(int [,] sudokuBoard)
        {
            // Get the total columns in the sudokuBoard
            var cols = sudokuBoard.GetLength(1);

            // Get the total rows in the sudokuBoard
            var rows = sudokuBoard.GetLength(0);
            // Create the size of the set cover grid based on the size of the sudokuBoard
            var emptySetCoverBoard = DetermineSetCoverBoard(sudokuBoard, rows, cols);

            var setCoverBoard = emptySetCoverBoard;

            // Create the rowArray based on the incoming sudokuboard
            var rowArray = new int[rows];
            
            // take first row of the sudoku board and turn it into a 1d array
            for (int m = 0; m < sudokuBoard.GetLength(0); m++)
            {
                // turn row by row into a 1d array
                rowArray = SudokuRowToArray(sudokuBoard, rows, rowArray, m);

                // get the first rowArray we created from the m row of the sudoku board
                // Then pipe value by value into the new setgrid
                setCoverBoard = SudokuValueToSetCover(rowArray, setCoverBoard, m);
            }
            return setCoverBoard;
        }
        private static int DetermineSudokuCell(int sudokuRow, int sudokuCol)
        {
            // Equation to find currently occupying cell:
            // R = Row, C = col
            // R * 9 = X
            // X - 9 = Y
            // y + c = ans

            // Need to plus 1 to deal with counting from 0
            var row = sudokuRow + 1;

            var x = row * 9;
            var y = x - 9;
            var sudokuCell = y + sudokuCol;

            return sudokuCell;
        }

        private static int[,] DetermineSetCoverBoard(int[,] sudokuBoard, int rows, int cols)
        {
            var setCoverConstraints = (cols * rows) * 4;
            var setCoverRows = (cols * rows) * 9;

            var setCoverBoard = new int[setCoverRows,setCoverConstraints];
            return setCoverBoard;
        }

        private static int DetermineSetCoverRow(int sudokuCell, int digit)
        {
            // To figure out the setCoverRow to place a digit:
            // Cell number - 1 = x
            // x * 9 = Y
            // y + digit = answer

            var x = sudokuCell - 1;
            var y = x * 9;
            var setCoverRow = y + digit;

            return setCoverRow;
        }

        private static int[,] ModifySetCoverBoard(int[,] emptySetCoverBoard, int firstCoverColumn, int secondCoverColumn, int thirdCoverColumn, int fourthCoverColumn, int row)
        {
            emptySetCoverBoard[row, firstCoverColumn] = 1;
            emptySetCoverBoard[row, secondCoverColumn] = 1;
            emptySetCoverBoard[row, thirdCoverColumn] = 1;
            emptySetCoverBoard[row, fourthCoverColumn] = 1;

            return emptySetCoverBoard;
        }

        private static int[] SudokuRowToArray(int [,] sudokuBoard, int rows, int[] rowArray, int m)
        {
            for (int i = 0; i < rows; i++)
            {
                rowArray[i] = sudokuBoard[m, i];
            }
            return rowArray;
        }

        private static int[,] SudokuValueToSetCover(int[] rowArray, int[,] emptySetCoverBoard, int sudokuRow)
        {
            double index = 1;

            foreach(int i in rowArray)
            {
                if(i != 0)
                {
                    var sudokuCell = DetermineSudokuCell(sudokuRow, (int)index);

                    var setCoverRowNumber = DetermineSetCoverRow(sudokuCell, i);
                    var arrayColumn = (int)index - 1;
                    // define the box we are up to by how far we are into the row:
                    var box = Math.Ceiling((index / 3));


                    // Take value by value into the setcover board
                    var firstCoverColumn = SCColumnConstraints.FirstConstraintRule(sudokuRow, arrayColumn);
                    var secondCoverColumn = SCColumnConstraints.SecondConstraintRule(sudokuRow, arrayColumn, i);
                    var thirdCoverColumn = SCColumnConstraints.ThirdConstraintRule(arrayColumn, i);
                    var fourthCoverColumn = SCColumnConstraints.FourConstraintRule(sudokuRow, arrayColumn, i, box);

                    emptySetCoverBoard = ModifySetCoverBoard(emptySetCoverBoard, (firstCoverColumn), (secondCoverColumn), (thirdCoverColumn), (fourthCoverColumn), setCoverRowNumber - 1);

                    //debug:
                    Console.WriteLine(setCoverRowNumber);
                }
                index++;
            }

            return emptySetCoverBoard;
        }
    }
}
