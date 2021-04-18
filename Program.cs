using System;

namespace DanceDanceSudokulution
{
    class Program
    {
        static void Main(string[] args)
        {
            //Import Sudoku board from CSV
            var sudokuFilePath = Console.ReadLine();
            var sudokuBoard = SudokuImport.CsvImporter(sudokuFilePath);

            // convert Sudoku board into DLX Problem
            var setCoverGrid = SudokuToDLX.CreateDLXGrid(sudokuBoard);

            DLXOnSetCover.CompletedSudoku(setCoverGrid);

            // test code to write to csv 
            PrintArray.Print2DArrayToCsv(setCoverGrid, setCoverGrid);
        }
    }
}
