public class Solution {
    // recursion + memo cache => avoid duplicated calculation on the same string
    Dictionary<string, List<int>> memo = new Dictionary<string, List<int>>();
    public IList<int> DiffWaysToCompute(string expression) {
        Func<int,int,char,int> f = (x,y,op) => {
            if (op == '*') return x * y;
            if (op == '-') return x - y;
            if (op == '+') return x + y;
            return -1;
        };
        if (memo.ContainsKey(expression)) return memo[expression];
        var ans = new List<int>();
        for (int i = 0; i < expression.Length; i++) {
            var c = expression[i];
            if (char.IsDigit(c)) continue;
            var left = DiffWaysToCompute(expression.Substring(0,i));
            var right = DiffWaysToCompute(expression.Substring(i+1));
            foreach (int l in left) foreach (int r in right) ans.Add(f(l,r,c));
        }
        // no op char case
        // base case
        if (!ans.Any()) ans.Add(Int32.Parse(expression));
        return memo[expression] = ans;
    }
}
