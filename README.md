# DanceDanceSudokulution
An updated scalable Sudoku Solver built in c# using Dancing Links on an Extra cover problem to solve sudoku problems. 

Hey guys so a little while ago a few friends of mine gave me a challenge to write a Sudoku solver. We managed to all create one each that solved a 9x9 pretty easily by way of recursive algorithms. Which effectively just means for each blank cell the program will try a number, put it in if it fits the rules of Sudoku, then move onto the next cell. If it can not give it a number, it backtracks and tries the whole thing again for every possible solution. This works more than well enough for 9x9 however when you start to scale the sudoku puzzles to 16x16 or the big boi 25x25 you run into some real issues crunching those numbers. For scale there are 6,670,903,752,021,072,936,960 many possible sudoku grids on a 9x9 so imagine how many there are in a 25x25... 

So! To solve this here 25x25 we all took different paths to get there, for me I went down the road of converting a sudoku grid into a set cover problem, running a Dancing Links algorithm over this set cover matrix, then converting it back into a sudoku grid! For those who do not know, a set cover problem is when you have a set of elements and a collection of those elements in a subset, the idea is to then find the smallest collection of a subset whose union would equal the whole set.... okay I know that is confusing but stay with me: 

Imagine if we had (thanks to wikipedia for this example):
a universal set of:
{1 2 3 4 5}
then our sub set collections we had:
{1 2 3}, {2 4}, {3 4}, {4 5}
The idea here is to find the smallest collection of the above sub sets to get the full universal set, so the answer would be:
{1 2 3}, {4 5}
So at the end we have a big old grid of 1s and 0s, 324 columns long (81 columns for four constraints (cells, rows, columns boxes)) and a row for each possible placement in our sudoku grid. 

This means we get a huge grid of 0s with each row having exactly four 1s, each denoting a numbers placement. Each 1 is referring to a fulfilment of the four constraints of sudoku which are: 
1. Each cell must be filled from x-y (depending on the size of the grid, this could be 1-4, 1-9, 1-25 etc)
2. Each Row must be filled
3. Each Column must be filled 
4. Each Box must be filled

Now for a standard 9x9 sudoku grid this means we will have 81 columns per constraint (_9*9 in this case_) making a total of 324 columns. 
Each row within our set cover matrix is denoting a possible placement within the original sudoku grid, for instance if the first cell (we are using index from 0 because we are proper computer scientists here), row and column (0,0,0) had a 1 in it. You would have a row to denote that 1 and move on. If the next cell was blank however we would then need to make a row for every possible number that needs to go into that cell (so again for a 9x9 this would be a row for each number between 1-9).

After we have crafted a set cover matrix for the above it is time to move on to implmenting the magic of dancing links! For me I was a bit lazy and relied on https://github.com/taylorjg for his amazing implmentation of a DLXLIB (https://github.com/taylorjg/DlxLib). So we feed a List of Lists of integers (the set cover matrix if you are still following) into this great library.

We then run dancing links of the algroithm to get the solution to our set cover matrix! Thus we get a subset just like before (with the 12345 explanation), except this subset is our solution! 
Afterwards its just a matter of matching an indexer (remembering for us where our rows, columns and digits came from in the original sudoku grid) and translation the solution into another grid and taadaa! 


# References:
https://code.google.com/archive/p/narorumo/wikis/SudokuDLX.wiki
https://github.com/taylorjg/SudokuDlx
