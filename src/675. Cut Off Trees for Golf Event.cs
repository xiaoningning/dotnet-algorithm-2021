public class Solution {
    // Tree might not start from 2, it might not have 1
    // => find the shortest tree and tallest tree;
    //BFS T: O(mn*mn)
    public int CutOffTree(IList<IList<int>> forest) {
        int m = forest.Count, n = forest[0].Count;
        var trees = new List<int>();
        foreach (var r in forest) foreach (var c in r) if (c > 1) trees.Add(c);
        trees.Sort();
        Func<int,int,int,(int,int,int)> BFS = null;
        var dirs = new int[,]{{1,0},{-1,0},{0,1},{0,-1}};
        // BFS T:O(m*n)
        BFS = (sx, sy, target) => {
           var visited = new int[m,n];
            visited[sx,sy] = 1;
            var q = new Queue<(int, int)>();
            q.Enqueue((sx,sy));
            int steps = 0; 
            while (q.Any()) {
                int size = q.Count;
                while (--size >= 0) {
                    var t = q.Dequeue();
                    int x = t.Item1, y = t.Item2;
                    if (forest[x][y] == target) return (x, y, steps);
                    for (int d = 0; d < 4; d++) {
                        int i = x + dirs[d,0], j = y + dirs[d,1];
                        if (i < 0 || i >= m || j < 0 || j >= n || visited[i,j] == 1 || forest[i][j] == 0) continue;
                        q.Enqueue((i,j));
                        visited[i,j] = 1;
                    }
                }
                steps++;
            }
            return (-1,-1,-1);
        };
        int ans = 0, sx = 0, sy = 0;
        foreach(int t in trees) {
            var tmp = BFS(sx,sy,t);
            if (tmp.Item3 == -1) return -1;
            sx = tmp.Item1; sy = tmp.Item2;
            ans += tmp.Item3;
        }
        return ans;
    }
}
