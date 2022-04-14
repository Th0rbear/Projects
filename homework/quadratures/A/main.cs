/*Testing the implementation of our numerical integrator om some different integrals. Furthermore the integrator is
 *is used to implement the error function via its integral representation. */
using System;
using static System.Console;
using static System.Math;

public class main {

	public static void Main() {
		Func<double, double>[] funcs = {
			x => Sqrt(x),
			x => 1/Sqrt(x),
			x => 4*Sqrt(1-x*x),
			x => Log(x)/Sqrt(x)
		};
		double a = 0;
		double b = 1;
		WriteLine("Evaluating integrals	of the following functions from 0 to 1: \n Sqrt(x) \n 1/Sqrt(x)" + "\n 4*Sqrt(1-x^2) \n ln(x)/Sqrt(x)");
		WriteLine("Should give 2/3, 2, π and -4");
		for(int i=0; i<funcs.Length; i++) WriteLine($"\n {Integrate.quad(funcs[i], a, b)}");
			
		using(var outfile = new System.IO.StreamWriter("erf.txt")) {
			for(double x=-3; x<=3; x+=1.0/18) outfile.WriteLine($"{x} {erf(x)}");
		}
	}
	
	/*Method for implementing error function via its integral repsresentation*/
	public static double erf(double z) {
		Func<double, double> f = x => Exp(-x*x);
		double result = Integrate.quad(f:f, a:0, b:z);
		return result*2/Sqrt(PI);
	}
}
