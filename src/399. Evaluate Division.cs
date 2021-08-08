public class Solution {
    // Graph + DFS
    Dictionary<string, Dictionary<string, double>> m = new Dictionary<string, Dictionary<string, double>>();
    public double[] CalcEquation1(IList<IList<string>> equations, double[] values, IList<IList<string>> queries) {
        var ans = new List<double>();
        if (equations.Count == 0) return ans.ToArray();
        for (int i = 0; i < equations.Count; i++) {
            var e = equations[i];
            if (!m.ContainsKey(e[0])) m[e[0]] = new Dictionary<string, double>();
            if (!m.ContainsKey(e[1])) m[e[1]] = new Dictionary<string, double>();
            m[e[0]].Add(e[1], values[i]);
            m[e[1]].Add(e[0],  1 / values[i]);
        }
        foreach (var q in queries) {
            var visited = new HashSet<string>();
            ans.Add(DivideDFS(q[0], q[1], visited));
        }
        // T: O(e + query*e) S: O(e)
        return ans.ToArray();
    }
    double DivideDFS(string up, string down, HashSet<string> visited) {
        if (!m.ContainsKey(up)) return -1;
        if (m[up].ContainsKey(down)) return m[up][down];
        visited.Add(up);
        foreach (var u in m[up].Keys) {
            if (visited.Contains(u)) continue;
            visited.Add(u);
            var v = DivideDFS(u, down, visited);
            if (v > 0) return m[up][u] * v;
        }
        return -1;
    }
    // Union Find
    Dictionary<string, string> parentUnion = new Dictionary<string, string>();
    Dictionary<string, double> v = new Dictionary<string, double>();   
    public double[] CalcEquation(IList<IList<string>> equations, double[] values, IList<IList<string>> queries) {
        var ans = new List<double>();
        if (equations.Count == 0) return ans.ToArray();
        for (int i = 0; i < equations.Count; i++) {
            var e = equations[i];
            string a = e[0], b = e[1];
            Union(a, b, values[i]);
        }
        foreach (var q in queries) {
            string x = q[0], y = q[1];
            if (parentUnion.ContainsKey(x) 
                && parentUnion.ContainsKey(y)
                && UnionFind(x) == UnionFind(y)) {
                ans.Add(v[x] / v[y]);
            }
            else ans.Add(-1);
        }
        // T: O(e + query) S: O(e)
        return ans.ToArray();
    }
    void Union(string a, string b, double val) {
        string pa = UnionFind(a), pb = UnionFind(b);
        parentUnion[pa] = pb;
        // a/b, c/e => b/e = (a/c)*(c/e)/(a/b)
        v[pa] = val * v[b] / v[a];
    }
    string UnionFind(string x) {
        if (!parentUnion.ContainsKey(x)) {
            parentUnion[x] = x;
            v[x] = 1.0;
            return x;
        }
        else {
            string px = parentUnion[x];
            if (x != px) {
                parentUnion[x] = UnionFind(px);
                v[x] = v[x]* v[px];
            }
            return parentUnion[x];
        }
    }
}
