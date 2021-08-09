/* The Knows API is defined in the parent class Relation.
      bool Knows(int a, int b); */

public class Solution : Relation {
    public int FindCelebrity1(int n) {
        int ans = 0;
        // find ans
        for (int i = 0; i < n; i++) {
            // Candidate ans should not know ans
            if (Knows(ans, i)) ans = i;
        }
        // verify
        for (int i = 0; i < n; i++) {
            if (ans != i && (!Knows(i, ans) || Knows(ans, i))) return -1;
        }
        return ans;
    }
    // directed graph, in/out degress of node
    public int FindCelebrity(int n) {
        int[] ins = new int[n], outs = new int[n];
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < n; j++) {
                if (i != j && Knows(i, j)) {
                    ins[j]++; outs[i]++;
                }
            }
        }
        for (int i = 0; i < n; i++) {
            if (ins[i] == n - 1 && outs[i] == 0) return i;
        }
            
        return -1;
    }
}
