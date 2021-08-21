public class Solution {
    // T: (m*n*4^(word.length))
    // S: (m*n + word.length)
    public bool Exist(char[][] board, string word) {
        if (board.Length == 0 || board[0].Length == 0) return false;
        int m = board.Length, n = board[0].Length;
        int[,] dirs = new int[,]{{1,0},{-1,0},{0,1},{0,-1}};
        
        Func<int, int, int, bool> DFS = null;
        DFS = (i, j, idx) =>{
            if (idx == word.Length) return true;
            if (i < 0 || i >= m || j < 0 || j >= n || board[i][j] != word[idx]) return false;
            char c = board[i][j];
            board[i][j] = '#';
            for (int d = 0; d < 4; d++) {
                int x = i + dirs[d,0], y = j + dirs[d,1];
                if (DFS(x, y, idx + 1)) return true;
            }
            board[i][j] = c;
            return false;
        };
        
        for (int i = 0; i < m; i++)
            for (int j = 0; j < n; j++)
                if (DFS(i, j, 0)) return true;
        return false;
    }
}
