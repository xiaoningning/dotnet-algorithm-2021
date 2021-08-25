public class Solution {
    // BFS
    // T: O(2^D) D: steps
    // S: O(2^D)
    public int Racecar1(int target) {
        int steps = 0;
        //(position, speed)
        var q = new Queue<(int, int)>();
        q.Enqueue((0,1));
        var visited = new HashSet<(int, int)>();
        visited.Add((0,1));
        while (q.Any()) {
            int size = q.Count;
            while (--size >= 0) {
                var t = q.Dequeue();
                int pos = t.Item1, speed = t.Item2;
                if (pos == target) return steps;
                int npos = pos + speed, nspeed = speed * 2;
                // can not go too far, but by pass target then reverse
                if (!visited.Contains((npos, nspeed)) && npos > 0 && npos < target * 2) {
                    q.Enqueue((npos,nspeed));
                    visited.Add((npos,nspeed));
                }
                npos = pos; nspeed = speed > 0 ? -1 : 1;
                // can not go back to far
                if (!visited.Contains((npos, nspeed)) && npos >= target / 2) {
                    q.Enqueue((npos,nspeed));
                    visited.Add((npos,nspeed));
                }
            }
            steps++;
        }
        return -1;
    }
    // DP
    // T: O(TlogT), T=> target 
    // S: O(T+ logT)
    public int Racecar2(int target) {
        // dp: # of instructions to i 
        // case 1: Ai
        // case 2: Aj,R,Ak,R,i
        // case 3: Aj,R,i
        int[] dp = new int[target + 1];
        for (int i = 1; i <= target; i++) {
            dp[i] = Int32.MaxValue;
            int cntA_j = 1, j = 1;
            // 0..k...j....i: 0..Aj.->..j => 0...k..Rj => 0.Aj.->..k..<-.Ak..Rj..->..i
            // => 0..Aj.->..k.R...<-.Ak..Rj..i
            // nextpos = pos + speed * 2 
            // target = 0 -> 1 -> 3 -> 7 -> 15 -> 31
            // => target = 2^n - 1
            for (; j < i; j = (1 << ++cntA_j) - 1) {
                for (int k = 0, cntA_k = 0; k < j; k = (1 << ++cntA_k) - 1)
                    // Aj,R,Ak,R,dp[i - (j -k)]
                    dp[i] = Math.Min(dp[i], cntA_j + 1 + 1 + cntA_k + dp[i - (j - k)]);
            }
            // 0...i...j: 0..Aj..->..i..->..j => 0..Aj.->..i..<-...Rj
            // Aj,R,dp[j-i]
            dp[i] = Math.Min(dp[i], cntA_j + (j == i ? 0 : 1 + dp[j - i]));
        }
        return dp[target];
    }
    // DP
    // T: O(TlogT), T=> target 
    // S: O(T+ logT)
    public int Racecar(int target) {
        // 1 <= target <= 10^4
        int[] memo = new int[target + 1];
        Func<int,int> DP = null;
        DP = (t) => {
            if(memo[t] > 0) return memo[t];
            // nextpos = pos + speed * 2 
            // target = 0 -> 1 -> 3 -> 7 -> 15 -> 31
            // => target = 2^n - 1
            int n = (int)Math.Ceiling(Math.Log(t + 1) / Math.Log(2));
            // AA...A: (nA) == t best case
            if (t == (1 << n) - 1) return memo[t] = n;
            // AA...AR: (nA + 1R) + dp(left) 
            memo[t] = n + 1 + DP((1 << n) - 1 - t);  
            // nA > t case: m < n - 1
            for (int m = 0; m < n - 1; ++m) {
                int cur = (1 << (n - 1)) - (1 << m);
                //AA...ARA...AR: (n-1A + 1R + mA + 1R) + dp(left) 
                memo[t] = Math.Min(memo[t], (n - 1) + 1 + m + 1 + DP(t - cur)); 
            }
            return memo[t];  
        };
        return DP(target);
    }
}
