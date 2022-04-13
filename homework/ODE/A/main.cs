/*Testing our RKF45 ODE integrator. First the well known ordinary differential equation u''=-u (the harmonic oscillator)
 *will be tested, and secondly the example from the scipy.integrate.odeint manual (oscillator with friction) is also 
 *reproduced.*/
using System;
using static System.Math;
using static System.Console;

public class main {

	public static void Main() {
		//solving u''=-u for the initial values y0=(1,0)
		double step = 1.0/18;
		vector yinit = new vector(1,0);
		Func<double, vector, vector> harmo = delegate(double t, vector y) {
			return new vector(y[1], -y[0]);
		};
		using(var outfile = new System.IO.StreamWriter("oscillator.txt")) {
			for(double t=0; t<=10.0; t+=step) {
				vector ySol = RKF45.driver(harmo, t, yinit, t+step);
				outfile.WriteLine($"{t} {ySol[0]} {ySol[1]}");
				yinit = ySol;
			}
		}
		//reproducing the example from scipy.integrate.odeint manual
		double b=0.25, c=5;
		Func<double, vector, vector> pend = delegate(double t, vector y) {
			double θ=y[0], ω=y[1];
			return new vector(ω, -b*ω-c*Sin(θ));
		};
		vector ystart = new vector(PI-0.1,0.1);
		for(double t=0; t<=10; t+=step) {
			vector ySol = RKF45.driver(pend, t, ystart, t+step);
			WriteLine($"{t} {ySol[0]} {ySol[1]}");
			ystart = ySol;
		}
	}
}
