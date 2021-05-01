using System.Collections.Generic;
using System.Linq;
using DlxLib;

namespace DanceDanceSudokulution
{
    class DLXOnSetCover
    {
        public List<Solution> CompletedSudoku(List<List<int>> setCoverBoard)
        {
            return SolvedSetCoverBoard(setCoverBoard);
        }

        private List<Solution> SolvedSetCoverBoard(List<List<int>> setCoverBoard)
        {
            var dlx = new Dlx();

            var solvedSetCoverBoard = new Dlx().Solve(setCoverBoard, d => d, r => r).Take(1).ToList();

            return solvedSetCoverBoard;
        }
    }
}
