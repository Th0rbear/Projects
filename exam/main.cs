/*The following program will test the implementation of the method with Lagrange multipliers that uses our modified
 *rootfinding routine. The test is carried out by creating a random symmetric real matrix and determining its eigen-
 *vector and eigenvalue.*/

using System;
using static System.Console;
using static System.Math;

public class main {
	
	public static void Main() {
		int n = 4;
		matrix A = new matrix(n, n);
		var rnd = new Random();
		for(int i=0; i<n; i++) {
			for(int j=0; j<n; j++) {
				A[i,j] = 9*rnd.NextDouble();
				A[j,i] = A[i,j];		//ensures that the matrix is symmetric
			}
		}
		WriteLine("Creating a random real symmetric 4x4 matrix:");
		A.print("");
		WriteLine("\nNow searching for its eigenvalue and eigenvector");
		double λinit = 1;
		(double λ, vector eigenvec) = LagrangeMult.search(A, λinit);
		WriteLine($"Using an initial guess on {λinit} for the eigenvalue we get:");
		WriteLine($"Eigenvalue λ = {λ}");
		WriteLine("The corresponding eigenvector is:");
		eigenvec.print("\n");
		WriteLine("\nNow testing that the following constraint v^Tv = 1 is fulfilled:");
		double vTv = eigenvec.dot(eigenvec);
		WriteLine($"v^Tv = {vTv}");
		//using machine epsilon to test if v^Tv is approximately equal to 1
		if(machineepsilon.approx(vTv, 1.0, tau: 1e-9, epsilon: 1e-9)) WriteLine("test passed");
		else WriteLine("test failed");
		WriteLine("\nAs a last thing we can also test the eigenvalue equation for matrix A, i.e. (A-λI)v = 0");
		matrix B = new matrix(n, n);
		B.set_identity();
		vector zerovec = new vector(n);
		vector AλIv = (A-λ*B)*eigenvec;
		WriteLine("(A-λI)v:\n");
		AλIv.print("");
		if(AλIv.approx(zerovec)) WriteLine("test passed");
		else WriteLine("test failed");
	}
}
