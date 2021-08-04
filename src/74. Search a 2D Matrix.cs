public class Solution {
    public bool SearchMatrix1(int[][] matrix, int target) {
        if (matrix.Length == 0 || matrix[0].Length == 0) return false;
        int m = matrix.Length, n = matrix[0].Length;
        int l = 0, r = matrix.Length;
        while (l < r) {
            int mid = l + (r - l) / 2;
            if (matrix[mid][0] == target) return true;
            else if (matrix[mid][0] < target) l = mid + 1;
            else r = mid;
        }
        int tmp = (r > 0) ? (r - 1) : r;
        l = 0;
        r = matrix[tmp].Length;
        while (l < r) {
            int mid = l + (r - l) / 2;
            if (matrix[tmp][mid] == target) return true;
            else if (matrix[tmp][mid] < target) l = mid + 1;
            else r = mid;
        }
        return false;
    }
    public bool SearchMatrix(int[][] matrix, int target) {
        if (matrix.Length == 0 || matrix[0].Length == 0) return false;
        int m = matrix.Length, n = matrix[0].Length;
        int i = 0, j = n - 1;
        while (i < m && j >= 0) {
            if (matrix[i][j] == target) return true;
            if (target < matrix[i][j] ) j--;
            else i++;
        }
        return false;
    }   
}
