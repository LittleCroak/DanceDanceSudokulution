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
            var emptySetCoverBoard = DetermineSetCoverBoard(rows, cols);

            var setCoverBoard = emptySetCoverBoard;

            //Counter to find which cell we are operating in
            var sudokuCellCounter = 0;

            //Counter for the SetCoverRow we are up to
            var setCoverRowNumber = 0;


            // iterate through sudokuBoard to get each value
            for (int m = 0; m < rows; m++)
            {
                for (int n = 0; n < cols; n++)
                {
                    //Get current SudokuDigit
                    var currentSudokuDigit = sudokuBoard[m, n];
                    
                    // if the cell is zero we have to add the possibility of the cell being numbers 1 - 9
                    if (currentSudokuDigit == 0)
                    {
                        for(int i = 1; i <= cols; i++)
                        {
                            setCoverBoard = SudokuValueToSetCover(sudokuCellCounter, setCoverBoard, currentSudokuDigit, n, m, sudokuCellCounter);
                            setCoverRowNumber++;
                        }
                    }

                    if (currentSudokuDigit != 0)
                    {
                        // pipe in each value from the sudokuBoard into this function which will create an equivalent
                        // set cover board to work on later.
                        setCoverBoard = SudokuValueToSetCover(sudokuCellCounter, setCoverBoard, currentSudokuDigit, n, m, sudokuCellCounter);
                        setCoverRowNumber++;
                    }
                    sudokuCellCounter++;
                }
            }

            return setCoverBoard;
        }

        private static int[,] DetermineSetCoverBoard(int rows, int cols)
        {
            var setCoverConstraints = (cols * rows) * 4;
            var setCoverRows = (cols * rows) * 9;

            var setCoverBoard = new int[setCoverRows, setCoverConstraints];
            return setCoverBoard;
        }

        private static int DetermineSetCoverRow(int sudokuCell, int digit)
        {
            // To figure out the setCoverRow to place a digit:
            // Cell number - 1 = x
            // x * 9 = Y
            // y + digit = answer

            var x = sudokuCell;
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

        private static int[,] SudokuValueToSetCover(int sudokuCell, int[,] emptySetCoverBoard, int sudokuDigit, int sudokuColumnNumber, int sudokuRowNumber, int sudokuCellCounter)
        {
            //determine the setcoverRow we are writing too
            var setCoverRow = DetermineSetCoverRow(sudokuCellCounter, sudokuDigit);

            var sudokuBoxNumber = Math.Ceiling(((double)sudokuColumnNumber + 1) / 3);

            // Take values and run them through SetCover Constraint equations to determine columns:
            var firstCoverColumn = SCColumnConstraints.FirstConstraintRule(sudokuRowNumber, sudokuColumnNumber);
            var secondCoverColumn = SCColumnConstraints.SecondConstraintRule(sudokuRowNumber, sudokuColumnNumber, sudokuDigit);
            var thirdCoverColumn = SCColumnConstraints.ThirdConstraintRule(sudokuColumnNumber, sudokuDigit);
            var fourthCoverColumn = SCColumnConstraints.FourConstraintRule(sudokuRowNumber, sudokuColumnNumber, sudokuDigit, sudokuBoxNumber);

            // Modify the EmptySetCoverBoard with the values determined above:
            emptySetCoverBoard = ModifySetCoverBoard(emptySetCoverBoard, firstCoverColumn, secondCoverColumn, thirdCoverColumn, fourthCoverColumn, setCoverRow);

            return emptySetCoverBoard;
        }
    }
}
