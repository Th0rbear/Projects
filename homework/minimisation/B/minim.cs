/*An implementation of the quasi-Newton minimization method with numerical gradient, back-tracking linesearch, and a
 *rank-1 update.*/
using System;
using static System.Math;

public static class Minim{
	
	/*A variation of the Newton's method. The function φ is the objective function, x0 is the starting point and
	 *acc is the accuracy goal, where on exit the norm of the gradient should be less than acc. The function
	 *returns the minima and the number of steps for the algorithm to reach it/them */
	public static (vector, int) qnewton(Func<vector,double> φ, vector x0, double acc=1e-2) {
		int n = x0.size;
		int steps = 0;
		double eps = Pow(2,-26);
			
		vector x = x0.copy();
		double φx = φ(x);
		vector gx = gradient(φ,x);

		//the inverse Hessian matrix denoted B
		matrix B = new matrix(n,n);
		B.set_identity();
		
		int maxsteps = 20000; //we allow only a finite number of steps
		while(acc < gx.norm() && steps < maxsteps) {
			steps++;
			vector Δx = -B*gx; //eq. 6 in minimization note, also called Newton's step

			//backtracking line search
			double φxs, λ = 1.0;
			vector xs = new vector(n);
			while(true) {
				xs = x + λ*Δx;
				φxs = φ(xs);
				if(φxs < φx) break; //the step is accepted
				if(λ < eps) {
					B.set_identity();
					break;
					//if λ is too small the step is accepted and B is resat to identity
				}
				λ /= 2;
			}
			vector gxs = gradient(φ,xs);
			vector y = gxs - gx;         //below eq. 12
			vector u = λ*Δx - B*y;       //below eq. 12

			//Symmetric-rank-1 update see eq. 18 and below
			double uTy = u.dot(y);
			
			if(Abs(uTy) > 1e-6) {
				B.update(u, u, 1/uTy); //recalculates every entry as B[i,j] += u[i]*u[j]*(1/uTy)
			}
			x = xs;
			gx = gxs;
			φx = φxs;
		}
		return (x, steps);
	}

	/*An auxiliary method for calculating the gradient numerically. It is a bit like how the Jacobian matrix was
	 *calculated in roots A.*/
	public static vector gradient(Func<vector,double> φ, vector x) {
		int n = x.size;
		vector g = new vector(n);
		double φx = φ(x);
		double eps = Pow(2,-26);

		for(int i = 0; i < n; i++) {
			double δx = Abs(x[i])*eps;
			if(Abs(x[i]) < eps) δx = eps;
			vector xδx = x.copy();
			xδx[i] += δx;
			g[i] = (φ(xδx) - φx)/δx;
		}
		return g;	
	}
}
