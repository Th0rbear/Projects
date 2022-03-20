/*Quadratic spline is a piecewise polynomial of second order used to construct a graph with a set of data points.
Note that quadratic spline is not used for practical applications. One of the reasons is at discontinuities it has
a wiggly behaviour. */
using System;
using static System.Console;
using static System.Math;

public class qspline {
	
	/*field variables, arrays of x-data y-data, b and c coefficients */
	private double[] xs, ys, bs, cs;
	
	/*constructor storing x-data, y-data and calculates b and c */
	public qspline(double[] xs, double[] ys) {
		this.xs = xs;
		this.ys = ys;
		int n = xs.Length;
		this.bs = new double[n-1];
		this.cs = new double[n-1];

		//determining constants
		double[] ps = new double[n-1];
		double[] dxs = new double[n-1];
		for(int i=0; i<n-1; i++) {
			dxs[i] = xs[i+1] - xs[i];
			ps[i] = (ys[i+1] - ys[i])/dxs[i];
		}

		//calculate coefficient b and c. Practically it's best to do a forward and backwards recursion of the
		//c's and then averaging the backward and forward c's.
		//recursion forward
		double[] csF = new double[n-1];
		csF[0] = 0;
		for(int i=0; i<n-2; i++) {
			csF[i+1] = (ps[i+1] - ps[i] - csF[i]*dxs[i])/dxs[i+1];
		}

		//recursion backwards
		double[] csB = new double[n-1];
		csB[n-2] = 0;
		for(int i=n-3; i>=0; i--) {
			csB[i] = (ps[i+1] - ps[i] - csB[i+1]*dxs[i+1])/dxs[i];
		}

		//the resulting c coefficients
		for(int i=0; i<n-1; i++) {
			cs[i] = (csF[i] + csB[i])/2;
		}

		//the b coefficients
		for(int i=0; i<n-1; i++) {
			bs[i] = ps[i] - cs[i]*dxs[i];
		}
	}

	/*A print method that prints the b's and c's in table form */
	public void print() {
		for(int i=0; i<this.xs.Length-1; i++) {
			WriteLine($"{this.bs[i]} {this.cs[i]}");
		}
		WriteLine("\n");
	}

	/*Makes quadratic spline at a given point x */
	public double eval(double x) {
		int i = binsearch(xs, x);
		double h = x - xs[i];
		return ys[i] + bs[i]*h + cs[i]*h*h;
	}

	/*Evaluates the derivative of the quadratic spline at a given point x */
	public double derivative(double x) {
		int i = binsearch(xs, x);
		double h = x - xs[i];
		return bs[i] + 2*cs[i]*h;
	}

	/*Evaluates the integral of the quadratic spline at a given point x */
	public double integrate(double x) {
		//similar implementation as in linspline
		double totalsum = 0, dx = 0;
		int id = binsearch(xs, x);
		for(int i=0; i<id; i++) {
			dx = xs[i+1] - xs[i];
			totalsum += ys[i]*dx + 0.5*bs[i]*Pow(dx, 2) + 1.0/3*cs[i]*Pow(dx, 3);
		}
		dx = x - xs[id];
		totalsum += ys[id]*dx + 0.5*bs[id]*Pow(dx, 2) + 1.0/3*cs[id]*Pow(dx, 3);
		return totalsum;

	}

	/* binary search algorithm */
	public static int binsearch(double[] xs, double x) {
		int i=0, j=xs.Length-1;
		while(j-i > 1) {
			int mid = (i+j)/2;
			if(x > xs[mid]) i=mid; else j=mid;
		}
		return i;
	}

}
