using System;
using static System.Console;

public class main{
	
	public static void Main() {
		var list = new genlist<double[]>();
		char[] delimiters = {' ', '\t'};
		var options = StringSplitOptions.RemoveEmptyEntries;
		for(string line = ReadLine(); line != null; line = ReadLine()) {
			var words = line.Split(delimiters, options);
			int n = words.Length;
			var numbers = new double[n];
			for(int i=0; i<n; i++) numbers[i] = double.Parse(words[i]);
			list.push(numbers);
		}
		
		for(int i=0; i<list.size; i++) {
			var numbers = list.data[i];
			foreach(var number in numbers) Write($"{number:e} ");
			WriteLine();
		}
		genlist<int> intlist = new genlist<int>();
		intlist.push(2);
		intlist.push(5);
		intlist.push(7);
		intlist.push(8);
		for(int i=0; i<intlist.data.Length; i++)
			WriteLine($"index {Array.IndexOf(intlist.data, intlist.data[i])} has value {intlist.data[i]}");
		intlist.removeItem(1);
	}
}
