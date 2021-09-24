public class Solution {
    Dictionary<string, List<string>> g = new Dictionary<string, List<string>>();
    List<string> ans = new List<string>();
    // DFS
    public IList<string> FindItinerary1(IList<IList<string>> tickets) {
        foreach (var t in tickets) {
            if (!g.ContainsKey(t[0])) g[t[0]] = new List<string>();
            if (!g.ContainsKey(t[1])) g[t[1]] = new List<string>();
            g[t[0]].Add(t[1]);
        }
        foreach (var kv in g) kv.Value.Sort();
        DFS("JFK");
        ans.Reverse();
        return ans;
    }
    void DFS(string s) {
        while (g[s].Any()) {
            string nx = g[s].First();
            g[s].Remove(nx);
            DFS(nx);
        }
        ans.Add(s);
    }
    // stack
    public IList<string> FindItinerary(IList<IList<string>> tickets) {
        foreach (var t in tickets) {
            if (!g.ContainsKey(t[0])) g[t[0]] = new List<string>();
            if (!g.ContainsKey(t[1])) g[t[1]] = new List<string>();
            g[t[0]].Add(t[1]);
        }
        foreach (var kv in g) kv.Value.Sort();
        var st = new Stack<string>();
        st.Push("JFK");
        while (st.Any()) {
            var t = st.Peek();
            if (!g[t].Any()) ans.Insert(0, st.Pop());
            else {
                string nx = g[t].First();
                st.Push(nx);
                g[t].Remove(nx);
            }
        }
        return ans;
    }
}
