using System;

namespace DanceDanceSudokulution
{
    class Program
    {
        static void Main(string[] args)
        {
            //Import Sudoku board from CSV
            Console.WriteLine("Please input file path of sudoku CSV: ");
            var sudokuFilePath = Console.ReadLine();

            SudokuImport sudokuImport = new SudokuImport();
            var sudokuBoard = sudokuImport.CsvImporter(sudokuFilePath);

            // convert Sudoku board into DLX Problem
            SudokuToDLX sudokuToDLX = new SudokuToDLX();
            var setCoverBoard = sudokuToDLX.CreateDLXGrid(sudokuBoard);

            // Utilize DLX to solve the setcover Matrix
            DLXOnSetCover dlxOnSetCover = new DLXOnSetCover();
            var solvedSetCoverGrid = dlxOnSetCover.CompletedSudoku(setCoverBoard.extraCover);

            // now we have the solved set cover grid, convert it into a sudoku grid
            SolvedSetCoverToSudoku solvedSetCoverToSudoku = new SolvedSetCoverToSudoku();
            solvedSetCoverToSudoku.setCoverToSudokuGrid(solvedSetCoverGrid, setCoverBoard.indexer);
        }
    }
}
