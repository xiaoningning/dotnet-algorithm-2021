public class Solution {
    HashSet<int> seen = new HashSet<int>();
    // DFS
    public bool CanVisitAllRooms1(IList<IList<int>> rooms) {
        if (!rooms.Any()) return true;
        DFS(rooms, 0);
        return seen.Count == rooms.Count;
    }
    void DFS(IList<IList<int>> rooms, int i) {
        if (seen.Contains(i)) return;
        seen.Add(i);
        foreach (int k in rooms[i]) DFS(rooms, k);
    }
    // BFS
    public bool CanVisitAllRooms(IList<IList<int>> rooms) {
        if (!rooms.Any()) return true;
        var q = new Queue<int>();
        q.Enqueue(0);
        while (q.Any()) {
            var r = q.Dequeue();
            seen.Add(r);
            foreach (int k in rooms[r]) {
                if (seen.Contains(k)) continue;
                seen.Add(k);
                q.Enqueue(k);
            }
        }
        return seen.Count == rooms.Count;
    }
}
