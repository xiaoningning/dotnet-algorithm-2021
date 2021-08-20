// T: O(searchWord.Length * cnt(dict))
public class MagicDictionary1 {
    Trie p;
    /** Initialize your data structure here. */
    public MagicDictionary1() { p = new Trie(); }
    public void BuildDict(string[] dictionary) {
        foreach (string w in dictionary) p.Insert(w);
    }
    // replace char to search
    public bool Search(string searchWord) {
        if (string.IsNullOrEmpty(searchWord)) return false;
        var cur = p.root;
        for (int i = 0; i < searchWord.Length; i++) {
            for (char c = 'a'; c <= 'z'; c++) {
                if (c == searchWord[i]) continue;
                var tmp = searchWord.ToArray();
                tmp[i] = c;
                if (Contains(cur, new string(tmp).Substring(i))) return true;
            }
            cur = cur.children[searchWord[i] - 'a'];
            if (cur == null) return false;
        }
        return false;
    }
    bool Contains(TrieNode p, string w) {
        var cur = p;
        foreach (char c in w) {
            cur = cur.children[c - 'a'];
            if (cur == null) return false;
        }
        return cur.isWord;
    }
    // search and count replaced chars
    public bool Search1(string searchWord) {
        if (string.IsNullOrEmpty(searchWord)) return false;
        else return Search(p.root, searchWord, 0);
    }
    bool Search(TrieNode p, string w, int cnt) {
        for (int i = 0; i < w.Length; i++) {
            for (int j = 0; j < 26; j++){
                if (p.children[j] == null) continue;
                if (Search(p.children[j], w.Substring(i+1), w[i] - 'a' == j ? cnt : cnt + 1)) return true;
            }   
            return false;
        }
        return cnt == 1 && p.isWord;
    }
}
public class Trie {
    public TrieNode root;
    public Trie() { root = new TrieNode(); }
    public void Insert(string w) {
        if (string.IsNullOrEmpty(w)) return;
        var p = root;
        foreach(char c in w) {
            int idx = c - 'a';
            if (p.children[idx] == null) p.children[idx] = new TrieNode();
            p = p.children[idx];
        }
        p.isWord = true;
    }
}
public class TrieNode {
    public bool isWord;
    public TrieNode[] children;
    public TrieNode() {
        children = new TrieNode[26];
        isWord = false;
    }
}
// T: O(searchWord.Length * cnt(dict))
public class MagicDictionary {
    Dictionary<int, List<string>> d ;
    /** Initialize your data structure here. */
    public MagicDictionary() { d = new Dictionary<int, List<string>>(); }
    public void BuildDict(string[] dictionary) {
        foreach (string w in dictionary) {
            if (!d.ContainsKey(w.Length)) d[w.Length] = new List<string>();
            d[w.Length].Add(w);
        }
    }
    public bool Search(string searchWord) {
        if (!d.ContainsKey(searchWord.Length)) return false;
        foreach (string str in d[searchWord.Length]) {
            int cnt = 0, i = 0;
            for (i = 0; i < searchWord.Length; i++) {
                if (str[i] == searchWord[i]) continue;
                // reduce some search space
                if (str[i] != searchWord[i] && cnt == 1) break;
                ++cnt;
            }
            if (i == searchWord.Length && cnt == 1) return true;
        }   
        return false;
    }
}
/**
 * Your MagicDictionary object will be instantiated and called as such:
 * MagicDictionary obj = new MagicDictionary();
 * obj.BuildDict(dictionary);
 * bool param_2 = obj.Search(searchWord);
 */
