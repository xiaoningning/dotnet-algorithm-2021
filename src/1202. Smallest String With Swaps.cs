public class Solution {
    // DFS
    Dictionary<int, List<int>> g = new Dictionary<int, List<int>>();
    int[] visited;
    public string SmallestStringWithSwaps(string s, IList<IList<int>> pairs) {
        int n = s.Length;
        visited = new int[n];
        for (int i = 0; i < n; i++) g[i] = new List<int>();
        foreach (var p in pairs) {
            g[p[0]].Add(p[1]);
            g[p[1]].Add(p[0]);
        }
        char[] ans = new char[n];
        for (int i = 0; i < n; i++) {
            if (visited[i] == 1) continue;
            var idx = new List<int>();
            var str = new List<char>();
            DFS(s, i, idx, str);
            str.Sort(); idx.Sort();
            for (int k = 0; k < idx.Count; k++) ans[idx[k]] = str[k];
        }
        // T: O(nlogn + k*(V+E))
        return new string(ans);
    }
    void DFS(string s, int i, List<int> idx, List<char> str) {
        if (visited[i] == 1) return;
        visited[i] = 1;
        str.Add(s[i]);
        idx.Add(i);
        foreach (int j in g[i]) DFS(s, j, idx, str); 
    }
    // Union Find
    int[] roots;
    public string SmallestStringWithSwaps1(string s, IList<IList<int>> pairs) {
        int n = s.Length;
        roots = new int[n];
        for (int i = 0; i < n; i++) roots[i] = i;
        foreach (var p in pairs) {
            int rx = UnionFind(roots, p[0]);
            int ry = UnionFind(roots, p[1]);
            if(rx != ry) roots[ry] = rx;
        }
        var idx = new Dictionary<int, List<int>>();
        var str = new Dictionary<int, List<char>>();
        for (int i = 0; i < n; i++) {
            int ri = UnionFind(roots,i);
            if (!idx.ContainsKey(ri)) idx[ri] = new List<int>();
            if (!str.ContainsKey(ri)) str[ri] = new List<char>();
            idx[ri].Add(i);// idx is already sorted
            str[ri].Add(s[i]);
        }
        char[] ans = new char[n];
        int[] visited = new int[n];
        for (int i = 0; i < n; i++) {
            if (visited[i] == 1) continue;
            int ri = UnionFind(roots, i);
            str[ri].Sort();
            for (int k = 0; k < idx[ri].Count; k++) {
                ans[idx[ri][k]] = str[ri][k];
                visited[idx[ri][k]] = 1;
            }
        }
        // T: O(nlogn + V+E)
        return new string(ans);
    }
    int UnionFind(int[] roots, int x) {
        return roots[x] == x ? x : roots[x] = UnionFind(roots, roots[x]);
    }
}
