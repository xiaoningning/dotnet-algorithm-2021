public class Solution {
    // OJ failed by sorting result not the same
    // BFS
    public IList<IList<string>> AccountsMerge(IList<IList<string>> accounts) {
        var users = new Dictionary<string, List<int>>();
        var ans = new List<IList<string>>();
        int n = accounts.Count;
        for (int i = 0; i < n; i++) {
            for (int j = 1; j < accounts[i].Count; j++) {
                if (!users.ContainsKey(accounts[i][j])) users[accounts[i][j]] = new List<int>();
                users[accounts[i][j]].Add(i);
            }
        }
        var visited = new int[n];
        for (int i = 0; i < n; i++) {
            if (visited[i] == 1) continue;
            var q = new Queue<int>();
            var st = new HashSet<string>();
            q.Enqueue(i);
            while (q.Any()) {
                var t = q.Dequeue();
                visited[t] = 1;
                var mails = new List<string>();
                for (int j = 1; j < accounts[t].Count; j++) mails.Add(accounts[t][j]);
                foreach (string m in mails) {
                    st.Add(m);
                    foreach (int id in users[m]) {
                        if (visited[id] == 1) continue;
                        q.Enqueue(id);
                    }
                }
            }
            var lst = st.ToList();
            lst.Sort();
            lst.Insert(0, accounts[i][0]);
            ans.Add(lst);
        }
        return ans;
    }
    // Dijkstra set: Union find
    int[] roots;
    public IList<IList<string>> AccountsMerge1(IList<IList<string>> accounts) {
        Dictionary<int, string> names = new Dictionary<int, string>();
        Dictionary<string, int> ids = new Dictionary<string, int>(); 
        Dictionary<int, List<string>> merged = new Dictionary<int, List<string>>();
        int n = accounts.Count;
        roots = new int[n];
        for (int i = 0; i < n; i++) roots[i] = i;
        for (int i = 0; i < n; i++) {
            int len = accounts[i].Count;
            for (int j = 1; j < len; j++) {
               if (ids.ContainsKey(accounts[i][j])) roots[i] = UnionFind(ids[accounts[i][j]]);
               else ids[accounts[i][j]] = UnionFind(i);
            }
            names[UnionFind(i)] = accounts[i][0];
            merged[i] = new List<string>();
        }
        foreach (var a in accounts) {
            for (int j = 1; j < a.Count; j++) merged[ids[a[j]]].Add(a[j]);
        }
        var ans = new List<IList<string>>();
        var st = new HashSet<int>(new List<int>(roots));
        foreach (var id in st) {
            var t = new HashSet<string>();
            t.Add(names[id]);
            foreach (string e in merged[id]) t.Add(e);
            var tl = t.ToList(); 
            tl.Sort((x,y) => {
                if (x.Length == y.Length) return string.Compare(x, y);
                else return x.Length - y.Length;
            });
            ans.Add(tl);
        }
        return ans;
    }
    int UnionFind(int x) {
        return roots[x] == x ? x : roots[x] = UnionFind(roots[x]); 
    }
}
