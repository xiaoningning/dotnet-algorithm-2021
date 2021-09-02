public class Solution {
    // DP + bit mask
    // T: O(m* 2^n) S: O(2^n)
    public int[] SmallestSufficientTeam(string[] req_skills, IList<IList<string>> people) {
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
                    pt[nk] = new List<int>(pt[k]); pt[nk].Add(i);
                }
            }
        }
        return pt[(1 << n) - 1].ToArray();
    }
}
