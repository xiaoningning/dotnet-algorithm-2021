public class Solution {
    // DFS v1 : recursion + memo
    // T: O(n^4) S: O(n^3)
    public int RemoveBoxes1(int[] boxes) {
        int n = boxes.Length;
        // memo[left,right, k] 
        // =: max # of [left..right] with k the same color boxes as box[right] after right
        int[,,] memo = new int[n,n,n];
        Func<int,int,int,int> DFS = null;
        DFS = (l,r,k) =>{
            if (l > r) return 0;
            if (memo[l,r,k] > 0) return memo[l,r,k];
            int ans = DFS(l,r-1,0) + (1+k)*(1+k);
            // (l,r,k) : (0,4,3) "ABACA|AAA" 
            // case 1: dp("ABAC") + score("AAAA") drop r and the tail.
            // case 2: box[i] == box[r], l <= i < r, try all break points
            // max({dp("A|AAAA") + dp("BAC")}, {dp("ABA|AAAA") + dp("C")})
            for (int i = l; i < r; i++) {
                if (boxes[i] == boxes[r])
                    ans = Math.Max(ans, DFS(l,i,k+1) + DFS(i+1,r-1,0));
            }
            return memo[l,r,k] = ans;
        };
        return DFS(0, n-1, 0);
    }
    // DP v2
    public int RemoveBoxes2(int[] boxes) {
        int n = boxes.Length;
        int[,,] memo = new int[n,n,n];
        Func<int,int,int,int> DFS = null;
        DFS = (l,r,k) =>{
            if (l > r) return 0;
            if (memo[l,r,k] > 0) return memo[l,r,k];
            int rr = r, kk = k;
            // optimization of r and k
            while (l < r && boxes[r-1] == boxes[r]) { --r; ++k; }
            int ans = DFS(l,r-1,0) + (1+k)*(1+k);
            for (int i = l; i < r; i++) {
                if (boxes[i] == boxes[r])
                    ans = Math.Max(ans, DFS(l,i,k+1) + DFS(i+1,r-1,0));
            }
            // update r .. rr
            for (int len = 1; len <= rr - r; len++) memo[l,r+len,k-len] = ans;
            return memo[l,r,k] = ans;
        };
        return DFS(0, n-1, 0);
    }
    // DP v3 similar to v2
    public int RemoveBoxes(int[] boxes) {
        int n = boxes.Length;
        int[] len = new int[n];
        // pre-calculate the len of boxes[i] left
        for (int i = 1; i < n; i++) if (boxes[i] == boxes[i-1]) len[i] = len[i-1] + 1;
        int[,,] memo = new int[n,n,n];
        Func<int,int,int,int> DFS = null;
        DFS = (l,r,k) =>{
            if (l > r) return 0;
            k += len[r];
            r -= len[r];
            if (memo[l,r,k] > 0) return memo[l,r,k];
            int ans = DFS(l,r-1,0) + (1+k)*(1+k);
            for (int i = l; i < r; i++)
                if (boxes[i] == boxes[r])
                    ans = Math.Max(ans, DFS(l,i,k+1) + DFS(i+1,r-1,0));
            return memo[l,r,k] = ans;
        };
        return DFS(0, n-1, 0);
    }
}
