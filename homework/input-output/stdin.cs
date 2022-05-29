//Program that reads a sequence of numbers, separated by characters such as blanks (' '), tabs ('\t'), and newline
//('\n'), from the standard input and print these numbers together with their sines and cosines to the standard output
using static System.Console;
using static System.Math;
class main{
	public static void Main() {
		WriteLine("\nPart A:\n");
		char[] delimiters = {' ', '\t', '\n'};
		for(string line = ReadLine(); line != null; line = ReadLine()) {
			var words = line.Split(delimiters, System.StringSplitOptions.RemoveEmptyEntries);
			foreach(var word in words) {
				double x = double.Parse(word); // convert a string to a number
				WriteLine($"{x} {Sin(x)} {Cos(x)}");
			}	
		}
	}
}
