/*The subroutine that calculates the eigenvalue (close to a given value λ-initial) and the corresponding eigenvector
 *of a given symmetric real matrix A by searching for the root of the function f(v,λ) = {Av-λv, v^(T)v-1}, using
 *the components of the vector v and the Lagrangian multiplier λ as free parameters.*/

using System;

public static class LagrangeMult {

	public static (double, vector) search(matrix A, double λinit) {
		int n = A.size1;
		Func<vector, vector> f = delegate(vector x) {
			vector vecf = new vector(n+1);
			vector v = new vector(n);
			for(int i=0; i<n; i++) v[i] = x[i];
			vector Avλv = A*v - x[n]*v;
			for(int j=0; j<n; j++) vecf[j] = Avλv[j];	//the components up to the n'th component for f
			vecf[n] = v.dot(v)-1;				//the n+1 component for the vector f

			return vecf;
		};

		vector x0 = new vector(n+1); 				//the inital guess for the root-finding routine
		for(int k=0; k<n; k++) x0[k] = 1;
		x0[n] = λinit;
		vector sol = Roots.newton(A, f, x0, eps: 1e-9);
		double eigenval = sol[n];
		vector eigenvec = new vector(n);
		for(int m=0; m<n; m++) eigenvec[m] = sol[m];

		return (eigenval, eigenvec);
	}
}
