//Linear interpolation is a spline, i.e. a piecewise polynomial to the first order used to
//construct a function passing through a set of points. 
using System;
using static System.Math;

public static class linspline{
	
	//Makes use of the binary search algorithm or half-interval search. It finds the index i
	//of point x in an ordered array of x-data.
	public static int binsearch(double[] xs, double x) {
		//if(!(xs[0]<=x && x<=xs[xs.Length-1])) throw new Exception("binsearch: bad value for x");
		int i=0, j=xs.Length-1;
		while(j-i > 1){
			int mid = (i+j)/2;
			if(x > xs[mid]) i=mid; else j=mid;
		}
		return i;
	}

	//A function that makes linear spline interpolation from a table {xs[i], ys[i]} at a given point x.
	public static double linterp(double[] xs, double[] ys, double x) {
		int i=binsearch(xs, x);
		double dx=xs[i+1]-xs[i]; if(!(dx>0)) throw new Exception("The x-data list is not sorted");
		double dy=ys[i+1]-ys[i];
		return ys[i] + dy/dx*(x - xs[i]);
	}

	//A function that calculates the integral of the linear spline from the point xs[0] to the given
	//point x. You must make a linear spline interpolation before using this function.
	public static double linterpInteg(double[] xs, double[] ys, double x) {
		double totalsum = 0, dx = 0, dy = 0;
		int id=binsearch(xs, x);
		//summing up the infinitesimal wide rectangles to just before the given point x with index id
		for(int i=0; i<id; i++){
			dy = ys[i+1] - ys[i];
			dx = xs[i+1] - xs[i];
			totalsum += ys[i]*dx + dy/dx*0.5*dx*dx;
		}
		//and then lastly add the last rectangle to the given point x
		dx=xs[id+1]-xs[id]; 
		dy=ys[id+1]-ys[id];
		totalsum += ys[id]*(x-xs[id]) + dy/dx*0.5*(x-xs[id])*(x-xs[id]);
		return totalsum;
	}
}
