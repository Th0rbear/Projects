/*The implementation of quadratic splines is tested here */
using System;
using static System.Console;
using static System.Math;
using System.Collections.Generic; /* ables you to use generic Lists */
using System.Linq; /* ables you to use Min and Max method for arrays */

class main {
	
	public static void Main(){
		/*Some tables of x-data and y-data is created to see if the correct b's and c's are created. */
		double[] xdata = {-5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5}; //add some extra negativ x-values to make
		//graph symmetric
		double[] ydata1 = {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};
		double[] ydata2 = {25, 16, 9, 4, 1, 0, 1, 4, 9, 16, 25};

		/*Checking qspline interpolation with the given x-data and y-data */
		var ysList = new List<double[]> {ydata1, xdata, ydata2};
		
		/*For every y-data print the b's, c's, xdata, ydata, the quadratic splines, the derivative of the
		 * qsplines and integral of the qsplines  */
		foreach(var ys in ysList) {
			qspline qs = new qspline(xdata, ys);

			/*printing b's and c's to std output stream */
			qs.print();
			
			/*printing x-data and y-data to std error stream */	
			for(int i=0; i<ys.Length; i++) {
				Error.WriteLine($"{xdata[i]} {ys[i]}");
			}
			
			Error.WriteLine("\n");

			/*printing qsplines their derivatives and integrals */
			for(double x=xdata.Min(); x<xdata.Max(); x+=1.0/3) {
				double qinterp = qs.eval(x);
				double deriv = qs.derivative(x);
				double integ = qs.integrate(x);
				Error.WriteLine($"{x} {qinterp} {deriv} {integ}");
			}

			Error.WriteLine("\n");
		}
	}
}
