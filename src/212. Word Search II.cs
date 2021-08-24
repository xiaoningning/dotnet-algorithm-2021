public class Solution {
    // Trie
    // T: O(sum(words) + 4^maxLength(words))
    // S: O(sum(words) + words)
    public IList<string> FindWords1(char[][] board, string[] words) {
        var ans = new List<string>();
        if (words.Length == 0 || board.Length == 0 || board[0].Length == 0) return ans;
        int m = board.Length, n = board[0].Length;
        int[,] dirs = new int[4,2]{{1,0},{-1,0},{0,1},{0, -1}};
        Trie T = new Trie();
        foreach (string w in words) T.Insert(w);
        
        Action<TrieNode, int, int, int[,]> DFS = null;
        DFS = (r, x, y, seen) =>{
            var p = r.children[board[x][y] - 'a'];
            if (p == null) return;
            // if found one, just remove it from Trie in case overlapped word
            if (p.w != null) { ans.Add(p.w); p.w = null; }
            seen[x, y] = 1;
            for (int d = 0; d < 4; d++) { 
                int i = x + dirs[d,0], j = y + dirs[d,1];
                if (i < 0 || i >= m || j < 0 || j >= n || seen[i, j] == 1) continue;
                DFS(p, i, j, seen);
            }
            seen[x, y] = 0;
        };
        
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                DFS(T.root, i, j, new int[m,n]);
            }
        }
        return ans;
    }
    // DFS
    // T: O(sum(m*n*4^l)) l: words.length
    // S: O(l)
    public IList<string> FindWords(char[][] board, string[] words) {
        var ans = new List<string>();
        if (words.Length == 0 || board.Length == 0 || board[0].Length == 0) return ans;
        
        int m = board.Length, n = board[0].Length;
        Func<string, int, int, int, bool> Search = null;
        Search = (w, d, i, j) => {
            if (d == w.Length) return true;
            if (i < 0 || i >= m || j < 0 || j >= n || board[i][j] != w[d]) return false;
            char t = board[i][j];
            board[i][j] = '#';
            bool ans = Search(w, d+1, i+1, j)
                    || Search(w, d+1, i-1, j)
                    || Search(w, d+1, i, j+1)
                    || Search(w, d+1, i, j-1);
            board[i][j] = t;
            return ans;
        };
        Func<string, bool> Find = null;
        Find = (w) => {
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    if (Search(w, 0, i, j)) return true;
            return false;
        };
        var dict = new HashSet<string>(words);
        foreach (string w in dict) if (Find(w)) ans.Add(w);
        return ans;
    }
}

public class Trie {
    public Trie() { root = new TrieNode(); }
    public void Insert(string word) {
        TrieNode p = root;
        foreach (char c in word) {
            int idx = c - 'a';
            if (p.children[idx] == null) p.children[idx] = new TrieNode();
            p = p.children[idx];
        }
        p.w = word;
    }
    public TrieNode root;
}
public class TrieNode {
    public TrieNode () {
        w = null;
        children = new TrieNode[26];
    }
    public string w; // keep word for searching result
    public TrieNode[] children;
}
