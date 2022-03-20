/* Testing cubic spline implementation with sine function and comparing with the plot pyxplot makes */
using System;
using static System.Console;
using static System.Math;

class main {

	public static void Main() {
		
		int n=5, N=200;
		double[] xdata = new double[n];
		double[] ydata = new double[n];
		
		int i;
		for(i=0; i<n; i++) {
			xdata[i] = 2*PI*i/(n-1);
			ydata[i] = Sin(xdata[i]);
			WriteLine("{0:g6} {1:g6}", xdata[i], ydata[i]);
		}

		WriteLine("\n");

		var cs = new cspline(xdata, ydata);
		double x = 0, step = (xdata[n-1]-xdata[0])/(N-1);
		for(x = xdata[0], i=0; i<N; x = xdata[0] + (++i)*step) {
			WriteLine($"{x} {Sin(x)} {cs.eval(x)}");
		}
		WriteLine("\n");
		for(x = xdata[0], i=0; i<N; x = xdata[0] + (++i)*step) {
			WriteLine($"{x} {Cos(x)} {cs.deriv(x)}");
		}
		WriteLine("\n");
		for(x = xdata[0], i=0; i<N; x = xdata[0] + (++i)*step) {
			WriteLine($"{x} {1-Cos(x)} {cs.integ(x)}");
		}
	}
}
