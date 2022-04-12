/*To showcase the Jacobi algorithm works, this class will generate a random symmetric matrix A which will be 
 *eigenvalue decomposed by the Jacobi algorithm, such that  A = V*D*V^T, where V is the orthogonal matrix of eigen-
 *vectors and D is the diagonal matrix with the corresponding eigenvalues. Then we test that V^T*A*V = D, V*D*V^T = A,
 *V^T*V = I and V*V^T = I.*/
using System;
using static System.Console;

class main{

	public static void Main() {
		// Generating real symmetric matrix A
		int n = 4;
		matrix A = new matrix(n,n);
		var rnd = new Random();
		// random 4 times 4 matrix
		for(int i=0; i<n; i++) {
			for(int j=0; j<=i; j++) {
				A[i,j] = 10*rnd.NextDouble();
				A[j,i] = A[i,j];
			}
		}

		A.print("\n random symmetric matrix A:");
		//save copy of A before its diagonalized
		matrix Acopy = A.copy();
		(vector e, matrix V) = Jevd.JacobiAlg(A);
		matrix D = A.copy();
		D.print("\n A is diagonalized:");
		WriteLine("\n The vector e with A's eigenvalues and matrix V consisting of eigenvectors for A:");
		e.print("\n e:");
		V.print("\n V:");
		
		//Now testing that V^T*A*V is equal to D
		matrix VTAV = V.T*Acopy*V;
		VTAV.print("\n V^T*A*V should give D:");
		if(VTAV.approx(D)) WriteLine("\n test passed");
		else WriteLine("\n test failed");
		
		//Testing that V*D*V^T is equal to A
		matrix VDVT = V*D*V.T;
		VDVT.print("\n V*D*V^T should give A:");
		if(VDVT.approx(Acopy)) WriteLine("\n test passed");
		else WriteLine("\n test failed");

		//Testing that V^T*V is equal to the identity matrix I, and V*V^T is equal to I
		matrix VTV = V.T*V;
		matrix VVT = V*V.T;
		WriteLine("\n V^T*V and V*V^T should give the identity matrix I:");
		VTV.print("\n V^T*V:");
		VVT.print("\n V*V^T:");
		matrix I = new matrix(n,n);
		I.set_unity();
		if(VTV.approx(I) && VVT.approx(I)) WriteLine("\n test passed");
		else WriteLine("\n test failed");

		WriteLine($"\n {1/2} {1/2.0}");
		WriteLine($"\n {2/(3*Math.Sqrt(3))} {2.0/(3*Math.Sqrt(3))}");
	}
}
