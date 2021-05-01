using System;
using System.Collections.Generic;
using System.Linq;
using DlxLib;

namespace DanceDanceSudokulution
{
    class SolvedSetCoverToSudoku
    {
        public void setCoverToSudokuGrid(List<Solution> solvedSetCoverGrid, List<Tuple<int,int,int>> sudokuIndexer)
        {
            buildSudokuGridFromSetCover(solvedSetCoverGrid, sudokuIndexer);
        }

        private void buildSudokuGridFromSetCover(List<Solution> solvedSetCoverGrid, List<Tuple<int, int, int>> sudokuIndexer)
        {
            var test = solvedSetCoverGrid.First();

            var solvedSudRows = test.RowIndexes
                 .Select(index => sudokuIndexer[index])
                 .OrderBy(p => p.Item1)
                 .ThenBy(p => p.Item2)
                 .GroupBy(p => p.Item1, p => p.Item3)
                 .Select(resultVal => string.Concat(resultVal))
                 .ToList();

            Console.WriteLine("");
            solvedSudRows.ForEach(Console.WriteLine);
        }

    }
}
