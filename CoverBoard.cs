using System;
using System.Collections.Generic;
using System.Text;

namespace DanceDanceSudokulution
{
    class CoverBoard
    {
        public List<List<int>> extraCover { get; set; }
        public List<Tuple<int, int, int>> indexer { get; set; }
    }
}
