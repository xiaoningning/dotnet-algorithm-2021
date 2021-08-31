public class Solution {
    // DP + monotonic stack
    // T: (m*(n^2))
    public int MaximalRectangle2(char[][] matrix) {
        if (matrix.Length == 0 || matrix[0].Length == 0) return 0;
        int ans = 0, m = matrix.Length, n = matrix[0].Length;
        int[] height = new int[n+1];
        for (int i = 0; i < m; i++) {
            // Monotonic stack
            var st = new Stack<int>();
            // == n is the last col
            for (int j = 0; j <= n; j++) {
                if (j < n) height[j] = matrix[i][j] == '1' ? height[j] + 1 : 0;
                while (st.Any() && height[st.Peek()] >= height[j]) {
                    int cur = st.Pop();
                    ans = Math.Max(ans, height[cur] * (st.Any() ? j - st.Peek() - 1 : j));
                }
                st.Push(j);
            }
        }
        return ans;
    }
    // DP T: O(n*(m^2))
    public int MaximalRectangle1(char[][] matrix) {
        if (matrix.Length == 0 || matrix[0].Length == 0) return 0;
        int ans = 0, m = matrix.Length, n = matrix[0].Length;
        int[,] len = new int[m,n];
        for (int i = 0; i < m; i++) 
            for (int j = 0; j < n; j++) 
                if (matrix[i][j] == '1') len[i,j] = 1 + (j == 0 ? 0 : len[i,j - 1]);
        
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (matrix[i][j] == '0') continue;
                int mn = len[i,j];
                // only one row case
                ans = Math.Max(mn, ans);
                for (int k = i - 1; k >= 0 && len[k,j] != 0; k--) {
                    mn = Math.Min(mn, len[k,j]);
                    ans = Math.Max(ans, mn * (i - k + 1));
                }
            }
        }
        return ans;
    }
    // T: O(m*n)
    public int MaximalRectangle(char[][] matrix) {
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
                
                ans = Math.Max(ans, h[j] * (right1Pos[j] - left1Pos[j]));
            }
        }
        return ans;
    }
}
