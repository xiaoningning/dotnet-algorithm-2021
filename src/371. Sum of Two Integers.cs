public class Solution {
    public int GetSum(int a, int b) {
        // 0 ^ 1 = 1, 1 ^ 1 = 0 => without carry case
        // 0 & 1 = 0, 1 & 1 = 1 => with carry case
        // GetSum(sumNoCarry, carry)
        return b == 0 ? a : GetSum(a ^ b, (a & b) << 1);
    }
}
