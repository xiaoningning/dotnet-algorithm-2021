public class Solution {
    public IList<IList<int>> GetSkyline(int[][] buildings) {
        var bhLst = new List<(int, int)>();
        foreach (int[] b in buildings) {
            bhLst.Add((b[0], b[2]));
            bhLst.Add((b[1], -b[2])); // mark as end
        }
        bhLst.Sort((x, y) => {
            // case: two buildings side to side on the same x
            // if end-xi == start-xj, xj should be ahead of xi
            if (x.Item1 == y.Item1) return y.Item2 - x.Item2;
            else return x.Item1 - y.Item1;
            });
        int prev = 0;
        var h = new List<int>(){prev};
        var ans = new List<IList<int>>();
        foreach (var bh in bhLst) {
            if (bh.Item2 > 0) InsertList(h, bh.Item2);
            else h.Remove(-bh.Item2); // -Item2 is end
            int cur = h.Last();
            if (cur != prev) {
                ans.Add(new int[]{bh.Item1, cur});
                prev = cur;
            }
        }
        return ans;
    }
    // List sort is too slow for LeetCode
    // Binary search for insert
    public void InsertList(List<int> lst, int x) {
        int l = 0, r = lst.Count;
        while (l < r) {
            int m = l + (r - l) / 2;
            if (lst[m] < x) l = m + 1;
            else r = m;
        }
        lst.Insert(r, x);
    }
}
