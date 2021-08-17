public class WordDictionary {
    /** Initialize your data structure here. */
    public WordDictionary() { root = new TrieNode(); }
    public void AddWord(string word) {
        TrieNode p = root;
        foreach (char c in word) {
            int idx = c - 'a';
            if (p.children[idx] == null) p.children[idx] = new TrieNode();
            p = p.children[idx];
        }
        p.isWord = true;
    }
    public bool Search(string word) {
        return Search(root, word, 0);
    }
    bool Search(TrieNode r, string word, int i) {
        if (i == word.Length) return r != null && r.isWord;
        if (word[i] == '.') {
            foreach (TrieNode c in r.children) 
                if (c != null && Search(c, word, i + 1)) return true;
            return false;
        }
        else {
            TrieNode c = r.children[word[i] - 'a'];
            return c != null && Search(c, word, i + 1);
        }
    }
    private TrieNode root;
}

public class TrieNode {
    public TrieNode () {
        isWord = false;
        children = new TrieNode[26];
    }
    public bool isWord;
    public TrieNode[] children;
}


/**
 * Your WordDictionary object will be instantiated and called as such:
 * WordDictionary obj = new WordDictionary();
 * obj.AddWord(word);
 * bool param_2 = obj.Search(word);
 */
