public class Solution {
    // BFS
    // T: O(n*26^word.Length)
    // S: O(n)
    public int LadderLength1(string beginWord, string endWord, IList<string> wordList) {
        var st = new HashSet<string>(wordList);
        if (!st.Contains(endWord)) return 0;
        var q = new Queue<string>();
        q.Enqueue(beginWord);
        int steps = 0;
        while (q.Any()) {
            steps++;
            int size = q.Count;
            while (--size >= 0) {
                string t = q.Dequeue();
                for (int i = 0; i < t.Length; i++) {
                    char[] arr = t.ToCharArray();
                    for (char c = 'a'; c <= 'z'; c++) {
                        arr[i] = c;
                        string nx = new string(arr);
                        // add endword as one step
                        if (nx == endWord) return steps + 1; 
                        if (st.Contains(nx) && nx != t) {
                            q.Enqueue(nx);
                            st.Remove(nx);
                        }
                    }
                }
            }
        }
        return 0;
    }
    // Bidirectional BFS
    // T: O(n*26^word.Length / 2)
    public int LadderLength(string beginWord, string endWord, IList<string> wordList) {
        var st = new HashSet<string>(wordList);
        if (!st.Contains(endWord)) return 0;
        int steps = 0;
        var q1 = new HashSet<string>(){beginWord};
        var q2 = new HashSet<string>(){endWord};
        while (q1.Any() && q2.Any()) {
            steps++;
            // always start the smaller set of q
            if (q1.Count > q2.Count) {var t = q1; q1 = q2; q2 = t;}
            var nq = new HashSet<string>();
            foreach (string w in q1) {
                for (int i = 0; i < w.Length; i++) {
                    char[] arr = w.ToCharArray();
                    for (char c = 'a'; c <= 'z'; c++) {
                        arr[i] = c;
                        string nx = new string(arr);
                        if (q2.Contains(nx)) return steps + 1;
                        if (st.Contains(nx) && nx != w) {
                            nq.Add(nx);
                            st.Remove(nx);
                        }
                    }
                }
            }
            q1 = nq;
        }
        return 0;
    }
}
