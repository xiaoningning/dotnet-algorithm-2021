public class Solution {
    // BFS
    public int Racecar(int target) {
        int steps = 0;
        //(position, speed)
        var q = new Queue<(int, int)>();
        q.Enqueue((0,1));
        var visited = new HashSet<(int, int)>();
        visited.Add((0,1));
        while (q.Any()) {
            int size = q.Count;
            while (--size >= 0) {
                var t = q.Dequeue();
                int pos = t.Item1, speed = t.Item2;
                if (pos == target) return steps;
                int npos = pos + speed, nspeed = speed * 2;
                // can not go too far, but by pass target then reverse
                if (!visited.Contains((npos, nspeed)) && npos > 0 && npos < target * 2) {
                    q.Enqueue((npos,nspeed));
                    visited.Add((npos,nspeed));
                }
                npos = pos; nspeed = speed > 0 ? -1 : 1;
                // can not go back to far
                if (!visited.Contains((npos, nspeed)) && npos >= target / 2) {
                    q.Enqueue((npos,nspeed));
                    visited.Add((npos,nspeed));
                }
            }
            steps++;
        }
        return -1;
    }
}
