/*This implementation of a numerical integrator will include a recursive open-quadrature adaptive integrator that 
 *estimates the integral of a given function f(x) on a given interval [a,b] with the required absolute, δ, or relative,
 *ε, accuracy goals. The integrator has to use a combination of a higher order quadrature and an embedded lower order
 *quadrature (to estimate the local error). */
using System;
using static System.Math;
using static System.Double;

public static class Integrate {
	
	/*See section 1.5 adaptive quadratures in integration.pdf*/
	public static double quad(Func<double, double> f, double a, double b, double δ=1e-3, double ε=1e-3,
			          double f2=NaN, double f3=NaN) {
		double h=b-a;
		//using reusable points per equation (48)
		if(IsNaN(f2)) {
			f2 = f(a + 2*h/6);
			f3 = f(a + 4*h/6);
		} 
		double f1 = f(a + h/6);
		double f4 = f(a + 5*h/6);
		double Q = (2*f1 + f2 + f3 + 2*f4)/6*h; // equation (44)+(49)+(51) 
		double q = (f1 + f2 + f3 + f4)/4*h;     // equation (45)+(50)+(51)
		double err = Abs(Q-q);                  // equation (46)
		double tol = δ + ε*Abs(Q);              // equation (47)
		if(err <= tol) return Q;
		else {
			double Q1 = quad(f, a, (a+b)/2, δ/Sqrt(2), ε, f1, f2);
			double Q2 = quad(f, (a+b)/2, b, δ/Sqrt(2), ε, f3, f4);
			return Q1 + Q2;
		}
		
	}

	/*Method that implements Clenshaw-Curtis variable transformation for the above qaudrature routine. */
	public static double quadClenCurt(Func<double, double> f, double a, double b, double δ=1e-3, double ε=1e-3,
					  double f2=NaN, double f3=NaN) {
		Func<double, double> substitute;
		//equation (58)
		if(a==-1 && b==1) substitute = t => f(Cos(t))*Sin(t);
		else substitute = t => f((a+b)/2 + (b-a)/2*Cos(t))*Sin(t)*(b-a)/2;
		return quad(substitute, 0, PI);
	}
}
