public class Solution {
    Dictionary<int, List<int>> prereq = new Dictionary<int, List<int>>();
    Dictionary<int, List<int>> course = new Dictionary<int, List<int>>();
    HashSet<int> visited = new HashSet<int>();
    // BFS
     public bool CanFinish1(int numCourses, int[][] prerequisites) {
        int n = prerequisites.Length;
        for (int c = 0; c < numCourses; c++)  {
            prereq[c] = new List<int>();
            course[c] = new List<int>();
            if (!Array.Exists(prerequisites, p => p[0] == c)) visited.Add(c);
        }
        // build directed graph
        for (int i = 0; i < n; i++) {
            int pre = prerequisites[i][1];
            int co = prerequisites[i][0];
            prereq[pre].Add(co);
            course[co].Add(pre);
        }
        var q = new Queue<int>();
        foreach (int c in visited) q.Enqueue(c);
        while (q.Any()) {
            var pre = q.Dequeue();
            foreach (var c in prereq[pre])  {
                course[c].Remove(pre);
                if (!course[c].Any())  {
                    visited.Add(c);
                    q.Enqueue(c);
                }
            }
        }
        return visited.Count == numCourses;
    }
    // DFS
    int[] courseState;
    // state: 0: unknown, 1: taken, 2: checking 
    public bool CanFinish(int numCourses, int[][] prerequisites) {
        int n = prerequisites.Length;
        courseState = new int[numCourses];
        for (int c = 0; c < numCourses; c++) course[c] = new List<int>();
        // build directed graph
        for (int i = 0; i < n; i++) {
            int pre = prerequisites[i][1];
            int co = prerequisites[i][0];
            course[co].Add(pre);
        }
        for (int c = 0; c < numCourses; c++)  {
           if (!canDFS(c)) return false;
        }
        return true;
    }
    bool canDFS(int c) {
        if (courseState[c] == 1) return true;
        if (courseState[c] == 2) return false; // cycled graph
        courseState[c] = 2;
        foreach (int p in course[c]) {
            if (!canDFS(p)) return false;
        }
        courseState[c] = 1;
        return true;
    }
}
