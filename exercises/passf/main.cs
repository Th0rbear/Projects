using System;
using static System.Console;
class main{ 
	
	public static void Main(){
		Write("\n");	
		double k=1,a=-1,b=1,dx=1.0/4;
		Func<double, double> sin = delegate(double x) {return Math.Sin(k*x);};
		k=1;
		WriteLine("making table with f(x)=sin(x) with k=1\n");
		table.make_table(sin, a, b, dx);
		WriteLine("---------------------------------------");
		k=2;
		WriteLine("making table with f(x)=sin(2x) with k=2\n");
		table.make_table(sin, a, b, dx);
		WriteLine("---------------------------------------");
		k=3;
		WriteLine("making table with f(x)=sin(3x) with k=3\n");
		table.make_table(sin, a, b, dx);
	}
}
