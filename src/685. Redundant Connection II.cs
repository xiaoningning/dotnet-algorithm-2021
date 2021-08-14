public class Solution {
    // Union find
    // case1: 2 parents, but no cycle
    // case2: has cycle but no 2 parents
    // case3: has cycle and 2 parents
    // T: O(nlogn)
    public int[] FindRedundantDirectedConnection(int[][] edges) {
        int n = edges.Length;
        int[] first = new int[2], second = new int[2];
        int[] roots = new int[n + 1];
        // check if there are two parents case
        foreach (int[] e in edges) {
            if (roots[e[1]] == 0) roots[e[1]] = e[0];
            else {
                first = new int[]{roots[e[1]], e[1]};
                second = new int[]{e[0], e[1]};
                e[1] = -1; // remove this edge
            }
        }
        // UnionFind to find cycle
        for (int i = 1; i <= n; i++) roots[i] = i;
        foreach (int[] e in edges) {
            // edge was removed
            if (e[1] == -1) continue;
            int p0 = UnionFind(roots, e[0]);
            int p1 = UnionFind(roots, e[1]);
            // case2 or case 3 (cycle and 2 parents)
            // cycle edge must be last of redundant one
            if (p0 == p1) return first[0] == 0 ? e : first;
            roots[p1] = p0;
        }
        // case 1
        return second;
    }
    // T: O(logn)
    // Union Find recursion
    int UnionFind1(int[] roots, int x) {
        return roots[x] == x ? x : roots[x] = UnionFind(roots, roots[x]);
    }
    // Union Find while loop
    int UnionFind(int[] roots, int x) {
        while (roots[x] != x) x = roots[roots[x]];
        return x;
    }
}
