public class Solution {
    public int TotalNQueens(int n) {
        int ans = 0;
        // check if valid to put Q at (row, col)
        Func<List<string>, int, int, bool> isValid = (q, row, col) => {
            for (int i = 0; i < row; i++) if (q[i][col] == 'Q') return false;
            for (int i = row - 1, j = col - 1; i >= 0 && j >= 0; i--, j--) if (q[i][j] == 'Q') return false;
            for (int i = row - 1, j = col + 1; i >= 0 && j < n; i--, j++) if (q[i][j] == 'Q') return false;
            return true;
        };
        Action<int, List<string>> DFS = null;
        DFS = (row, queens) => {
            if (row == n) { ans++; return; }
            for (int i = 0; i < n; i++) {
                if (isValid(queens, row, i)) {
                    char[] t = queens[row].ToCharArray();
                    t[i] = 'Q';
                    queens[row] = new string(t);
                    DFS(row + 1, queens);
                    t[i] = '.';
                    queens[row] = new string(t);
                }
            }
        };
        // init as all '.'
        var queens = new List<string>();
        int cnt = n;
        char[] t = new char[n];
        Array.Fill(t, '.');
        string str = new string(t);
        while (--cnt >= 0) queens.Add(str);
        
        DFS(0, queens);
        return ans; 
    }
}
