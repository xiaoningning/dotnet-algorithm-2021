// Binary Index Tree 2D
// T: O(logm * logn)
public class NumMatrix {
    int[,] mat;
    int[,] bitSums;
    public NumMatrix(int[][] matrix) {
        if (matrix.Length == 0 || matrix[0].Length == 0) return;
        int m = matrix.Length, n = matrix[0].Length;
        bitSums = new int[m + 1, n + 1];
        mat = new int[m+1, n+1];
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                Update(i, j, matrix[i][j]);
            }
        }
    }
    // build binary index tree 2D
    public void Update(int row, int col, int val) {
        for (int i = row + 1; i < bitSums.GetLength(0); i += lowBit(i))
           for (int j = col + 1; j < bitSums.GetLength(1); j += lowBit(j))
               bitSums[i, j] += val - mat[row+1, col+1];
        mat[row + 1,col + 1] = val;
    }
    // inclusive row1, col1
    public int SumRegion(int row1, int col1, int row2, int col2) {
        return GetSum(row2+1, col2+1) - GetSum(row2+1, col1) - GetSum(row1, col2+1) + GetSum(row1, col1);
    }
    // idx of bitSums
    int GetSum(int row, int col) {
        int sum = 0;
        for (int i = row; i > 0; i -= lowBit(i))
           for (int j = col; j > 0; j -= lowBit(j))
               sum += bitSums[i, j];
        return sum;
    }
    // low bit of index => how many nums in this index
    // x & (x ^ (x-1))  == x & -x
    int lowBit(int x) {
        // return x & (x ^ (x-1));
        return x & (-x);
    }
}

// Column Sum
// move matrix 2D sum to column 1D sum avoid some update
public class NumMatrix1 {
    int[][] mat;
    int[,] colSum;
    public NumMatrix1(int[][] matrix) {
        if (matrix.Length == 0 || matrix[0].Length == 0) return;
        mat = (int[][]) matrix.Clone();
        int m = matrix.Length, n = matrix[0].Length;
        colSum = new int[m + 1, n];
        for (int i = 1; i <= m; i++) {
            for (int j = 0; j < n; j++) {
                colSum[i,j] = colSum[i-1,j] + mat[i-1][j];
            }
        }
    }
    
    public void Update(int row, int col, int val) {
        for (int i = row + 1; i < colSum.GetLength(0); i++)
            colSum[i, col] += val - mat[row][col];
        mat[row][col] = val;
    }
    // inclusive row1, col1
    public int SumRegion(int row1, int col1, int row2, int col2) {
        int sum = 0;
        for (int i = col1; i <= col2; i++)
            sum += colSum[row2+1, i] - colSum[row1, i];
        return sum;
    }
}

/**
 * Your NumMatrix object will be instantiated and called as such:
 * NumMatrix obj = new NumMatrix(matrix);
 * obj.Update(row,col,val);
 * int param_2 = obj.SumRegion(row1,col1,row2,col2);
 */
