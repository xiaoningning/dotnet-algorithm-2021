// search/insert T: O(word.Length)
public class Trie {
    /** Initialize your data structure here. */
    public Trie() { root = new TrieNode(); }
    /** Inserts a word into the trie. */
    public void Insert(string word) {
        TrieNode p = root;
        foreach (char c in word) {
            int idx = c - 'a';
            if (p.children[idx] == null) p.children[idx] = new TrieNode();
            p = p.children[idx];
        }
        p.isWord = true;
    }
    /** Returns if the word is in the trie. */
    public bool Search(string word) {
        TrieNode node = Find(word);
        return node != null && node.isWord;
    }
    /** Returns if there is any word in the trie that starts with the given prefix. */
    public bool StartsWith(string prefix) {
        return Find(prefix) != null;
    }
    
    TrieNode Find(string prefix) {
        TrieNode p = root;
        foreach (char c in prefix) {
            int idx = c - 'a';
            if (p.children[idx] == null) return null;
            p = p.children[idx];
        }
        return p;
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
 * Your Trie object will be instantiated and called as such:
 * Trie obj = new Trie();
 * obj.Insert(word);
 * bool param_2 = obj.Search(word);
 * bool param_3 = obj.StartsWith(prefix);
 */
