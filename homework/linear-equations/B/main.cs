/*A class for testing the QR decomposition and that the Q and R matrices solves the equation QRx=b */
using System;
using static System.Console;

class main {
	
	public static void Main() {
		int n = 4;
		//testing inversion method of matrices
		var A = new matrix(n, n);
		var rnd = new Random();
		//random square 4 times 4 matrix 
		for(int i=0; i<n; i++) {
			for(int j=0; j<n; j++) {
				A[i,j] = 7*rnd.NextDouble();
			}
		}
		WriteLine("\n");
		A.print("random square matrix A:");
		Write("\n");
		QRGS qrA = new QRGS(A);
		matrix B = qrA.inverse();
		B.print("QRGS decomposition: inverse matrix of A\n");
		Write("\n");
		var AB = A*B;
		AB.print("checking A*B equals the identity matrix:\n");
		var I = new matrix(n,n);
		I.set_identity();
		if(AB.approx(I)) WriteLine("test passed");
		else WriteLine("test failed");
	}
}
