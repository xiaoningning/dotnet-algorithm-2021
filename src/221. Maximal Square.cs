public class Solution {
    // DP on sum similar to LC 304. Range Sum Query 2D - Immutable
    // T: O(n^3)
    public int MaximalSquare(char[][] matrix) {
        if (matrix.Length == 0 || matrix[0].Length == 0) return 0;
        int m = matrix.Length, n = matrix[0].Length;
        // dp: sum at (i,j)
        int[,] dp = new int[m+1, n+1];
        for (int i = 1; i <= m; i++) for (int j = 1; j <= n; j++) dp[i,j] = matrix[i-1][j-1] - '0' + dp[i-1,j] + dp[i,j-1] - dp[i-1,j-1];
        int ans = 0;
        for (int i = 1; i <= m; i++) 
            for (int j = 1; j <= n; j++)
                for (int len = Math.Min(m-i+1, n-j+1); len > 0; len--) {
                    int sum = dp[i+len-1, j+len-1] - dp[i+len-1,j-1] - dp[i-1, j+len-1] + dp[i-1,j-1];
                    // all 1 in this area
                    if (sum == len*len) ans = Math.Max(ans, sum);
                }
        return ans;
    }
    // DP v1
    // T: (m*n) S:O(m*n)
    public int MaximalSquare3(char[][] matrix) {
        if (matrix.Length == 0 || matrix[0].Length == 0) return 0;
        int ans = 0, m = matrix.Length, n = matrix[0].Length;
        // len of square at (i,j) of bottom right square
        int[,] dp = new int[m, n];
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (i == 0 || j == 0) dp[i,j] = matrix[i][j] - '0';
                else if (matrix[i][j] == '1') dp[i,j] = new int[]{dp[i-1,j-1], dp[i,j-1], dp[i-1,j]}.Min() + 1;
                ans = Math.Max(ans, dp[i,j]);
            }
        }
        return ans* ans;
    }
    // DP v2 save space
    // T: (m*n) S:O(m)
    public int MaximalSquare4(char[][] matrix) {
        if (matrix.Length == 0 || matrix[0].Length == 0) return 0;
        int ans = 0, m = matrix.Length, n = matrix[0].Length, preJ = 0;
        // len of square at (i,j) of bottom right square
        int[] dp = new int[m+1];
        // must calculate col first to save space
        for (int j = 0; j < n; j++) {
            for (int i = 1; i <= m; ++i) {
                int t = dp[i];
                if (matrix[i-1][j] == '1') {
                    // dp[i] == dp[i,j-1], preJ == dp[i-1,j-1], dp[i-1] == dp[i-1,j]
                    dp[i] = new int[]{dp[i], preJ, dp[i-1]}.Min() + 1;
                    ans = Math.Max(ans, dp[i]);
                }
                else dp[i] = 0;
                preJ = t; 
            }
        }
        return ans* ans;
    }
    // similar to LC 85. Maximal Rectangle
    // DP + monotonic stack
    // T: (m*(n^2))
    public int MaximalSquare2(char[][] matrix) {
        if (matrix.Length == 0 || matrix[0].Length == 0) return 0;
        int ans = 0, m = matrix.Length, n = matrix[0].Length;
        int[] h = new int[n+1]; // = n for the last col
        for (int i = 0; i < m; i++) {
            var st = new Stack<int>(); // Monotonic stack
            // <= n for the last col
            for (int j = 0; j <= n; j++) {
                if (j < n) h[j] = matrix[i][j] == '1' ? h[j] + 1 : 0;
                while (st.Any() && h[st.Peek()] >= h[j]) {
                    int l = Math.Min(h[st.Pop()], st.Any() ? j - st.Peek() - 1 : j);
                    ans = Math.Max(ans, l*l); 
                }
                st.Push(j);
            }
        }
        return ans;
    }
    // T: O(m*n)
    public int MaximalSquare1(char[][] matrix) {
        if (matrix.Length == 0 || matrix[0].Length == 0) return 0;
        int ans = 0, m = matrix.Length, n = matrix[0].Length;
        int[] h = new int[n], left1Pos = new int[n], right1Pos = new int[n];
        Array.Fill(right1Pos, n);
        for (int i = 0; i < m; i++) {
            int curLeft = 0, curRight = n;
            for (int j = 0; j < n; j++) { 
                if (matrix[i][j] == '1') { 
                    h[j]++; 
                    left1Pos[j] = Math.Max(curLeft, left1Pos[j]); 
                }
                else { 
                    h[j] = 0; 
                    left1Pos[j] = 0; 
                    curLeft = j + 1; 
                }
            }
            for (int j = n - 1; j >= 0; j--) {
                if (matrix[i][j] == '1') right1Pos[j] = Math.Min(curRight, right1Pos[j]);
                else { 
                    right1Pos[j] = n; 
                    curRight = j; 
                }
                int l = Math.Min(h[j], (right1Pos[j] - left1Pos[j]));
                ans = Math.Max(ans, l*l);
            }
        }
        return ans;
    }
}
