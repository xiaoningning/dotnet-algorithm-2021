public class Solution {
    // DFS 
    // T: C(m, k) = C(9, k) = O(9!/k!/(9-k)!)
    // S: O(k + k * ans.Count)
    public IList<IList<int>> CombinationSum31(int k, int n) {
        var ans = new List<IList<int>>();
        Action<int, List<int>> DFS = null;
        DFS = (start, tmp) => {
            if (tmp.Count == k && tmp.Sum() == n) {ans.Add(new List<int>(tmp)); return;}
            for (int i = start; i <= 9; i++) {
                if (tmp.Contains(i)) continue;
                if (tmp.Sum() + i > n) break;
                tmp.Add(i);
                DFS(i + 1, tmp);
                tmp.Remove(i);
            }
        };
        DFS(1, new List<int>());
        return ans;
    }
    // bit mask operation
    // T: O(2^m) = O(2^9)
    // S: O(k + k * ans.Count)
    public IList<IList<int>> CombinationSum3(int k, int n) {
        var ans = new List<IList<int>>();
        // 2^9, generate all combinations of [1 .. 9]
        for (int s = 0; s < 1 << 9; s++) {
            var tmp = new List<int>();
            for (int i = 1; i <= 9; i++) {
                // Console.WriteLine($"{Convert.ToString(s, 2)} {Convert.ToString(1 << i - 1, 2)}");
                if ((s & (1 << (i - 1))) > 0) tmp.Add(i);
            }
            if (tmp.Count == k && tmp.Sum() == n) ans.Add(new List<int>(tmp));
        }
        return ans;
    }
}
