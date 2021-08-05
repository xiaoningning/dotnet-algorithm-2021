public class TimeMap {

    /** Initialize your data structure here. */
    public TimeMap() {
        m = new Dictionary<string, List<(int, string)>>();
    }
    
    public void Set(string key, string value, int timestamp) {
        if (!m.ContainsKey(key)) m[key] = new List<(int, string)>();
        var lst = m[key];
        int l = 0, r = lst.Count;
        while (l < r) {
            int m = l + (r - l) / 2;
            if (lst[m].t <= timestamp) l = m + 1;
            else r = m;
        }
        lst.Insert(r, (timestamp, value));
    }
    
    public string Get(string key, int timestamp) {
        if (!m.ContainsKey(key)) return "";
        var lst = m[key];
        if (timestamp < lst[0].t) return "";
        int l = 0, r = lst.Count;
        while (l < r) {
            int m = l + (r - l) / 2;
            if (lst[m].t <= timestamp) l = m + 1;
            else r = m;
        }
        return lst[--l].v;
    }
    // can use sorted dict or sorted list
    Dictionary<string, List<(int t, string v)>> m;
}

/**
 * Your TimeMap object will be instantiated and called as such:
 * TimeMap obj = new TimeMap();
 * obj.Set(key,value,timestamp);
 * string param_2 = obj.Get(key,timestamp);
 */
