/*A modification on the homework root-finding method. This is used for variational method with Lagrange multipliers.
 *Here the rootfinding algorithm will be customized to use an analytic Jacobian matrix instead of using finite dif-
 *ferences.*/
using System;
using static System.Math;

public static class Roots{
	
	/*The function for finding the root in the n+1 dimensional space, with x={v,λ}. It returns x, i.e.
	 *the eigenvalue and the corresponding eigenvector. The symmetric real matrix A, whose eigenvalue and 
       	 *eigenvector we would like to know is included as an argument. Furthermore the arguments include: 
	 *f, the n+1 dimensional vector-function, x0, the n+1 dimensional vector which functions as an ini-
	 *tial guess, and eps, which acts as an accuracy goal.*/
	public static vector newton(matrix A, Func<vector,vector> f, vector x0, double eps=1e-2) {
		int n = x0.size;
		vector x = x0.copy();
		vector fx = new vector(x0);

		while(true) {
			fx = f(x);
			matrix J = analyticJ(A, x);
			QRGS Jd = new QRGS(J);		//J diagonalized
			vector Δx = Jd.solve(-fx);	//solving the linear equation JΔx = -f(x)

			//backtracking line search
			double λ = 1.0;
			vector u = new vector(n);
			vector fΔx = new vector(n);
			while(true) {
				λ /= 2;
				u = x + λ*Δx;
				fΔx = f(u);
				if(fΔx.norm()<(1 - λ/2)*fx.norm() || λ<1.0/32) break; 
			}	
			x = u;
			fx = fΔx;
			if(fx.norm()<eps) break; //infinite loop stops if condition is true
			//condition with δx is omitted, since we don't use finite differences
		}
		return x;
	}

	/*Method that returns the analytic Jacobian matrix. The parameters include a real symmetric n times n matrix, A,
	 *and the vector x with n+1 components.*/
	public static matrix analyticJ(matrix A, vector x) {
		int n = A.size1;
		matrix J = new matrix(n+1, n+1); //we have a n+1 vector function
		vector v = new vector(n);
		for(int id=0; id<n; id++) v[id] = x[id];
		
		for(int i=0; i<n; i++) {
			for(int j=0; j<n; j++) {
				if(i==j) { 
					J[i,j] = A[i,j] - x[n]; //x[n] corresponds to the lagrange multiplier λ
				}
				else {
					J[i,j] = A[i,j];
				}
			}
		} //All entries in the matrix except the n+1 row and column

		for(int i=0; i<n; i++) {
			J[i,n] = -v[i]; //the column where the vector function is differentiated with respect to λ
			J[n,i] = 2*v[i]; //the row where the n+1 f is differetiated with respect to variabl v_1 to v_n
		}
		J[n,n] = 0; //the last diagonal element is zero.
		return J;
	}
}
