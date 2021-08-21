public class Solution {
    // Time complexity: O(n! + nlogn)
    // Space complexity: O(n)
    // DFS + prunning
    public bool CanPartitionKSubsets1(int[] nums, int k) {
        if (nums.Sum() % k != 0) return false;
        int t = nums.Sum() / k;
        Array.Sort(nums);
        var visited = new int[nums.Length];
        Func<int, int, int, bool> DFS = null;
        DFS = (start, sum, cnt) => {
            // all nums should be used
            if (cnt == 0) return visited.Sum() == nums.Length; 
            if (sum == t) return DFS(0, 0, cnt - 1);
            for (int i = start; i < nums.Length; i++) {
                if (visited[i] == 1) continue;
                // prunning search since nums is sorted
                if (sum + nums[i] > t) break; 
                visited[i] = 1;
                if (DFS(start + 1, sum + nums[i], cnt)) return true;
                visited[i] = 0;
            }
            return false;
        };
        return DFS(0,0, k);
    }
    // assume all positive nums
    // Search and fill buckets
    // Time complexity: O(n*k + nlogn)
    // Space complexity: O(n)
    public bool CanPartitionKSubsets(int[] nums, int k) {
        if (nums.Sum() % k != 0) return false;
        int t = nums.Sum() / k;
        Array.Sort(nums);
        int[] buckets = new int[k];
        Func<int, bool> DFS = null;
        DFS = (idx) => {
            // reach the end
            if (idx == -1) {
                foreach (var sum in buckets) if (sum != t) return false;
                return true;
            }
            int num = nums[idx];
            for (int i = 0; i < k; i++) {
                // prunning since nums is sorted
                // put num into a different bucket
                if (buckets[i] + num > t) continue;
                buckets[i] += num;
                if (DFS(idx - 1)) return true;
                buckets[i] -= num;
            }
            return false;
        };
        // sorted, start from the end
        return DFS(nums.Length - 1);
    }
}
