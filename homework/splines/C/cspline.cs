/*Cubic spline is a piecewise polynomial of third order used to construct a graph with a set of data points.
 * This kind of interpolation is more accurate compared to quadratic spline, because when encountering discontinuities
 * like a step function the wiggle behaviour is toned down. */
using System;
using System.Diagnostics;

public class cspline {
	
	/*Field variables: arrays of x-data, y-data, b, c and d coefficients */
	private double[] xs, ys, bs, cs, ds;

	/*binary search algorithm */
	public static int binsearch(double[] xs, double x) {
		int i=0, j=xs.Length-1;
		while(j-i>1) {
			int mid = (i+j)/2;
			if(x > xs[mid]) i=mid; else j=mid;
		}
		return i;
	}

	/*constructor: storing x-data and y-data and calculates b, c and d */
	public cspline(double[] xs, double[] ys) {
		int n=xs.Length; Trace.Assert(ys.Length>=n);
		this.xs = xs;
		this.ys = ys;
		bs = new double[n];
		cs = new double[n-1];
		ds = new double[n-1];
		
		//determining constants
		var D = new double[n];
		var Q = new double[n-1];
		var B = new double[n];
		var h = new double[n-1];
		var p = new double[n-1];
		for(int i=0; i<n-1; i++) {
			h[i] = xs[i+1]-xs[i]; Trace.Assert(h[i]>0);
			p[i] = (ys[i+1]-ys[i])/h[i];
		}
		
		D[0]=2; Q[0]=1; B[0]=3*p[0]; D[n-1]=2; B[n-1]=3*p[n-2];
		for(int i=0; i<n-2; i++) {
			D[i+1] = 2*h[i]/h[i+1]+2;
			Q[i+1] = h[i]/h[i+1];
			B[i+1] = 3*(p[i] + p[i+1]*h[i]/h[i+1]);
		}

		for(int i=1; i<n; i++) {
			D[i]-=Q[i-1]/D[i-1];
			B[i]-=B[i-1]/D[i-1];
		}

		//determining coefficients
		bs[n-1]=B[n-1]/D[n-1];
		for(int i=n-2; i>=0; i--) {
			bs[i]=(B[i]-Q[i]*bs[i+1])/D[i];
		}

		for(int i=0; i<n-1; i++) {
			cs[i] = (-2*bs[i]-bs[i+1]+3*p[i])/h[i];
			ds[i] = (bs[i] + bs[i+1] -2*p[i])/h[i]/h[i];
		}
	}

	/* Makes the cubic spline at a given point x */
	public double eval(double x) {
		Trace.Assert(x>=xs[0] && x<=xs[xs.Length-1]);
		int i=binsearch(xs, x);
		double dx = x - xs[i];
		//calculate the interpolating spline:
		return ys[i] + dx*(bs[i] + dx*(cs[i] + dx*ds[i]));
	}
	
	/* derivative of the cubic spline at given point x */ 
	public double deriv(double x) {
		Trace.Assert(x>=xs[0] && x<=xs[xs.Length-1]);
		int i = binsearch(xs, x);
		double dx = x - xs[i];
		return bs[i] + dx*(2*cs[i] + dx*3*ds[i]);
	}

	/*Evaluates the integral of the cubic spline at given point x */
	public double integ(double x) {
		Trace.Assert(x>=xs[0] && x<=xs[xs.Length-1]);
		int ix = binsearch(xs, x);
		double sum=0, dx=0;
		for(int i=0; i<ix; i++) {
			dx = xs[i+1] - xs[i];
			sum += dx*(ys[i] + dx*(bs[i]/2 + dx*(cs[i]/3 + dx*ds[i]/4)));
		}
		dx = x - xs[ix];
		sum += dx*(ys[ix] + dx*(bs[ix]/2 + dx*(cs[ix]/3 + dx*ds[ix]/4)));
		return sum;
	}
}
