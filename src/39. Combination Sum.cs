public class Solution {
    // DFS
    public IList<IList<int>> CombinationSum1(int[] candidates, int target) {
        var ans = new List<IList<int>>();
        Action<int, List<int>> DFS = null;
        // combination takes start
        // permutation does not take start, but visited
        DFS = (start, tmp) => {
            if (tmp.Sum() == target) { ans.Add(new List<int>(tmp)); return; }
            for (int i = start; i < candidates.Length; i++) {
                int n = candidates[i];
                if ((tmp.Sum() + n) > target) continue;
                tmp.Add(n);
                DFS(i, tmp);
                tmp.Remove(n);
            }
        };
        DFS(0, new List<int>());
        return ans;
    }
    // recursion
    public IList<IList<int>> CombinationSum(int[] candidates, int target) {
        var ans = new List<IList<int>>();
        Array.Sort(candidates);
        for (int i = 0; i < candidates.Length; i++) {
            int n = candidates[i];
            // prunning since it is sorted
            if (n > target) break; 
            if (n == target) { ans.Add(new List<int>(){n}); break;}
            int[] tmp = new int[candidates.Length - i];
            Array.Copy(candidates, i, tmp, 0,  tmp.Length);
            var res = CombinationSum(tmp, target - n);
            foreach (var a in res) { a.Add(n); ans.Add(a); }
        }
        return ans;
    }
}
