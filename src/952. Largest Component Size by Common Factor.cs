public class Solution {
    // Dijkstra set: Union Find
    public int LargestComponentSize(int[] nums) {
        // potential common factor
        int mx = nums.Max();
        // common factor > 1
        var dsu = new DSUnion(mx + 1);
        foreach (int n in nums) {
            int sqrt = (int)Math.Sqrt(n);
            for (int f = 2; f <= sqrt; f++) {
                if (n % f != 0) continue;
                // union with all factors
                // union(6,2), union(6,3)
                dsu.Union(n, f);
                dsu.Union(n, n / f);
            }
        }
        int ans = 0;
        var cnt = new int[mx + 1];
        foreach (int n in nums) {
            ans = Math.Max(ans, ++cnt[dsu.UnionFind(n)]);
        }
        // T: O(n*sqrt(nums[i]))
        return ans;
    }
    // Dijkstra set data structure
    public class DSUnion {
        public int[] roots;
        public DSUnion(int n) {
            roots = new int[n];
            for (int i = 0; i < n; i++) roots[i] = i;
        }
        public void Union(int x, int y) {
            int px = UnionFind(x);
            int py = UnionFind(y);
            if (px != py) roots[px] = py;
        }
        public int UnionFind(int x) {
            return roots[x] == x ? x : roots[x] = UnionFind(roots[x]);
        }
    }
}
