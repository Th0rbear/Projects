/*Implementation of Monte Carlo integration or cubature (multidimensional integrals). Unlike the numerical integrator
 *in quadratures the abscissas (special points) are chosen randomly. In this version a quasi-random (low discrepancy)
 *sequence is also implemented */
using System;
using static System.Math;
using static System.Double;

public static class Mcintegrate {
	
	/*pseudo-random Monte-Carlo integrator */
	public static (double, double) plainmc(Func<vector, double> f, vector a, vector b, int N) {
		if(!(a.size == b.size)) throw new Exception("dimension of a and b should be the same");
		
		int dim = a.size;
		double V = 1;
		for(int i=0; i<dim; i++) V*=b[i]-a[i];
		double sum = 0;
		double sum2 = 0;
		var x = new vector(dim);
		var rnd = new Random();
		for(int i=0; i<N; i++) {
			for(int j=0; j<dim; j++) {
				x[j] = a[j] + rnd.NextDouble()*(b[j]-a[j]);
			}
			double fx = f(x);
			sum += fx;
			sum2 += fx*fx;
		}
		double mean = sum/N;
		double σ = Sqrt(sum2/N - mean*mean);
		var result = (mean*V, σ*V/Sqrt(N));
		return result;
	}

	/*generating qausi-random low discrepancy points */
	/*The van der Corput sequence is low discrepancy sequence */
	public static double corput(int n, int Base) {
		double q = 0;
		double bk = 1.0/Base;
		while(n>0) {
			q += (n % Base)*bk;
			n /= Base;
			bk /= Base;
		}
		return q;
	}

	/*using the Halton sequence, a generalization of the van der Corput sequence */
	public static vector halton(int n, int d, bool reversed=false) {
		int[] Base = {2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67};
		if(d > Base.Length) throw new Exception("Dimension size is too big");

		vector x = new vector(d);
		for(int i=0; i<d; i++) { 
			if(reversed) {
				Array.Reverse(Base);
				x[i] = corput(n, Base[i]);
			}
			else {
				x[i] = corput(n, Base[i]);
			}
		}
		return x;
	}

	/*Multidimensional Monte-Carlo integrator using quasi-random sequences */
	public static (double, double) quasimc(Func<vector, double> f, vector a, vector b, int N) {
		if(!(a.size == b.size)) throw new Exception("dimension of a and b should be the same");
		
		int dim = a.size;
		double V = 1;
		for(int i=0; i<dim; i++) V *= b[i] - a[i];
		double sum = 0;
		double sum2 = 0;
		
		//Creating two different sequences to estimate error
		vector x = new vector(dim);
		vector x2 = new vector(dim);

		for(int i=0; i<N; i++) {
			vector halton1 = halton(i, dim);
			vector halton2 = halton(i, dim, true);
			for(int j=0; j<dim; j++) {
				x[j] = a[j] + halton1[j]*(b[j] - a[j]);
				x2[j] = a[j] + halton2[j]*(b[j] - a[j]);
			}
			double fx = f(x);
			double fx2 = f(x2);

			if(IsNaN(fx) || IsInfinity(fx) || IsNaN(fx2) || IsInfinity(fx2)) i++;
			else {
				sum += f(x);
				sum2 += f(x2);
			}
		}
		double mean = sum/N;
		double mean2 = sum2/N;
		var result = (mean*V, Abs((mean - mean2)*V));
		return result;
	}
}
