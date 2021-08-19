public class Solution {
    public string ReplaceWords(IList<string> dictionary, string sentence) {
        var root = new TrieNode();
        foreach (string w in dictionary) Insert(root, w);
        var ans = new List<string>();
        foreach (string s in sentence.Split(" ")) {
            string res = Search(root, s);
            ans.Add(res == null ? s : res);
        }
        return string.Join(" ", ans);
    }
    void Insert(TrieNode root, string w) {
        if (string.IsNullOrEmpty(w)) return;
        var p = root;
        foreach (char c in w) {
            int idx = c - 'a';
            if (p.children[idx] == null) p.children[idx] = new TrieNode();
            p = p.children[idx];
        }
        p.str = w;
    }
    string Search(TrieNode root, string w) {
        if (string.IsNullOrEmpty(w)) return null;
        var p = root;
        foreach (char c in w) {
            int idx = c - 'a';
            p = p.children[idx];
            if (p == null) return null;
            else if (p.str != null)  return p.str;
        }
        return null;
    }
}
public class TrieNode {
    public string str;
    public TrieNode[] children;
    public TrieNode() {
        children = new TrieNode[26];
        str = null;
    }
}
