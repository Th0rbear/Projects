/*This class will test the implementation of the plain Monte Carlo integrator and the quasi-random Monte Carlo integra-
 *tor. By calculating some two-dimensional integrals and a difficult singular three-dimensional integral, the errors
 *of the two integrators are compared. */
using System;
using static System.Math;
using static System.Console;

public class main {

	public static void Main() {
		int N = (int) 1e5;
		Func<vector, double> f = x => 2*x[0]*x[1]*Sin(x[1])/((1+Pow(x[0],4))*(1+Pow(Cos(x[1]),2)));
		vector a = new vector(0, 0);
		vector b = new vector(PI, PI);
		(double plainRes, double plainErr) = Mcintegrate.plainmc(f,a,b,N);
		(double quasiRes, double quasiErr) = Mcintegrate.quasimc(f,a,b,N);
		//calculating the area of a circle with radius 0.9
		Func<vector, double> circle = x => x[0];
		double r = 0.9;
		vector b2 = new vector(r, 2*PI);
		(double cPlainRes, double cPlainErr) = Mcintegrate.plainmc(circle,a,b2,N);
		(double cQuasiRes, double cQuasiErr) = Mcintegrate.quasimc(circle,a,b2,N);
		WriteLine("\nCalculating the two dimensional integral of f(x,y)=2*x*y*sin(y)*[(1+x^4)*(1+(cos(y))^2)]^-1,\nfrom 0 to π with respect to x and y:");
		WriteLine("The integral should give: 3.62663, according to Wolfram");
		WriteLine($"\nPseudo-random MC:	{N}		result = {plainRes}		error = {plainErr}");
		WriteLine($"Quasi-random MC:	{N}		result = {quasiRes}		error = {quasiErr}");
		WriteLine("\nCalculating the area of a circle with radius r = 0.9:");
		WriteLine($"Should give {PI*r*r}");
		WriteLine($"\nPseudo-random MC:	{N}		result = {cPlainRes}		error = {cPlainErr}");
		WriteLine($"Quasi-random MC:	{N}		result = {cQuasiRes}		error = {cQuasiErr}");
		
		//We try to calculate a difficult three dimensional singular integral
		Func<vector, double> f3 = x => 1/(Pow(PI,3)*(1-Cos(x[0])*Cos(x[1])*Cos(x[2])));
		int M = (int) 8e6;
		vector a3 = new vector(0, 0, 0);
		vector b3 = new vector(PI, PI, PI);
		(double plainRes3, double plainErr3) = Mcintegrate.plainmc(f3,a3,b3,M);
		(double quasiRes3, double quasiErr3) = Mcintegrate.quasimc(f3,a3,b3,M);
		WriteLine("\nCalculating the three dimensional integral of f(x,y,z) = pi^-3*[(1-cos(x)cos(y)cos(z))^-1]");
		WriteLine($"Should give {(Pow(gammafun.gamma(1.0/4),4))/(4*Pow(PI,3))}");
		WriteLine($"\nPseudo-random MC:	{M}		result = {plainRes3}		error = {plainErr3}");
		WriteLine($"Quasi-random MC:	{M}		result = {quasiRes3}		error = {quasiErr3}");

		//Comparing the scaling of the error with the two Monte Carlo integrators, using 2d integral
		int Na = 1000;
		using(var outfile = new System.IO.StreamWriter("error-scaling.txt")) {
			for(int i=Na; i<N; i += Na/10) {
				var plain_res = Mcintegrate.plainmc(f,a,b,i);
				var quasi_res = Mcintegrate.quasimc(f,a,b,i);
				
				outfile.WriteLine($"{i} {plain_res.Item2} {quasi_res.Item2}");
			}
		}
	}
}
