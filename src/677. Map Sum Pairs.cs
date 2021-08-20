public class MapSum1 {
    Dictionary<string, int> vals = new Dictionary<string, int>();
    Dictionary<string, int> sums = new Dictionary<string, int>();
    public MapSum1() {}
    public void Insert(string key, int val) {
        int diff = val;
        if (vals.ContainsKey(key)) diff -= vals[key];
        vals[key] = val;
        for (int i = 1; i <= key.Length; i++) {
            string prefix = key.Substring(0, i);
            if (!sums.ContainsKey(prefix)) sums[prefix] = 0;
            sums[prefix] += diff;
        }
    }
    public int Sum(string prefix) {
        return sums.ContainsKey(prefix) ? sums[prefix] : 0;
    }
}
public class MapSum {
    TrieNode root = new TrieNode();
    // keep the previous val of key
    Dictionary<string, int> vals = new Dictionary<string, int>();
    public MapSum() {}
    // insert could update the old val
    public void Insert(string key, int val) {
        int diff = val;
        if (vals.ContainsKey(key)) diff -= vals[key];
        vals[key] = val;
        var p = root;
        foreach (char c in key) {
            int idx = c - 'a';
            if (p.children[idx] == null) p.children[idx] = new TrieNode();
            p = p.children[idx];
            p.sum += diff;
        }
        p.isWord = true;
    }
    public int Sum(string prefix) {
        if (string.IsNullOrEmpty(prefix)) return 0;
        var p = root;
        foreach (char c in prefix) {
            int idx = c - 'a';
            if (p.children[idx] == null) return 0;
            p = p.children[idx];
        }
        return p.sum;
    }
}
public class TrieNode {
    public TrieNode () {
        sum = 0;
        isWord = false;
        children = new TrieNode[26];
    }
    public int sum;
    public bool isWord;
    public TrieNode[] children;
}

/**
 * Your MapSum object will be instantiated and called as such:
 * MapSum obj = new MapSum();
 * obj.Insert(key,val);
 * int param_2 = obj.Sum(prefix);
 */
