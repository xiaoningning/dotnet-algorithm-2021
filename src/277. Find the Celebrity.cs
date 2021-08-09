/* The Knows API is defined in the parent class Relation.
      bool Knows(int a, int b); */

public class Solution : Relation {
    public int FindCelebrity(int n) {
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
}
