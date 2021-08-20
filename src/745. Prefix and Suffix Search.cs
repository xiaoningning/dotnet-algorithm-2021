// dict of prefix + suffix combo
// S: O(words.Legnth * w.Length ^2)
public class WordFilter {
    Dictionary<string, int> d = new Dictionary<string, int>();
    // T: O(words.Legnth * w.Length ^2)
    public WordFilter(string[] words) {
        for (int k = 0; k < words.Length; k++) {
            string w = words[k];
            for (int i = 0; i <= w.Length; i++) {
                for (int j = 0; j <= w.Length; j++) {
                    // all prefix + suffix combo
                    d[w.Substring(0, i) + "#" + w.Substring(w.Length - j)] = k;
                }   
            }
        }
    }
    // T: O(1)
    public int F(string prefix, string suffix) {
        return (d.ContainsKey(prefix + "#" + suffix)) ? d[prefix + "#" + suffix] : -1;
    }
}
// S: O(words.Legnth * w.Length ^2)
public class WordFilter1 {
    TrieNode root = new TrieNode();
    // T: O(words.Legnth * w.Length ^2)
    public WordFilter1(string[] words) {
        Action<string, int> buildTrie = null;
        buildTrie = (w, idx) => {
            var p = root;
            foreach (char c in w) {
                int i = c - 'a';
                if (c == '<') i = 26;
                if (c == '>') i = 27;
                if (p.children[i] == null) p.children[i] = new TrieNode();
                p = p.children[i];
                p.idx.Add(idx);
            }
        };
        
        for (int i = 0; i < words.Length; i++) {
            string w = words[i];
            // build a combined suffix + prefix
            // the tail of suffix must be there => suffix is in the front
            // the tail of prefix can be ignore => prefix is at the end
            // j == w.Length, build empty suffix as well
             for (int j = 0; j <= w.Length; j++)
                buildTrie(">" + w.Substring(j) + "<" + w, i);
        }
    }
    // T: O(w.Length)
    public int F(string prefix, string suffix) {
        Func<string,int> containsKey = null;
        containsKey = (w) => {
            var p = root;
            foreach (char c in w) {
                int i = c - 'a';
                if (c == '<') i = 26;
                if (c == '>') i = 27;
                p = p.children[i];
                if (p == null) return -1;
            }
            return p.idx.Last(); 
        };
        // suffix and prefix can be empty
        return containsKey(">" + suffix + "<" + prefix);
    }
}
public class TrieNode {
    public TrieNode () {
        idx = new List<int>();
        // < for prefix, > for suffix
        children = new TrieNode[28];
    }
    public List<int> idx;
    public TrieNode[] children;
}
/**
 * Your WordFilter object will be instantiated and called as such:
 * WordFilter obj = new WordFilter(words);
 * int param_1 = obj.F(prefix,suffix);
 */
