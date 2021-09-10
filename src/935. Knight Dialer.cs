public class Solution {
    // DP v1
    public int KnightDialer1(int n) {
        int M = (int)Math.Pow(10,9) + 7;
        int[,] dirs = new int[8,2]{{-1,-2},{-2,-1},{-2,1},{-1,2},{1,2},{2,1},{2,-1},{1,-2}};
        int[,] dp = new int[4,3];
        for (int i = 0; i < 4; i++) 
                for (int j = 0; j < 3; j++) dp[i,j] = 1;
        dp[3,0] = dp[3,2] = 0;
        // n - 1 jumps
        for (int m = 1; m < n; m++) {
            int[,] t = new int[4,3];
            for (int i = 0; i < 4; i++) 
                for (int j = 0; j < 3; j++) {
                    if (i == 3 && j != 1) continue;
                    for (int d = 0; d < 8; d++) {
                        int x = i + dirs[d,0], y = j + dirs[d,1];
                        if (x < 0 || x >= 4 || y < 0 || y >= 3) continue;
                        t[i,j] = (t[i,j] + dp[x,y]) % M;
                    }
                }
            dp = t;
        }
        int ans = 0;
        foreach (int cnt in dp) ans = (ans + cnt) % M;
        return ans;
    }
    // DP v2 save space
    public int KnightDialer(int n) {
        int M = (int)Math.Pow(10,9) + 7;
        var moves = new Dictionary<int,int[]>(){
            [0] = new int []{4,6},
            [1] = new int []{8,6},
            [2] = new int []{7,9},
            [3] = new int []{4,8},
            [4] = new int []{3,9,0},
            [5] = new int []{},
            [6] = new int []{1,7,0},
            [7] = new int []{2,6},
            [8] = new int []{1,3},
            [9] = new int []{2,4}
        };
        int[] dp = new int[10];
        Array.Fill(dp,1);
        // n - 1 jumps
        for (int m = 1; m < n; m++) {
            int[] t = new int[10];
            for (int i = 0; i < 10; i++) 
                foreach (int nx in moves[i]) 
                    t[nx] = (t[nx] + dp[i]) % M;
            dp = t;
        }
        int ans = 0;
        foreach (int cnt in dp) ans = (ans + cnt) % M;
        return ans;
    }
}
