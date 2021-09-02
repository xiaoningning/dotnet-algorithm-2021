public class Solution {
    // best test case "RRWWRRBBRR", "WB" 
    // =: RRWWRRBBRR -> RRWWRRBBR[W]R -> RRWWRRBB[B]RWR -> RRWWRRRWR -> RRWWWR -> RRR -> empty
    // the order of insert will impact the result
    // DFS + memo
    public int FindMinStep(string board, string hand) {
        Func<string,string> f = null;
        f = (str) => {
            // i <= str.Length => remove last char
            for (int i = 0, j = 0; i <= str.Length; i++) {
                if (i < str.Length && str[i] == str[j]) continue;
                if (i - j >= 3) return f(str.Substring(0, j) + str.Substring(i));
                else j = i;
            }
            return str;
        };
        int[] cnt = new int[128];
        foreach (char c in hand) cnt[c]++;
        var memo = new Dictionary<string, int>();
        Func<string, int> DFS = null;
        DFS = (b) => {
            if (memo.ContainsKey(b)) return memo[b];
            if (b.Length == 0) return 0;
            int ans = Int32.MaxValue;
            for (int i = 0; i < b.Length; i++) {
                for (int j = 0; j < 128; j++) {
                    if (cnt[j] == 0) continue;
                    cnt[j]--;
                    // search all possible str
                    string nb = f(b.Insert(i,((char)j).ToString()));
                    int mx = DFS(nb);
                    if (mx != -1) ans = Math.Min(ans, mx + 1);
                    cnt[j]++;
                }
            }
            if (ans == Int32.MaxValue) ans = -1;
            return memo[b] = ans;
        };
        return DFS(board);
    }
    // recursion: brute force search => TLE
    public int FindMinStep1(string board, string hand) {
        Func<string,string> f = null;
        f = (str) => {
            // i <= str.Length => remove last char
            for (int i = 0, j = 0; i <= str.Length; i++) {
                if (i < str.Length && str[i] == str[j]) continue;
                if (i - j >= 3) return f(str.Substring(0, j) + str.Substring(i));
                else j = i;
            }
            return str;
        };
        int ans = Int32.MaxValue;
        for (int i = 0; i < hand.Length; i++) {
            for (int j = 0; j < board.Length; j++) {
                // failed on the best test case due to the order of insertion
                // if (hand[i] != board[j]) continue;
                int len = 0;
                while (j < board.Length && hand[i] == board[j++] ) len++;
                string nxBoard = f(board.Insert(j - 1, hand[i].ToString()));
                // base case
                if (nxBoard.Length == 0) return 1;
                string nxHand = hand.Remove(i, 1);
                int mx = FindMinStep(nxBoard, nxHand);
                if (mx != -1) ans = Math.Min(ans, mx + 1);
            }
        }
        return ans == Int32.MaxValue ? -1 : ans;
    }
}
