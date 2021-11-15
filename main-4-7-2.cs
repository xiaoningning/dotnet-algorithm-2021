using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
	public static void Main()
	{
		// value type vs reference type:
		// value type stores in stack, holds value directly. no need to garage collection. live in methods
		// reference type refers a memory location, stores in managed heap, holds a reference to memory location
		// simple type or structure type or enum or tuple type is basically user defined value type
		// value type: int, byte, long, uint, ulong, sbyte, bool, char, decimal, enum
		// nullable value type: int?, char?, bool?
		// reference type: class, interface, delegate, array, record are user defined reference type, 
		// string/object/dynamic is a built-in reference type
		// delegate: Action<T> => void, Func<T,T> => return
		
		// c# datetime
		var tlst = new List<DateTime>();
		var x1 = DateTime.Parse("2020-11-02T14:00:00Z");
		var x2 = DateTime.Parse("2020-11-02T14:10:00Z");
		// DateTimeOffset.UnixEpoch not in c# 4.7.2
		// var timeSeconds = (x1 - DateTimeOffset.UnixEpoch).TotalSeconds;
		// Console.WriteLine($"time span to unix epoch: {timeSeconds}");

		tlst.Add(DateTime.Parse("2020-11-02T13:00:00Z"));
		tlst.Add(DateTime.Parse("2020-11-02T14:10:00Z"));
		tlst.Add(DateTime.Parse("2020-11-02T14:02:00Z"));
		tlst.Add(DateTime.Now);
		Console.WriteLine("time list: " + string.Join(",", tlst));
		var ttt = tlst.OrderByDescending(x => x);
		// string.Join() must be ",", but not ','!!!
		Console.WriteLine("time list order: " + string.Join(",", ttt));
		tlst.Sort((x, y) => -x.CompareTo(y));
		Console.WriteLine("time compare" + string.Join(",", tlst));
		Console.WriteLine("time span: {0}", x1 - x2);
		Console.WriteLine("time span in seconds: {0}", (x1 - x2).TotalSeconds);

		var switchCases = new string[] { "case1", "case2", "case3", "case4", "case5" };
		Console.WriteLine("c# switch case:");
		foreach (string sc in switchCases)
		{
		    switch (sc)
		    {
			case "case1":
			    Console.WriteLine(sc);
			    break;
			case "case2":
			    Console.WriteLine(sc);
			    break;
			case "case3":
			case "case4":
			    Console.WriteLine(sc);
			    break;
			default:
			    Console.WriteLine("default {0}", sc);
			    break;
		    }
		}
		
		// Array is reference type.  Clone or assign does overwrite the refered one.
		// only copy is to create a new objec
		var g1 = new int[1][];
		g1[0] = new int[]{2};
		var gg1 = new int[1][];
		gg1[0] = new int[1];
		for(int i = 0; i < g1.Length; i++) g1[i].CopyTo(gg1[i], 0); // deep copy
		Console.WriteLine(gg1[0][0]);
		g1[0][0] = 4;
		Console.WriteLine("a new array object, no change: {0}", gg1[0][0]);
		// gg1 = g1; // clone =: reference type
		gg1 = (int[][])g1.Clone(); // a shadow copy
		Console.WriteLine(gg1[0][0]);
		g1[0][0] = 8;
		Console.WriteLine("a cloned array object, it changes!!!: {0}", gg1[0][0]);
		
		Console.WriteLine("|"+"axc".Substring(3)+"|");
		Console.WriteLine("axc".Substring(3) == "");
		var st1 = new HashSet<List<int>>();
		st1.Add(new List<int>(){1,2});
		st1.Add(new List<int>(){1,2});
		Console.WriteLine("hash set list cnt: " + st1.Count);
		var st2 = new HashSet<int[]>();
		st2.Add(new int[]{1,2});
		st2.Add(new int[]{1,2});
		Console.WriteLine("hash set array cnt: " + st2.Count);
		Console.WriteLine("axc".CompareTo("abe"));
		Console.WriteLine("binary shift " + Convert.ToString(1 << 0 | 1 << 1 | 1 << 2, 2));
		Console.WriteLine("n = 6 state: " + Convert.ToString((1 << 6) - 1, 2));
		Console.WriteLine(1 << 6);
		Console.WriteLine(Math.Pow(2,6));
		Console.WriteLine(Convert.ToString(7, 2));
		Console.WriteLine(Convert.ToString(-7, 2));
		Console.WriteLine(Convert.ToString(7 & -7, 2));
		Console.WriteLine(Convert.ToString(4, 2));
		Console.WriteLine(Convert.ToString(3, 2));
		Console.WriteLine(Convert.ToString(4 ^ (4 & 3), 2));
		Console.WriteLine((int)Math.Sqrt(6));
		
		var lst20 = new List<int>();
		lst20.InsertRange(0, new List<int>(){2,2});
		Console.WriteLine(string.Join("|", lst20));
		var lst1 = new List<int[]>();
		lst1.Add(new int[]{1,2,-4});
		lst1.Add(new int[]{3,5,8});
		lst1.Add(new int[]{1,5,4});
		lst1.Sort((x,y) => {
			if (x[0] == y[0]) return y[2] == x[2] ? (y[1] - x[1]) : (y[2] - x[2]);
			else return x[0] - y[0];
		});
		var tl = lst1.Select(x => string.Join(",", x) );
		Console.WriteLine(string.Join("|", tl));
		var intarray = Enumerable.Range(1,5);	
		Console.WriteLine("enumerable range:"+string.Join(",", intarray));
		var sarray = Enumerable.Repeat("a", 5);
		Console.WriteLine("enumerable repeat:"+string.Join(",", sarray));
		Console.WriteLine("array exist ab: " + Array.Exists(sarray.ToArray(), x => x.StartsWith("ab")));
		var a1 = new int[1,2,3];
		Console.WriteLine("array rank: " + a1.Rank);
		var a2 = new int[2][][];
		Console.WriteLine("array rank: " + a2.Rank);
		var a3 = new int[3];
		//Array.Fill(a3, Int32.MaxValue);
		Console.WriteLine("array fill NOT in 4.7.2: " + string.Join(",", a3));
		var a4 = new int[3]{1,2,3};
		Array.Resize(ref a4, a4.Length + 2);
		Console.WriteLine("array resize bigger: " + string.Join(",", a4));
		Array.Resize(ref a4, 2);
		Console.WriteLine("array resize smaller: " + string.Join(",", a4));
		
		var s = "xcba";
		var s1 = new string(s.OrderBy(a => a).ToArray());
		Console.WriteLine("string orderby:" + s1);
		s1 = s1.Remove(1,1);
		Console.WriteLine("remove string at 1:"+s1);
		var s2 = new string(s.OrderByDescending(a => a).ToArray());
		Console.WriteLine("OrderByDescending:" + s2);
		var s3 = s.ToList();
		// list sort is in place vs linq orderby is new ienumerable
		s3.Sort((x, y) => x - y);
		Console.WriteLine("sort:" + new string(s3.ToArray()));
		
		// array (tuple) as key of dictionary
		Console.WriteLine("c# 4.7.2 tuple as Dictionary key and value");
		var d = new Dictionary<Tuple<int,string>, string>(){{Tuple.Create(1,"11"), "abcd"}, {Tuple.Create(2,"22"),"xyz"}};
		Console.WriteLine(d[Tuple.Create(1,"11")]);
		Console.WriteLine(d.ContainsKey(Tuple.Create(2,"3")));
		// Console.WriteLine(d.ContainsKey((1,"11")));
		foreach (var kv in d){
			Console.WriteLine("tuple as key of dict: "+ kv.Key.Item2);
		}
		/**
		// c# 4.7.2 does not support init dictionary as [key] = val
		var d2 = new Dictionary<Tuple<int,string>, Tuple<string, int>>()
		{
			[Tuple.Create(1,"11")] = Tuple.Create("11", 1),
			[Tuple.Create(2, "22")] = Tuple.Create("22", 2)
		};
		Console.WriteLine(d2.ContainsValue(Tuple.Create("22", 2)));
		foreach(var kv in d2) Console.WriteLine("tuple as dictionary key/value: " + kv.Key.Item1 + ":-" + kv.Value.Item1);
		
		var limitsLookup = new Dictionary<int, (int Min, int Max)>()
		{
			[2] = (4, 10),
			[4] = (10, 20),
			[6] = (0, 23)
		};

		if (limitsLookup.TryGetValue(4, out (int Min, int Max) limits))
			Console.WriteLine("Found limits: min is {0}, max is {1}", limits.Min, limits.Max);
		*/
		
		/**
		var q = new Queue<(int, string)>();
		q.Enqueue((1,"11"));
		Console.WriteLine("tuple queue: " + q.Dequeue().Item2);
		*/
		var lst = new List<string>(){"a","b", "ba", "bc", "v"};
		
		// Get index of list
		foreach (var res in lst.Select((item, index) => Tuple.Create(index, item + "@idx:" + index))) {
			Console.WriteLine("linq select idx: " + res.Item1 + ": " + res.Item2);
		}
		
		var lstSelect = lst.Select(x => x.StartsWith("b"));
		Console.WriteLine("linq select: " + string.Join(",", lstSelect));
		var lstWhere = lst.Where(x => x.StartsWith("b"));
		Console.WriteLine("linq where: " + string.Join(",", lstWhere));
		var lst2 = new List<string>(lst);
		Console.WriteLine("linq SequenceEqual: " + lst.SequenceEqual(lst2));
		
		Console.WriteLine("array upper bound => idx: " + lst[lst.ToArray().GetUpperBound(0)]);
		Console.WriteLine("array lower bound => idx: " + lst[lst.ToArray().GetLowerBound(0)]);
		lst = lst.Prepend("0").ToList();
		Console.WriteLine("IEnumerable prepend: " + string.Join(",", lst));
		var lSet = lst.Prepend("0").ToHashSet();
		Console.WriteLine("linq ToHashSet: " + string.Join(",", lSet));
		
		Console.WriteLine("is type check: " + ("abc" != null).ToString());
		Console.WriteLine("bool false to int: " + Convert.ToInt32(false));
		Console.WriteLine("bool true to int: " + Convert.ToInt32(true));
		
		double[] values = {7.03, 7.64, 0.12, -0.12, -7.1, -7.6};
		foreach (double v in values) Console.WriteLine("{0},  ceiling: {1}, floor: {2}", v, Math.Ceiling(v), Math.Floor(v));
		
		// c# 4.7.2 does not support tuple (T1,T2), but support system.tuple new Tuple<string,int>("a", 1);
		// var (int, string) x = (0, "a");
		var t1 = Tuple.Create("a", 1);
		Console.WriteLine("system.tuple in c# 4.7.2 : {0}", t1.Item1);
		var t2 = Tuple.Create<string, int>("a", 1);
		Console.WriteLine("system.tuple in c# 4.7.2 : {0}", t2.Item2);
		var tst1 = new HashSet<Tuple<string,int>>();
		tst1.Add(t2);
		tst1.Add(t1);
		Console.WriteLine("system.tuple in c# 4.7.2 : HashSet<Tuple<string,int>> {0}", tst1.Contains(t1));
		Console.WriteLine("system.tuple in c# 4.7.2 : HashSet<Tuple<string,int>> {0}", tst1.Count);
		var xstr = "a";
		// Console.WriteLine($"{xstr}"); // c# 4.7.2 does not support string $"{x}"
		Console.WriteLine("{0}", xstr);
		
		var td1 = new Dictionary<Tuple<string,int>, List<Tuple<int, string>>>();
		td1[Tuple.Create("a", 1)] = new List<Tuple<int,string>>(){Tuple.Create(1, "a")};
		Console.WriteLine("system.tuple in c# 4.7.2 : Dictionary<Tuple<string,int>, List<Tuple<int, string>>> {0}", td1.Count);
		Console.WriteLine("system.tuple in c# 4.7.2 : Dictionary<Tuple<string,int>, List<Tuple<int, string>>> {0}", td1.ContainsKey(Tuple.Create("b", 2)));
		Console.WriteLine("system.tuple in c# 4.7.2 : Dictionary<Tuple<string,int>, List<Tuple<int, string>>> {0}", td1[Tuple.Create("a", 1)].First().Item2);

		var tlst1 = new List<Tuple<int, string>>();
		tlst1.Add(Tuple.Create(1, "a"));
		tlst1.Add(Tuple.Create(2, "c"));
		tlst1.Add(Tuple.Create(2, "a"));
		tlst1.Sort((x, y) => x.Item1 == y.Item1 ? x.Item2.CompareTo(y.Item2) : x.Item1 - y.Item1);
		Console.WriteLine("list tuple sort at {0} val: {1}", tlst1[1].Item1, tlst1[1].Item2);
		Console.WriteLine("list tuple sort at {0} val: {1}", tlst1[2].Item1, tlst1[2].Item2);
		
		// Func vs Action
		// Func return value, where Action does not
		Func<int, int> f1 = (x) => x * 10;
		Console.WriteLine("{0}", f1(3));
		
		Func<string, string> f2 = null; 
		f2 = (x) => { 
			return x + " -> f2";
		};
		Console.WriteLine("{0}", f2("abc"));
		
		var oStr = "";
		Action<string> af1 = (x) => oStr += x + "|f1|";
		af1("abc");
		Console.WriteLine("{0}", oStr);
		
		Action<string> af2 = null;
		af2 = (x) => { 
			oStr += x + "|f2|";
		};
		af2("abc");
		Console.WriteLine("{0}", oStr);
		
		Func<Tuple<string, int>, Tuple<int,string>> f3 = null;
		f3 = (x) => { 
			return Tuple.Create<int,string>(x.Item1.Length, x.Item1 + "|f3|");
		};
		Console.WriteLine("c# 4.7.2 system.tuple in Func f3 return tuple as well: {0} {1}", f3(Tuple.Create("abc", 1)).Item1, f3(Tuple.Create("abc", 1)).Item2);
		
		var tfo1 = GetIt("tfo1");
		Console.WriteLine("c# 4.7.2 systme.tuple: {0} {1}", tfo1.Item1, tfo1.Item2);
		
		var strFm = string.Format("{0}-{1}", "abc", 123);
		Console.WriteLine("c# 4.7.2 only support {{}} : {0}", strFm);
		
		var ahs1 = new int[]{1,2};
		var hs = new HashSet<int[]>(){new int[]{1,2}};
		Console.WriteLine("hashset<int[]> should be false  since object hash and array is reference type {0}", hs.Contains(ahs1));
	}
	
	static Tuple<int, string> GetIt(string s) {
		return Tuple.Create(s.Length, s);
	}
	/**
	// c# 4.7.2 does not support tuple (T1,T2), but support system.tuple new Tuple<string,int>("a", 1);
	static (int, string) GetIt(string s) {
		return (s.Length, s);
	}
	*/
}

