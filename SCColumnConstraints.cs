namespace DanceDanceSudokulution
{
    // d = digits, r = row, c = col, b = box.   
    class SCColumnConstraints
    {
        public static int FirstConstraintRule(int row, int col, int cellNum)
        {
            // (sRow * 9) + sCol
            var firstContraintCol = (row * cellNum) + col;
            return firstContraintCol;
        }
        public static int SecondConstraintRule(int row, int d, int cellNum, int colWidth)
        {
            // (sRow * 9) + d + 81 + 81
            var secondConstraintCol = (row * cellNum) + d + colWidth - 1;
            return secondConstraintCol;
        }

        public static int ThirdConstraintRule(int col, int d, int cellNum, int colWidth)
        {
            // (sCol * 9) + d + 81 + 81 - 1
            var thirdConstraintCol = (col * cellNum) + d + colWidth + colWidth - 1;
            return thirdConstraintCol;
        }

       public static int FourConstraintRule(int d, double box, int setCoverRow, int cellNum, int colWidth)
        {
            // (sBox * 9) + d + 81 + 81 + 81 - 1
            var fourthConstraintCol = (box * cellNum) + d + colWidth + colWidth + colWidth - 1;
            return (int)fourthConstraintCol;
        }
    }
}
