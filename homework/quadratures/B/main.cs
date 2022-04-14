/*Testing some integrals with integrable divergencies at the end-points of the intervals. The number of integrand
 *evaluations is recorded and is compared with the ordinary integrator without variable transformation */
using System;
using static System.Console;
using static System.Math;

public class main {
	private static int ncalls;

	public static void Main() {
		double a = 0;
		double b = 1;
		ncalls = 0;
		Func<double, double> invSqrt = delegate(double x) {
			ncalls++;
			return 1/Sqrt(x);
		};
		Func<double, double> logInvSqrt = delegate(double t) {
			ncalls++;
			return Log(t)/Sqrt(t);
		};
		double ordRes1 = Integrate.quad(invSqrt, a, b);
		WriteLine("evaluation of integral 1/Sqrt(x):");
		WriteLine("\nOrdinary:");
		reset(ordRes1);

		double transRes1 = Integrate.quadClenCurt(invSqrt, a, b);
		WriteLine("\nTransf.:		");
		reset(transRes1);
		
		WriteLine("\n\nevaluation of integral ln(x)/Sqrt(x):");
		
		double ordRes2 = Integrate.quad(logInvSqrt, a, b);
		WriteLine("\nOrdinary:         ");
		reset(ordRes2);

		double transRes2 = Integrate.quadClenCurt(logInvSqrt, a, b);
		WriteLine("\nTransf.:            ");
		reset(transRes2);
		Write("\n");
		//evaluating with ordinary integration
		/*
		int i=0;
	       	int j=0;
		Func<double, double> invSqrt = x => {i++; return 1/Sqrt(x);};
		Func<double, double> logInvSqrt = x => {j++; return Log(x)/Sqrt(x);};
		double ordRes1 = Integrate.quad(invSqrt, a, b);
		double ordRes2 = Integrate.quad(logInvSqrt, a, b);

		//evaluating using Clenshaw-Curtis variable transformation
		int k=0;
		int m=0;
		Func<double, double> vt_invSqrt = x => {k++; return 1/Sqrt(x);};
		Func<double, double> vt_logInvSqrt = x => {m++; return Log(x)/Sqrt(x);};
		double transRes1 = Integrate.quadClenCurt(vt_invSqrt, a, b);
		double transRes2 = Integrate.quadClenCurt(vt_logInvSqrt, a, b);
		*/
		/*
		WriteLine("evaluation of integral 1/Sqrt(x):");
		WriteLine($"\nOrdinary:		result = {ordRes1}	#calls = {ncalls}");
		WriteLine($"Transf.:		result = {transRes1}	#calls = {ncalls}");
		WriteLine("\n evaluation of integral ln(x)/Sqrt(x):");
		WriteLine($"\nOrdinary:		result = {ordRes2}	#calls = {ncalls}");
		WriteLine($"Transf.:		result = {transRes2}	#calls = {ncalls}");*/
	}	
	
	public static void reset(double res) {
		Write($"result = {res}	#calls = {ncalls}");
		ncalls=0;
	}
}
