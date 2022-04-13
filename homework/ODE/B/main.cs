/*Testing our RKF45 ODE integrator. The example from the scipy.integrate.odeint manual (oscillator with friction) is
 *reproduced, and we test that the x- and y-values are stored in our generic lists when using RKF45.*/
using System;
using static System.Math;
using static System.Console;

public class main {

	public static void Main() {
		
		//reproducing the example from scipy.integrate.odeint manual
		double b=0.25, c=5;
		Func<double, vector, vector> pend = delegate(double t, vector y) {
			double θ=y[0], ω=y[1];
			return new vector(ω, -b*ω-c*Sin(θ));
		};
		//declaring variables 
		double start=0;
		double end=10;
		//choosing smaller values for acc and eps to get more points and smoothing out the graphs
		double h = 0.01;
		double acc = 1e-5;
		double eps = 1e-5;
		vector ystart = new vector(PI-0.1,0.1);
		//solving the odeint scipy example by using genlists
		var xlist = new genlist<double>();
		var ylist = new genlist<vector>();
		var ySol = RKF45.driver(pend, start, ystart, end, xlist, ylist, h, acc, eps);
		for(int i=0; i<xlist.size; i++) {
			WriteLine($"{xlist.data[i]} {ylist.data[i][0]} {ylist.data[i][1]}");
		}
	}
}
