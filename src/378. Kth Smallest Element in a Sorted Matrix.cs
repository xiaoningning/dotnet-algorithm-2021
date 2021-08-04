public class Solution {
    // rows and columns ascending order
    // it is fully ordered in matrix
    // T: O(nlogn * log(max - min))
    public int KthSmallest(int[][] matrix, int k) {
        int n = matrix.Length;
        long l = matrix[0][0], r = matrix[n-1][n-1] + 1;
        while (l < r) {
            long m = l + (r - l) / 2;
            int cnt = 0;
            foreach(var row in matrix) {
                foreach (int x in row) if (x <= m) cnt++;
            }
            if (cnt < k) l = m + 1;
            else r = m;
        }
        return (int)l;
    }
}
