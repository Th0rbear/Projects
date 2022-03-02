using System;
class main{ public static void Main(){
		double k=1,a=-1,b=1,dx=1.0/4;
		Func<double, double> sin = delegate(double x) {return Math.Sin(k*x);};
		k=1;
		table.make_table(sin, a, b, dx);
		Console.WriteLine("---------");
		k=2;
		table.make_table(sin, a, b, dx);
		Console.WriteLine("---------");
		k=3;
		table.make_table(sin, a, b, dx);
	}
}
