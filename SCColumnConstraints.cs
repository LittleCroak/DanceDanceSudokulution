using System;
using System.Collections.Generic;
using System.Text;

namespace DanceDanceSudokulution
{
    // d = digits, r = row, c = col, b = box.   
    class SCColumnConstraints
    {
        public static int FirstConstraintRule(int row, int col)
        {
            //r x 9 + c
            var firstContraintCol = row * 9 + col;
            return firstContraintCol;
        }
        public static int SecondConstraintRule(int row, int col, int d)
        {
            // r * 9 + d + 81
            var secondConstraintCol = 81 + row * 9 + d;

            return secondConstraintCol;
        }

        public static int ThirdConstraintRule(int col, int d)
        {
            // c * 9 + d + 81 + 81
            var thirdConstraintCol = 81 + 81 + col * 9 + d;

            return thirdConstraintCol;
        }

       public static int FourConstraintRule(int row, int col, int d, double box)
        {
            // b * 9 + d + 81 + 81 + 81
            var fourthConstraintCol = 81 + 81 + 81 + box * 9 + d;

            return (int)fourthConstraintCol;
        }
    }
}
