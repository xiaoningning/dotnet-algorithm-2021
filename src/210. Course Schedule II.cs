public class Solution {
    List<int> ans = new List<int>();
    Dictionary<int, List<int>> course = new Dictionary<int, List<int>>();
    Dictionary<int, List<int>> prereq = new Dictionary<int, List<int>>();
    // BFS in/out degress of nodes
    public int[] FindOrder(int numCourses, int[][] prerequisites) {
        int[] ins = new int[numCourses];
        for (int i = 0; i < numCourses; i++) prereq[i] = new List<int>();
        foreach (var p in prerequisites) {
            prereq[p[1]].Add(p[0]);
            ins[p[0]]++;
        }
        var q = new Queue<int>();
        for (int i = 0; i < numCourses; i++) { 
            if (ins[i] == 0) {
                q.Enqueue(i);
                ans.Add(i);
            }
        }
        while (q.Any()) {
            var t = q.Dequeue();
            foreach (var c in prereq[t]) {
                if (--ins[c] == 0) {
                    q.Enqueue(c);
                    ans.Add(c);
                }
            }
        }
        // T: O(V + E^2)
        return ans.Count != numCourses ? new int[]{} : ans.ToArray();
    }
    // DFS
    // state: 0: unknown, 1: taken, 2: checking
    int[] courseState;
    public int[] FindOrder1(int numCourses, int[][] prerequisites) {
        courseState = new int[numCourses];
        for (int i = 0; i < numCourses; i++) course[i] = new List<int>();
        foreach (var p in prerequisites) course[p[0]].Add(p[1]);
        for (int i = 0; i < numCourses; i++) DFS(i);
        // T: O(V+E)
        return ans.Count != numCourses ? new int[]{} : ans.ToArray();
    }
    bool DFS(int c) {
        if (courseState[c] == 1) return true;
        if (courseState[c] == 2) return false;
        courseState[c] = 2;
        foreach (var co in course[c]) if (!DFS(co)) return false;
        courseState[c] = 1;
        ans.Add(c);
        return true;
    }
}
