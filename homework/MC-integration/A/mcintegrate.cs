/*Implementation of Monte Carlo integration or cubature (multidimensional integrals). Unlike the numerical integrator
 *in quadratures the abscissas (special points) are chosen randomly. */
using System;
using static System.Math;

public static class Mcintegrate {
	private static Random rnd;

	public static (double, double) plainmc(Func<vector, double> f, vector a, vector b, int N) {
		if(!(a.size == b.size)) throw new Exception("dimension of a and b should be the same");
		int dim = a.size;
		double V = 1;
		for(int i=0; i<dim; i++) V*=b[i]-a[i];
		double sum = 0;
		double sum2 = 0;
		var x = new vector(dim);
		rnd = new Random();
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
}
