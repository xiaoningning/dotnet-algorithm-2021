public class Solution {
    // HashSet with prunning. faster than trie
    // T: O(n * sum (w.length))
    public string LongestWord2(string[] words) {
        var st = new HashSet<string>();
        foreach (string w in words) st.Add(w);
        string ans = "";
        foreach (string w in words) {
            if (w.Length < ans.Length || (w.Length == ans.Length && ans.CompareTo(w) < 0)) continue;
            bool valid = true;
            for (int i = 1; i <= w.Length; i++) {
                // prunning if invalid
                if (!st.Contains(w.Substring(0, i))) { valid = false; break; }
            }
            if (valid) ans = w;
        }
        return ans;
    }

    // Trie + no sorting
    // T: O (n * w.length + sum(w.length)) no sorting of words
    public string LongestWord1(string[] words) {
        TrieNode root = new TrieNode();
        
        Action<string> buildTrie = null;
        buildTrie = (w) => {
            var p = root;
            foreach (char c in w) {
                int idx = c - 'a';
                if (p.children[idx] == null) p.children[idx] = new TrieNode();
                p = p.children[idx];
            }
            p.word = w;
        };
        
        foreach (string w in words) buildTrie(w);
        
        string ans = "";
        Func<TrieNode, string> buildWord = null;
        buildWord = (root) => {
            var p = root;
            string x = "";
            foreach (var c in p.children) {
                if (c != null && c.word != null) x = buildWord(c);
                if (x.Length > ans.Length) ans = x;
                if (x.Length == ans.Length) {
                    var tmp = new List<string>(){x, ans};
                    tmp.Sort();
                    ans = tmp[0];
                }
            }
            return x == "" ? (p != null && p.word != null ? p.word : "") : ans;
        };
        
        return buildWord(root);
    }
    // Trie + sorting
    // T: O (n*logn + sum(w.length)) sorting of words first
    public string LongestWord(string[] words) {
        TrieNode root = new TrieNode();
        
        Action<string> buildTrie = null;
        buildTrie = (w) => {
            var p = root;
            foreach (char c in w) {
                int idx = c - 'a';
                if (p.children[idx] == null) p.children[idx] = new TrieNode();
                p = p.children[idx];
            }
            p.word = w;
        };
        
        foreach (string w in words) buildTrie(w);
        
        string ans = "";
        Func<string, bool> contains = null;
        contains = (w) => {
            var p = root;
            foreach (char c in w) {
                int idx = c - 'a';
                p = p.children[idx];
                if (p == null || p.word == null) return false; 
            }
            return true;
        };
        
        var tmp = new List<string>(words);
        tmp.Sort((x,y) => {
            if (x.Length == y.Length) return x.CompareTo(y);
            else return y.Length - x.Length;
        });
        foreach (string w in tmp) {
            if (contains(w)) return w;
        }
        return "";
    }
}
public class TrieNode {
    public TrieNode () {
        word = null;
        children = new TrieNode[26];
    }
    public string word;
    public TrieNode[] children;
}
