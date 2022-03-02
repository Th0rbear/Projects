//A class which is meant to print a table to the standard output using a function with the variable x
using System;
using static System.Console;
public static class table{

	public static void make_table(Func<double, double> f, double a, double b, double dx) {
		for(double x=a; x<=b; x+=dx) { 
			WriteLine($"{x} {f(x)}");
		}	
	}	
} 
