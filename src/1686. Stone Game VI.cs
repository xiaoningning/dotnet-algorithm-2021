public class Solution {
    // greedy search based on sum of alice + bob
    // T: O(nlogn)
    public int StoneGameVI(int[] aliceValues, int[] bobValues) {
        int n = aliceValues.Length;
        var sumIdx = new List<(int,int)>();
        for (int i = 0; i < n; i++) sumIdx.Add((aliceValues[i] + bobValues[i], i));
        sumIdx.Sort((x,y) => y.Item1 - x.Item1);
        int ans = 0;
        // alice => + value; bob => - value
        for (int i = 0; i < n; i++) { 
            int idx = sumIdx[i].Item2;
            ans += i % 2 == 0 ? aliceValues[idx] : -1 * bobValues[idx];
        }
        return ans < 0 ? -1 : Convert.ToInt32(ans > 0);
    }
}
