/*Ordinary least-squares fit by QR-decomposition */
using System;

public static class lsquares{
	
	/*Function that makes a least-squares fit of a given data set using QR decomposition. The arguments to the
	 *function is the data to fit, and the set of functions. The function returns a vector with the approximated
	 *coefficients and a matrix, which is the covariance matrix.  */
	public static (vector, matrix) lsfit(Func<double,double>[] fs, vector x, vector y, vector dy) {
		if(!(x.size == y.size && x.size == dy.size && y.size == dy.size)) throw new Exception("x, y and y-err should have same dimensions");
		
		int n = x.size;
		int m = fs.Length;
		var A = new matrix(n,m);
		var b = new vector(n);
		for(int i=0; i<n; i++) {
			b[i] = y[i]/dy[i];
			for(int k=0; k<m; k++) {
				A[i,k] = fs[k](x[i])/dy[i];
			}
		}
		//decomposition of matrix A to QR
		QRGS qrA = new QRGS(A);
		//the least squares solution, or the resulting coefficients for the fitting function
		vector c = qrA.solve(b);
		//calculating Covariance matrix
		matrix invR = (new QRGS(qrA.GetR())).inverse();
		matrix Σ = invR*invR.transpose();
		return (c, Σ);
	}
}
