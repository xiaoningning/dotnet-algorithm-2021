public class Solution {
    public IList<int> FallingSquares(int[][] positions) {
        int n = positions.Length;
        var ans = new List<int>();
        var lst = new List<int[]>();
        var mxLen = 0;
        foreach (var p in positions) {
            int l = p[0], len = p[1], r = l + len, prevLen = 0;
            foreach (var i in lst) {
                if (l >= i[1] || r <= i[0]) continue;
                prevLen = Math.Max(prevLen, i[2]);
            }
            len += prevLen;
            lst.Add(new int[]{l,r,len});
            mxLen = Math.Max(mxLen, len);
            ans.Add(mxLen);
        }
        // O(n^2)
        return ans;
    }
}
