public class Solution {
    // Disjoint Set : Union Find rank + path compression
    // rank + path compression => Union Find T: O(logE)
    // T: O(E^2 * logE)
    Dictionary<string, string> roots = new Dictionary<string, string>();
    Dictionary<string, int> rank = new Dictionary<string, int>();
    int ans = 0;
    public int NumSimilarGroups(string[] strs) {
        // s can be duplicated
        strs = new HashSet<string>(strs).ToArray();
        int n = strs.Length;
        ans = n;
        foreach (string s in strs)  {
            roots[s] = s;
            rank[s] = 0;
        }
        foreach (string a in strs) {
            foreach (string b in strs) {
                if (IsSimilar(a,b)) Union(a, b);
            }
        }
        return ans;
    }
    void Union(string x, string y) {
        string px = UnionFind(roots, x);
        string py = UnionFind(roots, y);
        if (px == py) return;
        // Union by rank: attaches the shorter tree to the root of the taller tree.
        if (rank[px] < rank[py]) roots[px] = py;
        else if (rank[py] < rank[px]) roots[py] = px;
        else {
            rank[py]++;
            roots[py] = px;
        }
        ans--;
    }
    string UnionFind(Dictionary<string, string> roots, string x) {
        return roots[x] == x ? x : roots[x] = UnionFind(roots, roots[x]);
    }
    bool IsSimilar(string x, string y) {
        if (x.Length != y.Length) return false;
        // the same str is similar as well
        if (x == y) return true;
        int cnt = 0;
        for (int i = 0; i < x.Length; i++) if (x[i] != y[i]) cnt++;
        return cnt == 2
            && new string (x.OrderBy(c => c).ToArray()) == new string (y.OrderBy(c => c).ToArray());
    }
}
