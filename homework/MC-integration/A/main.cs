/*This class will test the implementation of the plain Monte Carlo multi-dimensional integration. Some interesting two-
 *dimensional integrals will be calculated, and an attempt to calculate a difficult singular three-dimensional integral
 *will be done. */
using System;
using static System.Math;
using static System.Console;

public class main {

	public static void Main() {
		int N = (int) 1e6;
		Func<vector, double> f = x => 2*x[0]*x[1]*Sin(x[1])/((1+Pow(x[0],4))*(1+Pow(Cos(x[1]),2)));
		vector a = new vector(0, 0);
		vector b = new vector(PI, PI);
		(double res, double err) = Mcintegrate.plainmc(f,a,b,N);
		//calculating the area of a circle with radius 0.9
		Func<vector, double> circle = x => x[0];
		double r = 0.9;
		vector b2 = new vector(r, 2*PI);
		(double circRes, double circErr) = Mcintegrate.plainmc(circle,a,b2,N);
		WriteLine("\nCalculating the two dimensional integral of f(x,y)=2*x*y*sin(y)*[(1+x^4)*(1+(cos(y))^2)]^-1,\nfrom 0 to π with respect to x and y:");
		WriteLine("The integral should give: 3.62663, according to Wolfram");
		WriteLine($"\n{N}		result = {res}		error = {err}");
		WriteLine("\nCalculating the area of a circle with radius r = 0.9:");
		WriteLine($"Should give {PI*r*r}");
		WriteLine($"\n{N}		result = {circRes}	error = {circErr}");
		
		//We try to calculate a difficult three dimensional singular integral
		Func<vector, double> f3 = x => 1/(Pow(PI,3)*(1-Cos(x[0])*Cos(x[1])*Cos(x[2])));
		vector a3 = new vector(0, 0, 0);
		vector b3 = new vector(PI, PI, PI);
		(double res3, double err3) = Mcintegrate.plainmc(f3,a3,b3,N);
		WriteLine("\nCalculating the three dimensional integral of f(x,y,z) = pi^-3*[(1-cos(x)cos(y)cos(z))^-1]");
		WriteLine($"Should give {(Pow(gammafun.gamma(1.0/4),4))/(4*Pow(PI,3))}");
		WriteLine($"\n{N}		result = {res3}		error = {err3}");
	}
}
