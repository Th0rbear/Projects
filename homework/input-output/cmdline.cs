//Program that reads a set of numbers from the command-line and prints these numbers together with their sines and
//cosines to the standard output
using static System.Console;
using static System.Math;
class main {
	public static void Main(string[] args) {
		foreach(var arg in args) {
			double x = double.Parse(arg);
			WriteLine($"{x} {Sin(x)} {Cos(x)}");
		}
	}
}
