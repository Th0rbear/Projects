//A table of x-data and y-data will be created with the sine function, with this a plot will be made 
//with the linear interpolation class. Furthermore the integration will be tested aswell.
using System;
using static System.Console;
using static System.Math;

class main{
	
	public static void Main(){
		
		//create x- and y-data
		int n = 5;
		double inc = 1.0/3;
		double[] xdata = new double[n*3];
		double[] ydata = new double[n*3];
		int id = 0;
		for(double x=0; x < n-inc; x+=inc) {
			xdata[id] = x;
			ydata[id] = Sin(x);
			id++;
		}

		//print the x-data and y-data to std output stream
		for(int i=0; i<xdata.Length; i++) {
			WriteLine($"{xdata[i]} {ydata[i]}");
		}

		//data for the plots and printed to std error stream
		double step = 1.0/20;
		for(double x=0; x<n; x+=step) {
			double lspline = linspline.linterp(xdata, ydata, x);
			double integ = linspline.linterpInteg(xdata, ydata, x);
			Error.WriteLine($"{x} {lspline} {integ}");
		}
	}
}
