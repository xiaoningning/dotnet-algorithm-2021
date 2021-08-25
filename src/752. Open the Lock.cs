public class Solution {
    // BFS
    public int OpenLock(string[] deadends, string target) {
        string start = "0000";
        // hastset is much faster for contains
        var dict = new HashSet<string>(deadends);
        if (dict.Contains(start)) return -1;
        if (target == start) return 0;
        int steps = 0;
        var used = new HashSet<string>(){start};
        var q = new Queue<string>();
        q.Enqueue(start);
        while (q.Any()) {
            steps++;
            int size = q.Count;
            while (--size >= 0) {
                var t = q.Dequeue();
                for (int i = 0; i < t.Length; i++) {
                    foreach (int j in new int[]{1, -1}) {
                        char[] ct = t.ToCharArray();
                        ct[i] = (char)((ct[i] - '0' + j + 10) % 10 + '0');
                        string nx = new string(ct);
                        if (nx == target) return steps;
                        if (dict.Contains(nx) || used.Contains(nx)) continue;
                        used.Add(nx);
                        q.Enqueue(nx);
                    }
                }
            }
        }
        return -1;
    }
}
