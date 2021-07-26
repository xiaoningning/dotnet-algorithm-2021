using System;
using System.Collections.Generic;
using System.Linq;
					
public class Program
{
	public static void Main()
	{
		var intarray = Enumerable.Range(1,5);	
		Console.WriteLine("enumerable range:"+string.Join(',', intarray));
		var sarray = Enumerable.Repeat("a", 5);
		Console.WriteLine("enumerable repeat:"+string.Join(',', sarray));
		Console.WriteLine("array exist ab: " + Array.Exists(sarray.ToArray(), x => x.StartsWith("ab")));
		var a1 = new int[1,2,3];
		Console.WriteLine("array rank: " + a1.Rank);
		var a2 = new int[2][][];
		Console.WriteLine("array rank: " + a2.Rank);
		var a3 = new int[3];
		Array.Fill(a3, Int32.MaxValue);
		Console.WriteLine("array fill: " + string.Join(',', a3));
		var a4 = new int[3]{1,2,3};
		Array.Resize(ref a4, a4.Length + 2);
		Console.WriteLine("array resize bigger: " + string.Join(',', a4));
		Array.Resize(ref a4, 2);
		Console.WriteLine("array resize smaller: " + string.Join(',', a4));
		
		var (l, s) = GetIt("aefbgc");
		Console.WriteLine("string length:"+l);
		var res = GetIt("aefbgc");
		Console.WriteLine(res.vs);
		var s1 = new string(s.OrderBy(a => a).ToArray());
		Console.WriteLine("orderby:" + s1);
		s1 = s1.Remove(1,1);
		Console.WriteLine("remove string at 1:"+s1);
		var s2 = new string(s.OrderByDescending(a => a).ToArray());
		Console.WriteLine("OrderByDescending:" + s2);
		var s3 = s.ToList();
		// list sort is in place vs linq orderby is new ienumerable
		s3.Sort((x, y) => x - y);
		Console.WriteLine("sort:" + new string(s3.ToArray()));
		
		// array (tuple) as key of dictionary
		Console.WriteLine("tuple as Dictionary key and value");
		var d = new Dictionary<(int,string), string>(){{(1,"11"), "abcd"}, {(2,"22"),"xyz"}};
		Console.WriteLine(d[(1,"11")]);
		Console.WriteLine(d.ContainsKey((2,"3")));
		Console.WriteLine(d.ContainsKey((1,"11")));
		foreach (var kv in d){
			Console.WriteLine("tuple as key of dict: "+ kv.Key.Item2);
		}
		
		var d2 = new Dictionary<(int, string), (string, int)>()
		{
			[(1,"11")] = ("11", 1),
			[(2, "22")] = ("22", 2)
		};
		Console.WriteLine(d2.ContainsValue(("22", 2)));
		foreach(var kv in d2) Console.WriteLine("tuple as dictionary key/value: " + kv.Key.Item1 + ":-" + kv.Value.Item1);
		
		var limitsLookup = new Dictionary<int, (int Min, int Max)>()
		{
			[2] = (4, 10),
			[4] = (10, 20),
			[6] = (0, 23)
		};

		if (limitsLookup.TryGetValue(4, out (int Min, int Max) limits))
			Console.WriteLine($"Found limits: min is {limits.Min}, max is {limits.Max}");
		
		var q = new Queue<(int, string)>();
		q.Enqueue((1,"11"));
		Console.WriteLine("tuple queue: " + q.Dequeue().Item2);
		
		var lst = new List<string>(){"a","b","v"};
		// Get index of list
		foreach (var (idx, v) in lst.Select((item, index) => (index, item + "@idx:" + index))) {
			Console.WriteLine("linq select idx: " + idx + ": " + v);
		}
		var lstSelect = lst.Select(x => x + "->select");
		Console.WriteLine("linq select: " + string.Join(',', lstSelect));
		var lstWhere = lst.Where(x => x.StartsWith("a"));
		Console.WriteLine("linq where: " + string.Join(',', lstWhere));
		var lst2 = new List<string>(lst);
		Console.WriteLine("linq SequenceEqual: " + lst.SequenceEqual(lst2));
		
		Console.WriteLine("array upper bound => idx: " + lst[lst.ToArray().GetUpperBound(0)]);
		Console.WriteLine("array lower bound => idx: " + lst[lst.ToArray().GetLowerBound(0)]);
		lst = lst.Prepend("0").ToList();
		Console.WriteLine("IEnumerable prepend: " + string.Join(',', lst));
		var lSet = lst.Prepend("0").ToHashSet();
		Console.WriteLine("linq ToHashSet: " + string.Join(',', lSet));
		
		Console.WriteLine("is type check: " + ("abc" is not null));
		
		double[] values = {7.03, 7.64, 0.12, -0.12, -7.1, -7.6};
		foreach (double v in values) Console.WriteLine($"{v}, {Math.Ceiling(v)}, {Math.Floor(v)}");
		
		// Func vs Action
		// Func return value, where Action does not
	}
	
	static (int ln, string vs) GetIt(string s) {
		return (s.Length, s);
	}
}
