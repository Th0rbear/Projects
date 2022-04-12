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

		for(int i=0; i<time.size; i++) {
			WriteLine($"{time[i]} {activity[i]} {activityErr[i]}");
		}
		WriteLine("\n");

		//radioactive decay follows exponential law, i.e. y(t)=a*exp(-λ*t), but we would like to 
		//adjust it so that it becomes linear by taking the logarithm: ln(y)=ln(a)-λ*t. 
		//The uncertainties would then equally change as δln(y) = δy/y.
		for(int i=0; i<activity.size; i++) {
			activity[i] = Log(activity[i]);
			activityErr[i] = activityErr[i]/activity[i];
			WriteLine($"{time[i]} {activity[i]} {activityErr[i]}");
		}
		WriteLine("\n");

		//the set of functions passed to least-squares fitting function. Since data is linear 
		//there is a constant and linear term
		var fs = new Func<double, double>[] {z => 1.0, z => z};

		//Making the fit
		//var c = lsquares.lsfit(fs, time, activity, activityErr).Item1;
		//var Σ = lsquares.lsfit(fs, time, activity, activityErr).Item2;
		(vector c, matrix Σ) = lsquares.lsfit(fs, time, activity, activityErr);
		
		double[] tArr = time;
		double res = 0; 
		double maxRes = 0;  
		double minRes = 0;
		for(double t=0; t<=tArr.Max(); t+=1.0/5) {
			res = 0;
			maxRes = 0; 
			minRes = 0;
			for(int k=0; k<fs.Length; k++) {
				res += c[k]*fs[k](t); 
				maxRes += (c[k] + Sqrt(Σ[k,k]))*fs[k](t);
				minRes += (c[k] - Sqrt(Σ[k,k]))*fs[k](t);
			}
			//res = res;
			WriteLine($"{t} {res} {maxRes} {minRes}");
		}
		WriteLine("\n");
	}
}
