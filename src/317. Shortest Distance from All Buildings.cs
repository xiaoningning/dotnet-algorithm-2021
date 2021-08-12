public class Solution {
    // a single source path problem
    // BFS v2
    public int ShortestDistance(int[][] grid) {
        int m = grid.Length, n = grid[0].Length;
        int ans = Int32.MaxValue;
        // track empty land val
        // each round it is different
        // if empty land val is not change,
        // then it does not reach some building in the previous round
        // this way, no need to check building cnt of each empty land
        int targetEmptyVal = 0;
        // total travel distance to all buildings
        int[,] sum = new int[m,n];
        var dirs = new int[4,2]{{1,0},{-1,0},{0,1},{0,-1}};
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (grid[i][j] == 1) {
                    ans = Int32.MaxValue;
                    var dist = new int[m,n];
                    var q = new Queue<(int,int)>();
                    q.Enqueue((i, j));
                    // BFS from only 1 build to all 0 each time
                    while (q.Any()) {
                        var t = q.Dequeue();
                        int x = t.Item1, y = t.Item2;
                        for (int d = 0; d < 4; d++) {
                            int a = x + dirs[d,0], b = y + dirs[d,1];
                            if (a >= 0 && a < m && b >= 0 && b < n && grid[a][b] == targetEmptyVal) {
                                // update targetEmptyVal in grid for next round
                                grid[a][b]--; 
                                dist[a, b] = dist[x, y] + 1;
                                sum[a,b] += dist[a, b];
                                ans = Math.Min(ans, sum[a,b]);
                                q.Enqueue((a, b));
                            }
                        }
                    }
                    // building/obstacle is positive val
                    // serve as visited check
                    // as well as track previous round
                    // of empty land being reached
                    targetEmptyVal--;
                }
            }
        }
        // if never build a new house, ans is till m*n
        return ans == Int32.MaxValue ? -1 : ans;
    }
    // BFS v1
    public int ShortestDistance1(int[][] grid) {
        int m = grid.Length, n = grid[0].Length;
        int ans = Int32.MaxValue, building = 0;
        // total travel distance to all buildings
        int[,] sum = new int[m,n];
        // track # of building reached by each empty land 
        int[,] buildingCnt = new int[m,n];
        var dirs = new int[4,2]{{1,0},{-1,0},{0,1},{0,-1}};
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (grid[i][j] == 1) {
                    building++;
                    // each BFS needs visited/dist
                    var visited = new int[m,n];
                    var dist = new int[m,n];
                    var q = new Queue<(int,int)>();
                    q.Enqueue((i, j));
                    // BFS from only 1 build to all 0 each time
                    while (q.Any()) {
                        var t = q.Dequeue();
                        int x = t.Item1, y = t.Item2;
                        for (int d = 0; d < 4; d++) {
                            int a = x + dirs[d,0], b = y + dirs[d,1];
                            if (a < 0 || a >= m || b < 0 || b >= n || grid[a][b] != 0 || visited[a,b] == 1) continue;
                            visited[a,b] = 1;
                            dist[a, b] = dist[x, y] + 1;
                            sum[a,b] += dist[a, b];
                            buildingCnt[a, b]++;
                            q.Enqueue((a, b));
                        }
                    }
                }
            }
        }
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (grid[i][j] == 0 && buildingCnt[i,j] == building) 
                    ans = Math.Min(ans, sum[i,j]);
                }
        }
        // if never build a new house, ans is till m*n
        return ans == Int32.MaxValue ? -1 : ans;
    }
}
