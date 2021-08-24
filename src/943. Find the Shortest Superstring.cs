public class Solution {
    // DFS: TLE
    // ans is not correct
    public string ShortestSuperstring2(string[] words) {
        int n = words.Length;
        int[,] overlap = new int[n,n];
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < n; j++) {
                string A = words[i], B = words[j];
                 if (i == j) continue;
                int len = Math.Min(A.Length, B.Length);
                for (; len > 0; len--) {
                    if (A.Substring(A.Length - len) == B.Substring(0, len)) {
                        overlap[i, j] = len;
                        break;
                    }
                }
            }
        }
        int mn = Int32.MaxValue;
        string ans = "";
        int[] used = new int[n];
        Action<int, string> DFS = null;
        DFS = (start, cur) => {
            if (cur.Length >= mn) return;
            if (start == n) { mn = cur.Length; ans = cur; return; }
            for (int i = 0; i < n; i++) {
                if (used[i] == 1) continue;
                used[i] = 1;
                string tmp = cur;
                string nx = cur + (start == 0 || overlap[start - 1, i] == 0 ? words[i] : words[i].Substring(0, overlap[start - 1, i]));
                DFS(start + 1, nx);
            }
        };
        DFS(0,"");
        return ans;
    }
    // Travelling salesman problem (TSP) + Hamiltonian path
    // a shortest path of weighted graph
    // Time complexity: O(n^2 * 2^n)
    // Space complexity: O(n * 2^n)
    public string ShortestSuperstring(string[] words) {
        int n = words.Length;
        int[,] overlap = new int[n,n];
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < n; j++) {
                if (i == j) continue;
                string A = words[i], B = words[j];
                int len = Math.Min(A.Length, B.Length);
                for (; len > 0; len--) {
                    if (A.Substring(A.Length - len) == B.Substring(0, len)) {
                        overlap[i, j] = len;
                        break;
                    }
                }
            }
        }
        // dp[state, i]: overlapped substring
        // state: 1 << n, i: string node
        // each bit is the state of node 
        // 1: visited, 0: not visited
        string[,] dp = new string[1 << n, n];
        for (int i = 0; i < n; i++) dp[1 << i, i] = words[i];
        for (int mask = 0; mask < (1 << n); mask++) {
            // visit j from i
            for (int j = 0; j < n; j++) {
                // j is not in mask
                if ((mask & 1 << j) == 0) continue;
                for (int i = 0; i < n; i++) {
                    // i is not in mask, or j == i
                    if (i == j || (mask & 1 << i) == 0) continue;
                    // path to i without j appends none overlapped rest substring of j
                    var t = dp[mask ^ (1 << j), i] + words[j].Substring(overlap[i,j]);
                    if (dp[mask, j] == null || t.Length < dp[mask, j].Length) dp[mask, j] = t;
                }
            }
        }
        int finalState = (1 << n) - 1;
        string ans = dp[finalState, 0];
        for (int i = 1; i < n; i++) {
            if (dp[finalState, i].Length < ans.Length ) ans = dp[finalState, i];
        }
        return ans;
    }
}
