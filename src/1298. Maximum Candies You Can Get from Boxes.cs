public class Solution {
    public int MaxCandies(int[] status, int[] candies, int[][] keys, int[][] containedBoxes, int[] initialBoxes) {
        int n = status.Length;
        var q = new Queue<int>();
        // the opened one has key already
        int[] hasKey = (int[])status.Clone();
        int[] found = new int[n];
        foreach (int b in initialBoxes) {
            found[b] = 1;
            if (hasKey[b] == 1) q.Enqueue(b);
        }
        int ans = 0;
        // check if any found box has key
        while (q.Any()) {
            var t = q.Dequeue();
            ans += candies[t];
            foreach (int b in containedBoxes[t]) { 
                found[b] = 1;
                if (hasKey[b] == 1) q.Enqueue(b);
            }
            foreach (int k in keys[t]) {
                if (hasKey[k] == 0 && found[k] == 1) q.Enqueue(k);
                hasKey[k] = 1;
            }
        }
        // T: O(boxes + keys)
        return ans;
    }
}
