public class Solution {
    // DP v1
    public int MinimumTotal1(IList<IList<int>> triangle) {
        for (int i = 1; i < triangle.Count; i++) {
            for (int j = 0; j < triangle[i].Count; j++) {
                if (j == 0) triangle[i][j] += triangle[i-1][0];
                else if (j == triangle[i].Count - 1) triangle[i][j] += triangle[i-1][j-1];
                else triangle[i][j] += Math.Min(triangle[i-1][j-1], triangle[i-1][j]);
            }
        }
        return triangle.Last().Min();
    }
    // DP v2 cleaner
    public int MinimumTotal(IList<IList<int>> triangle) {
        var dp = new List<int>(triangle.Last());
        for (int i = triangle.Count - 2; i >= 0; i--)
            for (int j = 0; j < triangle[i].Count; j++)
                dp[j] = Math.Min(dp[j], dp[j+1]) + triangle[i][j];
        return dp[0];
    }
}
