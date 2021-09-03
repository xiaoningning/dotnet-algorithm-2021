public class Solution {
    // DP + bit mask
    // T: O(m* 2^n) S: O(2^n)
    public int[] SmallestSufficientTeam1(string[] req_skills, IList<IList<string>> people) {
        int n = req_skills.Length;
        int state = (1 << n) - 1;
        // pt: key =: state of skills
        var pt = new Dictionary<int, List<int>>(){{0, new List<int>()}};
        int m = people.Count;
        int[] skills = new int[m];
        for (int i = 0; i < m; i++) {
            int skill = 0;
            foreach (string s in people[i]) skill |= 1 << Array.FindIndex(req_skills, (x) => x == s);
            skills[i] = skill;
        }
        for (int i = 0; i < m; i++) {
            int skill = skills[i];
            var ks = pt.Keys.ToList();
            foreach (var k in ks) {
                int nk = k | skill;
                if (!pt.ContainsKey(nk) || pt[k].Count + 1 < pt[nk].Count) {
                    pt[nk] = new List<int>(pt[k]); 
                    pt[nk].Add(i);
                }
            }
        }
        return pt[(1 << n) - 1].ToArray();
    }
    // DP + bit mask 
    // v2 track the path of adding peopel
    // T: O(m* 2^n) S: O(2^n)
    public int[] SmallestSufficientTeam(string[] req_skills, IList<IList<string>> people) {
        int n = req_skills.Length;
        int state = (1 << n) - 1;
        int m = people.Count;
        int[] skills = new int[m];
        for (int i = 0; i < m; i++) {
            int skill = 0;
            foreach (string s in people[i]) skill |= 1 << Array.FindIndex(req_skills, (x) => x == s);
            skills[i] = skill;
        }
        // track min # of people at state
        int[] dp = new int[state + 1]; 
        Array.Fill(dp, Int32.MaxValue / 2);
        dp[0] = 0;
        // (state, i): track prev state and peopel in the path
        var path = new Dictionary<int, (int,int)>(); 
        for (int i = 0; i < m; i++) {
            int skill = skills[i];
            if (skill == 0) continue;
            for (int nx = state; nx >= 0; nx--) {
                if (dp[nx] + 1 < dp[nx|skill]) {
                    dp[nx|skill] = dp[nx] + 1;
                    path[nx|skill] = (nx, i);
                }
            }
        }
        int t = state;
        var ans = new List<int>();
        while (t != 0) {
            var p = path[t];
            ans.Add(p.Item2);
            t = p.Item1;
        }
        return ans.ToArray();
    }
    // DFS without memo => TLE
    public int[] SmallestSufficientTeam2(string[] req_skills, IList<IList<string>> people) {
        int n = req_skills.Length;
        int m = people.Count;
        var ans = new List<int>();
        int[] skills = new int[m];
        for (int i = 0; i < m; i++) {
            int skill = 0;
            foreach (string s in people[i]) skill |= 1 << Array.FindIndex(req_skills, (x) => x == s);
            skills[i] = skill;
        }
        
        Action<int, List<int>> DFS = null;
        DFS = (t, cur) => {
            if (t == (1 << n) - 1) {
                if (ans.Count == 0 || cur.Count < ans.Count) ans = new List<int>(cur);
                return;
            }
            // prunning 
            if (ans.Any() && cur.Count > ans.Count) return;
            for (int i = 0; i < m; i++) {
                if (cur.Contains(i)) continue;
                int skill = skills[i];
                cur.Add(i);
                DFS(t | skill, cur);
                cur.RemoveAt(cur.Count - 1);
            }
        };
        
        DFS(0, new List<int>());
        return ans.ToArray();
    }
}
