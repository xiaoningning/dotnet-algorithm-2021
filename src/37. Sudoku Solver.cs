public class Solution {
    // DFS + Recursion
    public void SolveSudoku1(char[][] board) {
        Func<int, int, char, bool> isValid = null;
        isValid = (i, j, c) => {
            for (int k = 0; k < 9; k++) {
                if (board[i][k] != '.' && board[i][k] == c) return false;
                if (board[k][j] != '.' && board[k][j] == c) return false;
                // 3x3 sub box
                int row = i / 3 * 3 + k / 3, col = j / 3 * 3 + k % 3;
                if (board[row][col] != '.' && board[row][col] == c) return false;
            }
            return true;
        };
        Func<bool> F = null;
        F = () => {
            for (int i = 0; i < 9; i++) {
                for (int j = 0; j < 9; j++) {
                    if (board[i][j] != '.') continue;
                    for (char c = '1'; c <= '9'; c++) {
                        if (!isValid(i,j,c)) continue;
                        board[i][j] = c;
                        if (F()) return true;
                        board[i][j] = '.';
                    }
                    // (i,j) [1..9] not valid
                    return false;
                }
            }
            // all filled case
            return true;
        };
        F();
    }
    // F is faster since it does not 0..9 every time
    public void SolveSudoku(char[][] board) {
        Func<int, int, char, bool> isValid = null;
        isValid = (i, j, c) => {
            for (int k = 0; k < 9; k++) {
                if (board[i][k] == c || board[k][j] == c) return false;
            }
            // 3x3 sub box
            int row = i - i % 3, col = j - j % 3;
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                    if (board[row + x][col + y] == c) return false;
            return true;
        };
        Func<int, int, bool> F = null;
        F = (i, j) => {
            // do j (col) first and then i (row)
            // if i == 9, then done
            if (i == 9) return true;
            // next i and j 0
            // move to next row from col 0
            if (j == 9) return F(i + 1, 0);
            if (board[i][j] != '.') return F(i, j + 1);
            // case: board[i][j] == '.'
            for (char c = '1'; c <= '9'; c++) {
                if (!isValid(i,j,c)) continue;
                board[i][j] = c;
                if (F(i,j + 1)) return true;
                board[i][j] = '.';
            }
            // (i,j) [1..9] not valid
            return false;
        };
        F(0,0);
    }
}
