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

            var firstContraintCol = col;
            return firstContraintCol;
        }
        public static int SecondConstraintRule(int row, int col, int d)
        {
            
            var secondConstraintCol = 81  + (d);

            return secondConstraintCol;
        }

        public static int ThirdConstraintRule(int col, int d)
        {
            // c * 9 + d + 81 + 81
            var thirdConstraintCol = 81 + 81 + d;

            return thirdConstraintCol;
        }

       public static int FourConstraintRule(int row, int col, int d, double box)
        {
            // b * 9 + d + 81 + 81 + 81
            var fourthConstraintCol = 81 + 81 + 81 + (row - (row % 3)) + (col /3);

            return (int)fourthConstraintCol;
        }
    }
}
