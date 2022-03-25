/*A class for testing the QR decomposition and that the Q and R matrices solves the equation QRx=b */
using System;
using static System.Console;

class main {
	
	public static void Main() {
		int n = 4, m=3;
		matrix A = new matrix(n,m);
		A[0] = new double[4] {0, 6, 6, 9}; //declares the values for the first column vector
		A[1] = new double[4] {7, 9, 1, 4};
		A[2] = new double[4] {3, 7, 2, 1};	
		A.print("A =");
		QRGS Ad = new QRGS(A);
		Write("\n");

		//printing R to see if it is upper triangular
		matrix R = Ad.GetR();
		matrix Q = Ad.GetQ();
		WriteLine("The Q & R matrices from decomposition of A:");
		R.print("R = ");
		Q.print("Q = ");
		
		//checking that Q^TQ gives the identity matrix
		matrix I = Q.transpose()*Q;
		Write("\n");
		WriteLine("Q transposed times Q should give the identity matrix:");
		I.print("I = ");
		matrix Id = new matrix(m,m);
		Id.set_identity(); //makes it a unit matrix
		if(I.approx(Id)) WriteLine("test passed");
		else WriteLine("test failed");

		//checking Q*R equals A
		Write("\n");
		WriteLine("Q times R should be A:");
		matrix A2 = Q*R;
		A2.print("A = ");
		if(A2.approx(A)) WriteLine("test passed");
		else WriteLine("test failed");

		//testing method for solving linear equation QRx=b
		var B = new matrix(n, n);
		var rnd = new Random(); //lets us create random numbers
		//random square 4 times 4 matrix 
		for(int i=0; i<n; i++) {
			for(int j=0; j<n; j++) {
				B[i,j] = 7*rnd.NextDouble();
			}
		}
		WriteLine("\n");
		B.print("random square matrix A:");
		Write("\n");
		QRGS qrB = new QRGS(B);
		//random vector b
		var b = new vector(n);
		for(int i=0; i<b.size; i++) {
			b[i] = 3*rnd.NextDouble();
		}
		b.print("random vector b:\n");
		Write("\n");
		var x = qrB.solve(b);
		x.print("QRGS decomposition: solution x to system A*x=b:\n");
		Write("\n");
		var Bx = B*x;
		Bx.print("checking A*x equals b:\n");
		if(Bx.approx(b)) WriteLine("test passed");
		else WriteLine("test failed");
	}
}
