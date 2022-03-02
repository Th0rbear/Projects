using System;
using static System.Console;
using static System.Math;
//A class to make a plot of the logarithm to the gamma function
class main{
	
	public static double lngamma(double x) {
		if(x<=0) return Double.NaN;
		//a method or operator returns NaN when the result of an operation is undefined
		//natural logarithm is undefined for negative values and x=0
		if(x<9) return lngamma(x+1)-Log(x);
		//The function calls itself recursively	
		return x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2;
	}
	
	public static void Main() {
		for(double x=0.1; x<=5; x+=1.0/20) {
			WriteLine($"{x} {lngamma(x)}");
		}
	}
}
