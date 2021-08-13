public class Solution {
    Dictionary<string, string> roots = new Dictionary<string,string>();
    public bool AreSentencesSimilarTwo(string[] sentence1, string[] sentence2, IList<IList<string>> similarPairs) {
        if (sentence1.Length != sentence2.Length) return false;
        foreach (var p in similarPairs) {
            string px = UnionFind(p[0]);
            string py = UnionFind(p[1]);
            if (px != py) roots[px] = py;
        }
        int n = sentence1.Length;
        for (int i = 0; i < n; i++) {
            string p1 = sentence1[i];
            string p2 = sentence2[i];
            if (UnionFind(p1) != UnionFind(p2)) return false;
        }
        return true;
    }
    string UnionFind(string x) {
        if (!roots.ContainsKey(x)) return roots[x] = x;
        else return roots[x] == x ? x : roots[x] = UnionFind(roots[x]);
    }
}
