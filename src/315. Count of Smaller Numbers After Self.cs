public class Solution {
    // binary search
    public IList<int> CountSmaller(int[] nums) {
        int n = nums.Length;
        var ans = new int[n];
        var t = new List<int>();
        // cnt after self, start with the last one
        for (int i = n - 1; i >= 0; i--){
            int l = 0, r = t.Count;
            while (l < r) {
                int m = l + (r - l) / 2;
                if (t[m] < nums[i]) l = m + 1;
                else r = m;
            }
            t.Insert(r, nums[i]);
            ans[i] = r;
        }
        // O(nlogn)
        return ans;
    }
    // binary index tree
    // TLE
    // O(nlogn) -> worst: O(n^2)
    public IList<int> CountSmaller1(int[] nums) {
        int n = nums.Length;
        var ans = new int[n];
        Node root = null;
        for (int i = n - 1; i >= 0; i--) {
            ans[i] = Insert(ref root, nums[i]);
        }
        return ans;
    }
    public class Node {
        public int val;
        public int smallerCnt;
        public Node left;
        public Node right;
        public Node (int v, int cnt) {
            this.val = v;
            this.smallerCnt = cnt;
            this.left = null;
            this.right = null;
        }
    }
    public int Insert(ref Node root, int v) {
        if (root == null) {
            root = new Node(v, 0);
            return 0;
        }
        if (root.val > v) { 
            root.smallerCnt++;
            return Insert(ref root.left, v);
        }
        else {
            return Insert(ref root.right, v) + root.smallerCnt + (v > root.val ? 1 : 0);
        }
    }
}
