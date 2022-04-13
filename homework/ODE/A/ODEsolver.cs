/*An implementation of an adaptive step-size ODE integrator using an embeded Runge Kutta method with error estimates.
 *The Runge Kutta-Fehlberg method also known as RKF45 is used as the stepper, and the adaptive step-size driver will be
 *using this routine. */
using System;
using static System.Math;

public static class RKF45 {
	
	/*The RKF45 method as the stepper. The f parameter is the f from dy/dx = f(x,y), where y is a vector same as
	 *y'(x)=dy/dx and x is a double. The parameter x is the current value of the variable, and y(x) is the current
	 *value of the sought function, while h is the step to be taken. */
	public static (vector, vector) rkfstep45(Func<double, vector, vector> f, double x, vector y, double h) {
		//c-coefficients
		double[] cs = {1.0/4, 3.0/8, 12.0/13, 1.0, 1.0/2};

		//a-coefficients
		double[] acs = {1.0/4, 3.0/32, 9.0/32, 1932.0/2197, -7200.0/2197, 7296.0/2197, 439.0/216, -8.0,
			        3680.0/513, -845.0/4104, -8.0/27, 2.0, -3544.0/2565, 1859.0/4104, -11.0/40}; 
		
		//b-coefficients
		double[] bs = {16.0/135, 0, 6656.0/12825, 28561.0/56430, -9.0/50, 2.0/55};

		//b*-coefficients
		double[] bstars = {25.0/216, 0, 1408.0/2565, 2197.0/4104, -1.0/5, 0};
		
		//calculating k-vectors using equation 19 from ode.pdf, the coefficient values comes from RKF45's 
		//Butcher tableau 
		vector k1 = f(x,y);
		vector k2 = f(x + cs[0]*h, y + acs[0]*h*k1);
		vector k3 = f(x + cs[1]*h, y + acs[1]*h*k1 + acs[2]*h*k2);
		vector k4 = f(x + cs[2]*h, y + acs[3]*h*k1 + acs[4]*h*k2 + acs[5]*h*k3);
		vector k5 = f(x + cs[3]*h, y + acs[6]*h*k1 + acs[7]*h*k2 + acs[8]*h*k3 + acs[9]*h*k4);
		vector k6 = f(x + cs[4]*h, y + acs[10]*h*k1 + acs[11]*h*k2 + acs[12]*h*k3 + acs[13]*h*k4 + acs[14]*h*k5);
		vector[] ks = {k1, k2, k3, k4, k5, k6};

		//using equation 18 and 22 to get the error esitmate, equation 23
		var ys = y;
		var ystars = y;
		for(int i=0; i<ks.Length; i++) { 
			ys += h*bs[i]*ks[i];
			ystars += h*bstars[i]*ks[i];
		}
		vector errors = ys - ystars;

		return (ys, errors);
	}
	
	/*An adaptive step-size driver routine which advances the solution from a to b, by calling the rkfstep45
	 *routine with appropriate step sizes, keeping the specified relative, eps, and absolute, acc, precision.
         *f is from dy/dx=f(x,y), a is the start point, vector ya is the intial value, b is the end-point of the
	 *integration, h is the initial step-size, acc is the absolute accuracy goal and eps is the relative accuracy
	 *goal.*/
	public static vector driver(Func<double, vector, vector> f, double a, vector ya, double b, double h=0.01,
		         	    double acc=0.01, double eps=0.01) {
		if(a>b) throw new Exception("driver: the start point a should be lower than the end point b");
		double x = a;
		vector y = ya;
		do{
			if(x>=b) {return y;} //job done
			if(x+h>b) {h=b-x;} //last step should end at b
			var (yh, erv) = rkfstep45(f, x, y, h);
			double tol = Max(acc, yh.norm()*eps)*Sqrt(h/(b-a));
			double err = erv.norm();
			if(err<=tol) {x+=h; y=yh;} //accept step
			double power = 0.25;
			double safety = 0.95;
			h*= Min(Pow(tol/err, power)*safety, 2); //readjust step-size
		}while(true);
	}
}
