public class Solution {
    // DijKstra set: Union Find
    int[] roots;
    public bool EquationsPossible(string[] equations) {
        // char to int
        roots = new int[256];
        for (int i = 0; i < 256; i++) roots[i] = i;
        foreach (var e in equations) {
            if (e[1] == '=') roots[UnionFind(e[0])] = roots[UnionFind(e[3])];
        }
        foreach (var e in equations) {
            if (e[1] == '!' && UnionFind(e[0]) == UnionFind(e[3])) return false;
        }
        return true;
    }
    int UnionFind(int x) {
        return roots[x] == x ? x : roots[x] = UnionFind(roots[x]);
    }
}
