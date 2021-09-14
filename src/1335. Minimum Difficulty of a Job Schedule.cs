public class Solution {
    // recursion + memo
    public int MinDifficulty3(int[] jobDifficulty, int d) {
        int n = jobDifficulty.Length;
        if (d > n) return -1;
        int[,] memo = new int[n, d+1];
        for (int i = 0; i < n; i++)
            for (int j = 0; j <= d; j++) memo[i,j] = -1;
        Func<int,int,int> DFS = null;
        DFS = (start, k) => {
            int mn = Int32.MaxValue / 2, mx = 0;
            if (k == 1 && start < n) {
                int x = start;
                while (x < n) mx = Math.Max(mx, jobDifficulty[x++]); 
                return memo[start, k] = mx;
            }
            if (memo[start, k] != -1) return memo[start, k];
            // i < n - (k -1) :=> start, k will not be out of range
            for (int i = start; i < n - (k -1); i++) {
                mx = Math.Max(mx, jobDifficulty[i]);
                mn = Math.Min(mn, DFS(i + 1, k - 1) + mx);
            }
            return memo[start,k] = mn;
        };
        return DFS(0, d);
    }
    /**
    dp[i][k] := min difficulties to schedule jobs 1~i in k days.
    Schedule 1 ~ j in k – 1 days and schedule j + 1 ~ i in 1 day.
    Init: dp[0][0] = 0
    Transition: dp[i][k] := min(dp[j][k -1] + max(jobs[j + 1 ~ i]), k – 1 <= j < i
    */
    // DP
    // T: O(n^2*d) S: O(n)
    public int MinDifficulty(int[] jobDifficulty, int d) {
        int n = jobDifficulty.Length;
        if (d > n) return -1;
        int[,] dp = new int[n+1, d+1];
        for (int i = 0; i <= n; i++) 
            for (int j = 0; j <= d; j++) dp[i,j] = jobDifficulty.Sum() + 1;
        dp[0,0] = 0;
        for (int i = 1; i <= n; i++) {
            for (int k = 1; k <= d; k++) {
                // each day at least one job
                for (int j = i - 1, mx = 0; j >= k - 1; j--) {
                    mx = Math.Max(mx, jobDifficulty[j]);
                    dp[i,k] = Math.Min(dp[i,k], dp[j, k - 1] + mx);
                }
            }
        }
        return dp[n, d];
    }
    // DP v2
    // T: O(n^2*d) S: O(n)
    public int MinDifficulty2(int[] jobDifficulty, int d) {
        int n = jobDifficulty.Length;
        if (d > n) return -1;
        // dp := Difficulty at job i when day d
        int[] dp = new int[n+1];
        // d == 1 case
        for (int i = n - 1; i >= 0; i--) dp[i] = Math.Max(dp[i + 1], jobDifficulty[i]);
        for (int k = 2; k <= d; k++) {
            for (int i = 0; i < n - (k - 1); i++) {
                dp[i] = Int32.MaxValue;
                for (int j = i, mx = 0; j < n - (k -1); j++) {
                    mx = Math.Max(mx, jobDifficulty[j]);
                    // dp[j+1] := k - 1 days
                    dp[i] = Math.Min(dp[i], mx + dp[j+1]);
                }
            }
        }
        return dp[0];
    }
    // DP v3 DP + Monotonic stack => not friendly to understand
    // T: O(n*d) <= monotonic stack is O(1) S: O(n)
    public int MinDifficulty5(int[] jobDifficulty, int d) {
        int n = jobDifficulty.Length;
        if (d > n) return -1;
        // dp:= min of job i at day k
        int[] dp = new int[n];
        Array.Fill(dp, Int32.MaxValue / 2);
        // k init as 0 => easy to calculate i
        for (int k = 0; k < d; k++) {
            int[] t = new int[n];
            var st = new Stack<int>();
            for (int i = k; i < n; i++) {
                t[i] = (i > 0 ? dp[i-1] : 0) + jobDifficulty[i];
                while (st.Any() && jobDifficulty[st.Peek()] <= jobDifficulty[i]) {
                    int j = st.Pop();
                    t[i] = Math.Min(t[i], t[j] - jobDifficulty[j] + jobDifficulty[i]);
                }
                if (st.Any()) t[i] = Math.Min(t[i], t[st.Peek()]);
                st.Push(i);
            }
            dp = t;
        }
        return dp[n-1];
    }
}
