/*In this program a linear fit is made of measurements of the radioactivity of the element called ThX back in 1902 when 
 *Rutherford and Soddy took the measurements. The element is now known as Ra-224 */
using System;
using System.Linq;
using static System.Console;
using static System.Math;

class main {

	public static void Main() {
		vector time = new vector("1,2,3,4,6,9,10,13,15");
		vector activity = new vector("117,100,88,72,53,29.5,25.2,15.2,11.1");
		vector activityErr = new vector("5,5,5,5,5,1,1,1,1");

		int n = activity.size;
		vector lnActivity = new vector(n);
		vector lnActivityErr = new vector(n);

		//radioactive decay follows exponential law, i.e. y(t)=a*exp(-λ*t), but we would like to adjust it so
		//that it becomes linear by taking the logarithm: ln(y)=ln(a)-λ*t. The uncertainties would then equally
		//change as δln(y) = δy/y.
		for(int i=0; i<n; i++) {
			lnActivity[i] = Log(activity[i]);
			lnActivityErr[i] = activityErr[i]/activity[i];
		}

		//the set of functions passed to least-squares fitting function. Since data is linear there is a 
		//constant and linear term
		var fs = new Func<double, double>[] {z => 1.0, z => z};

		//Making the fit
		var (c, Σ) = lsquares.lsfit(fs, time, lnActivity, lnActivityErr);
		
		double λerr = Sqrt(Σ[1,1]);
		double thalfErr = Abs((Log(2)/(c[1]*c[1]))*λerr);

		//The fit parameters is written to std error stream
		Error.WriteLine("\nThe fit is carried out but now as an extension the covariance-matrix is also cal-");
		Error.WriteLine("culated.");
		Error.WriteLine($"The Fit parameters remain: a = {c[0]} and λ = {c[1]}");
		Error.WriteLine("while the covariance matrix is given as:\n");
		Error.WriteLine($"{Σ[0,0]}	{Σ[0,1]}");
		Error.WriteLine($"{Σ[1,0]}	{Σ[1,1]}\n");
		Error.WriteLine($"The uncertainty on λ is thus ± {Sqrt(Σ[1,1])}, and since the half-life is given as");
		Error.WriteLine("ln(2)/λ, the uncertainty in the half-life is |ln(2)/λ^2*δλ|.");
		Error.WriteLine($"So from the fit, the half-life of Ra-224 is estimated to: {Log(2)/(-c[1])} ± {thalfErr} days");
		Error.WriteLine($"\nThe table value is 3.63 ± 0.23 days. Source: wikipedia.org/wiki/Isotopes_of_radium");
	}
}
