/*An implementation of Newton-Raphson method, i.e. a root-finding algorithm. Root finding is a problem of finding a set
 *of n variables which statisfy a system of n non linear equations, in matrix notation it is written as f(x)=0. */
using System;
using static System.Math;

public static class Roots{
	
	public static vector newton(Func<vector,vector> f, vector x0, double eps=1e-2) {
		int n = x0.size;
		double δx = 1.0;
		vector x = x0.copy();
		matrix J = new matrix(n,n);
		vector fx = new vector(x0);

		while(true) {
			//calculating the Jacobian matrix J
			fx = f(x);
			for(int i=0; i<n; i++) {
				δx = Abs(x[i])*Pow(2,-26);
				for(int k=0; k<n; k++) {
					vector xδx = x.copy();
					xδx[k] += δx;
					J[i,k] = (f(xδx)[i] - fx[i])/δx; //equation 7 in roots.pdf
				}
			}
			//Diagonalize J to solve the linear system JΔx = -f(x)
			QRGS Jd = new QRGS(J);
			vector Δx = Jd.solve(-fx);

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
			if(fx.norm()<eps || Δx.norm()<δx) break; //infinite loop stops if condition is true
		}
		return x;
	}
}
