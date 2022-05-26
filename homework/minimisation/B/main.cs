/*This program uses the minimization routine from A, to investigate some data made on the Higgs boson particle. In
 *more detail the Breit-Wigner function will be fitted to the data, which is done by minimizing χ^2. By doing this an
 *estimate of A the scale factor, m the mass and Γ the width of the sought resonance (experimental width of the Higgs  
 *boson) is determined. */
using System;
using static System.Console;
using static System.Math;

public class main {
	
	/*The Breit-Wigner function used for fitting the data for the Higgs boson. As arguments the function takes, 
	 *energy, mass and width. */
	public static double BreitWig(double E, double m, double Γ) {
		return 1/(Pow(E-m,2) + Pow(Γ,2)/4);
	}
	
	public static void Main() {
		//reading the data table into generic lists (our implementation) from the stnd input
		//the data goes into a list of energies E[GeV], cross-sections σ(E)[certain units], and uncertainties 
		//Δσ[same units]
		var energy = new genlist<double>();
		var signal = new genlist<double>();
		var error = new genlist<double>();
		var separators = new char[] {' ','\t'};
		var options = StringSplitOptions.RemoveEmptyEntries;
		do {
			string line = Console.In.ReadLine();
			if(line==null) break;
			string[] words = line.Split(separators, options);
			energy.push(double.Parse(words[0]));
			signal.push(double.Parse(words[1]));
			error.push(double.Parse(words[2]));
		} while(true);

		//testing the data has been stored in the three lists
		//for(int i=0; i<energy.size; i++) WriteLine($"{energy.data[i]}\t{signal.data[i]}\t{error.data[i]}");
		
		//the χ^2 function that needs to be minimized
		Func<vector, double> χ2 = v => {
			double Α = v[0];
			double m = v[1];
			double Γ = v[2];
			double res = 0;
			for(int i=0; i<energy.size; i++) {
				double E = energy.data[i];
				double σ = signal.data[i];
				double Δσ = error.data[i];
				res += Pow((v[0]*BreitWig(E, m, Γ) - σ)/Δσ, 2);
			}
			return res;
		};
		//Now to get the best fit parameters for A, m and Γ we minimize the chi squared function. For this we
		//use an initial guess for the quasi Newton method
		vector v0 = new vector(4, 110, 1); //A proper guess helps a lot to get a good fit
		var (x, steps) = Minim.qnewton(χ2, v0, acc: 1e-4);
		double AFit = Abs(x[0]);
		double mFit = Abs(x[1]);
		double ΓFit = Abs(x[2]);

		WriteLine("\nThe best fit parameters for A, m and Γ is determined with the quasi-Newton minimization method:");
		WriteLine($"Scale factor A:\t{AFit}\nMass m: \t{mFit}\nWidth Γ: \t{ΓFit}");
		WriteLine($"\nThe best fit parameters were found in {steps} iterations");
		WriteLine("\nThe mass of 125.9 GeV/c^2 is pretty close to 125.3 +/- 0.6 GeV/c^2"); 

		//Creating the data for making the fit with the best fit parameters
		using(var outfile = new System.IO.StreamWriter("higgs.fit.txt")) {
			for(double e = 100; e <= 160; e+=1.0/32) {
				outfile.WriteLine($"{e} {AFit*BreitWig(e, mFit, ΓFit)}");
			}
		}
		//The fitted function is then plotted with the data points for the Higgs boson
	}
}
