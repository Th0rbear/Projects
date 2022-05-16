/*The bound states of the hydrogen atom will be treated with the shooting method for boundary value problems. */
using System;
using static System.Console;
using static System.Math;

public class main {

	/*The function that finds the solution to the Schrödinger equation */
	public static double F_ε(double r, double E, double r_min, double acc=1e-5, double eps=1e-5, bool exact=false) {
		if(r < r_min) return r - r*r;
		//s-wave radial Schrödinger equation for the hydrogen atom (in units "Bohr radius" and "Hartree")
		//-(1/2)*f'' - (1/r)*f = E*f
		//f'' = -2*E*f - (2/r)*f
		Func<double, vector, vector> schrödingerEq = (x, f) => {
		if(exact) {
			return new vector(f[1], -2*x*Exp(-x)*(E + 1/x)); //inserting f0 = r*Exp(-r)
		}
		else {
			return new vector(f[1], -2*f[0]*(E + 1/x));
		}};
		//When solving the Schrödinger equation with our ODE-solver an initial condition is needed
		vector y_init = new vector(r_min - r_min*r_min, 1 - 2*r_min);
		vector y_sol = RKF45.driver(schrödingerEq, r_min, y_init, r, acc: acc, eps: eps);
		return y_sol[0];
	}
	
	/*This function will use the shooting method i.e. finding the negative root of the equation M(ε) = 0, where
	 *M(ε) = F_ε(r_max). This will estimate the energy for the s-wave function.*/
	public static double shootingMethod(double r_min, double r_max, double acc=1e-5, double eps=1e-5, bool exact=false) {
		Func<vector, vector> M = (vector v) => {
			double E = v[0];
			double f_max = F_ε(r_max, E, r_min, acc, eps, exact);
			return new vector(f_max);
		};
		
		//Now we find the root to the auxiliary function M(ε), using the root-finding routine from A
		vector v0 = new vector(-1.0); //an initial guess
		vector sol = Roots.newton(M, v0);
		double ε0 = sol[0];
		return ε0;
	}

	public static void Main() {
		//The Coulomb potential diverges at zero so r_min as the boundary condition should be chosen close to
		//zero. The upper boundary cannot be infinity so we choose it to be kinda far compared to hydrogens
		//dimensions, i.e. 10 Bohr radii.
		double r_min = 1e-4;
		double r_max = 8;

		//The lowest energy solution for hydrogen, should be -0.5 Hartree
		double E0 = shootingMethod(r_min, r_max);
		WriteLine($"The groundstate energy for hydrogen is estimated to:	{E0} Hartree");
		WriteLine("It should be: 						-0.5 Hartree");
		
		//The resulting wave function is plotted and compared with the exact result
		using(var outfile = new System.IO.StreamWriter("hydrogen.txt")) {
			for(double r=0; r<r_max; r+=1.0/32) {
				outfile.WriteLine($"{r} {F_ε(r, E0, r_min, 1e-5, 1e-5, false)} {r*Exp(-r)} {F_ε(r, -0.5, r_min, 1e-5, 1e-5, true)}"); 
			}
			//inserting ε0 and f0 = r*Exp(-r) in to F_ε gives pretty much the same as just r*Exp(-r)
		}
		
		//examining the convergence of the solution towards the exact result with respect to r_max and r_min,
		//and as well as with respect to the parameters acc and eps of the ODE-solver
		using(var outfile = new System.IO.StreamWriter("convergence.txt")) {
			for(double r_max2 = 0.6; r_max2 <= 10; r_max2 += 1.0/10) {
				double E = shootingMethod(r_min, r_max2);
				outfile.WriteLine($"{r_max2} {E} {-0.5}");
			}
			outfile.WriteLine("\n");

			for(double r_min2 = 0.5; r_min2 >= r_min; r_min2 -= 1.0/200) {
				double E2 = shootingMethod(r_min2, r_max);
				outfile.WriteLine($"{r_min2} {E2} {-0.5}");
			}
		}
		
		//now examining convergence with respect to the parameters acc and eps for the ODE-solver, standard
		//values for acc and eps is 1*10^-3
		using(var outfile = new System.IO.StreamWriter("convergence-ODE.txt")) {
			for(double acc = 0.01; acc >= 1e-8; acc-=1e-4) {
				double E3 = shootingMethod(r_min, r_max, acc: acc, eps:1e-3);
				outfile.WriteLine($"{acc} {E3} {-0.5}");
			}
			outfile.WriteLine("\n");
			
			for(double eps = 0.01; eps >= 1e-8; eps-=1e-4) {
				double E4 = shootingMethod(r_min, r_max, acc: 1e-3, eps: eps);
				outfile.WriteLine($"{eps} {E4} {-0.5}");
			}	
		}
		//What is quite interesting about this is that the energy value determined does not improve
		//significantly when eps and acc gets lower. When changing acc and eps both approach -0.5,
		//but with eps the solution approaches an asymptotic value, while for acc the solution keeps decreasing
		//even below -0.5. In this current regime, there seems to be an optimal value for both acc and eps 
	}
}
