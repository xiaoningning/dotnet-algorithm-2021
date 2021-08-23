public class Solution {
    // DFS
    public IList<IList<int>> CombinationSum21(int[] candidates, int target) {
        var ans = new List<IList<int>>();
        Array.Sort(candidates);
        Action<int, List<int>> DFS = null;
        DFS = (start, tmp) => {
            if (tmp.Sum() == target) { ans.Add(new List<int>(tmp)); return;}
            for (int i = start; i < candidates.Length; i++) {
                // each number in candidates used only once in ans
                // no duplicated in ans set
                if (i > start && candidates[i] == candidates[i - 1]) continue;
                int n = candidates[i];
                if (tmp.Sum() + n > target) break;
                tmp.Add(n);
                DFS(i+1, tmp);
                tmp.Remove(n);
            }
        };
        DFS(0, new List<int>());
        return ans;
    }
    // recursion
    public IList<IList<int>> CombinationSum2(int[] candidates, int target) {
        var ans = new HashSet<IList<int>>();
        Array.Sort(candidates);
        for (int i = 0; i < candidates.Length; i++) {
            // each number in candidates used only once in ans
            if (i > 0 && candidates[i] == candidates[i - 1]) continue;
            int n = candidates[i];
            if (n > target) break; 
            if (n == target) { ans.Add(new List<int>(){n}); break;}
            int[] tmp = new int[candidates.Length - i - 1];
            Array.Copy(candidates, i + 1, tmp, 0,  tmp.Length);
            var res = CombinationSum2(tmp, target - n);
            foreach (var a in res) { a.Add(n); ans.Add(a); }
        }
        return ans.ToList();
    }
}
