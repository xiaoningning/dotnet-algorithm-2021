public class Solution {
    // BFS
    // T: O(n*26^word.Length)
    // S: O(n)
    public IList<IList<string>> FindLadders1(string beginWord, string endWord, IList<string> wordList) {
        var ans = new List<IList<string>>();
        var dict = new HashSet<string>(wordList);
        if (!dict.Contains(endWord)) return ans;
        dict.Remove(beginWord);
        var q = new Queue<List<string>>();
        q.Enqueue(new List<string>(){beginWord});
        bool found = false; // found the shortest path
        while (q.Any() && !found) {
            foreach (var p in q) foreach (string w in p) dict.Remove(w);
            int size = q.Count;
            // BFS on the current level for all shortest paths
            while (--size >= 0) {
                var path = q.Dequeue();
                string w = path.Last();
                for (int i = 0; i < w.Length; i++) {
                    char[] cur = w.ToCharArray();
                    for (char c = 'a'; c <= 'z'; c++) {
                        cur[i] = c;
                        string nw = new string(cur);
                        if (!dict.Contains(nw)) continue;
                        var npath = new List<string>(path);
                        npath.Add(nw);
                        if (nw == endWord) { ans.Add(npath); found = true; }
                        else q.Enqueue(npath);
                    }
                }
            }
        }
        return ans;
    }
    // Bidirectional BFS
    // T: O(n*26^word.Length / 2)
    public IList<IList<string>> FindLadders(string beginWord, string endWord, IList<string> wordList) {
        var ans = new List<IList<string>>();
        var dict = new HashSet<string>(wordList);
        if (!dict.Contains(endWord)) return ans;
        dict.Remove(beginWord);dict.Remove(endWord);
        var q1 = new HashSet<string>(){beginWord};
        var q2 = new HashSet<string>(){endWord};
        
        bool found = false; // found the shortest path
        while (q1.Any() && q2.Any() && !found) {
            foreach (var p in q1) foreach (string w in p.Split(',')) dict.Remove(w);
            foreach (var p in q2) foreach (string w in p.Split(',')) dict.Remove(w);
            if (q1.First().Length > q2.First().Length) { var tq = q1; q1 = q2; q2 = tq; }
            var nq = new HashSet<string>();
            foreach (var path in q1) {
                string w = path.Split(',').Last();
                for (int i = 0; i < w.Length; i++) {
                    char[] cur = w.ToCharArray();
                    for (char c = 'a'; c <= 'z'; c++) {
                        cur[i] = c;
                        string nw = new string(cur);
                        // merge the final ans is complex. :<
                        if (q2.Any(x => x.IndexOf(nw) >= 0)) {
                            var path2Lst = q2.Where(x => x.IndexOf(nw) >= 0);
                            foreach (var p2 in path2Lst) {
                                var res = new List<string>();
                                string path1 = new string(path);
                                string path2 = new string(p2);
                                if (!path1.StartsWith(beginWord)) { var tp = path1; path1 = path2; path2 = tp; }
                                res = path1.Split(',').ToList();
                                var rpath2 = path2.Split(',').ToList();
                                rpath2.Reverse();
                                foreach (var str in rpath2) res.Add(str);
                                ans.Add(res);
                            }
                             
                            found = true; 
                        }
                        else if (dict.Contains(nw)) nq.Add(path + ',' + nw);
                    }
                }
            }
            q1 = nq;
        }
        return ans;
    }
}
