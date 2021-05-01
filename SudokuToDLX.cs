using System;
using System.Collections.Generic;
using System.Linq;

namespace DanceDanceSudokulution
{
    class SudokuToDLX
    {
        public CoverBoard CreateDLXGrid(int [,] sudokuBoard)
        {
            CoverBoard coverBoard = new CoverBoard();
            // Get the total columns in the sudokuBoard
            var cols = sudokuBoard.GetLength(1);

            // Get the total rows in the sudokuBoard
            var rows = sudokuBoard.GetLength(0);

            // For clarity this is a definition that gives us the possible number size.
            // For instance a 9x9 = 9 or a 16x16 = 16 and so on. 
            var cellNum = rows;

            //Counter to find which cell we are operating in
            var sudokuCellCounter = 0;

            //Counter for the SetCoverRow we are up to
            var setCoverRowNumber = 0;

            //
            coverBoard.extraCover = new List<List<int>>();

            coverBoard = CreateSetCoverBoard(rows, cols, sudokuBoard, coverBoard.extraCover, setCoverRowNumber, sudokuCellCounter, cellNum);

            return coverBoard;
        }

        private CoverBoard CreateSetCoverBoard(int rows, int cols, int[,] sudokuBoard, List<List<int>> setCoverBoard, int setCoverRowNumber, int sudokuCellCounter, int cellNum)
        {
            //init object to return
            CoverBoard coverBoard = new CoverBoard();
            coverBoard.indexer = new List<Tuple<int, int, int>>();

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
                        for (int i = 1; i < (cellNum + 1); i++)
                        {
                            setCoverBoard = SudokuValueToSetCover(setCoverBoard, i, n, m, sudokuCellCounter, setCoverRowNumber, cellNum);
                            coverBoard.indexer.Add(new Tuple<int, int, int>(m, n, i));

                            setCoverRowNumber++;
                        }
                    }

                    else
                    {
                        // pipe in each value from the sudokuBoard into this function which will create an equivalent
                        // set cover board to work on later.
                        setCoverBoard = SudokuValueToSetCover(setCoverBoard, currentSudokuDigit, n, m, sudokuCellCounter, setCoverRowNumber, cellNum);
                        coverBoard.indexer.Add(new Tuple<int, int, int>(m, n, currentSudokuDigit));


                        setCoverRowNumber++;
                    }
                    sudokuCellCounter++;
                }
            }
            coverBoard.extraCover = setCoverBoard;

            return coverBoard;
        }

        private List<int> ModifySetCoverBoard(int firstCoverColumn, int secondCoverColumn, int thirdCoverColumn, int fourthCoverColumn, int row, int cellNum)
        {
            var setCoverRowArray = new int[(cellNum * cellNum) * 4];

            setCoverRowArray[firstCoverColumn] = 1;
            setCoverRowArray[secondCoverColumn] = 1;
            setCoverRowArray[thirdCoverColumn] = 1;
            setCoverRowArray[fourthCoverColumn] = 1;

            var setCoverRow = setCoverRowArray.ToList();

            return setCoverRow;
        }

        private List<List<int>> SudokuValueToSetCover(List<List<int>> setCoverBoard, int sudokuDigit, int sudokuColumnNumber, int sudokuRowNumber, int sudokuCellCounter, int setCoverRow, int cellNum)
        {
            var sudokuBoxNumber = Math.Floor(sudokuRowNumber - (sudokuRowNumber % (Math.Sqrt(cellNum))) + (sudokuColumnNumber / (Math.Sqrt(cellNum))));
            var colWidth = cellNum * cellNum;

            // Take values and run them through SetCover Constraint equations to determine columns:
            var firstCoverColumn = SCColumnConstraints.FirstConstraintRule(sudokuRowNumber, sudokuColumnNumber, cellNum);
            var secondCoverColumn = SCColumnConstraints.SecondConstraintRule(sudokuRowNumber, sudokuDigit, cellNum, colWidth);
            var thirdCoverColumn = SCColumnConstraints.ThirdConstraintRule(sudokuColumnNumber, sudokuDigit, cellNum, colWidth);
            var fourthCoverColumn = SCColumnConstraints.FourConstraintRule(sudokuDigit, sudokuBoxNumber, setCoverRow, cellNum, colWidth);

            // Modify the EmptySetCoverBoard with the values determined above:
            setCoverBoard.Add(ModifySetCoverBoard(firstCoverColumn, secondCoverColumn, thirdCoverColumn, fourthCoverColumn, setCoverRow, cellNum));

            return setCoverBoard;
        }
    }
}
