using System;
using static System.Console;
using static System.Math;
//a class that plots the gamma-function with several of its tabulated values (factorials)
public static class gammafun{

	public static double gamma(double x) {
		//single precision gamma function (Gergo Nemes, from Wikipedia)
		if(x<0)return PI/Sin(PI*x)/gamma(1-x);
		if(x<9)return gamma(x+1)/x;
		double lngamma=x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2;
		return Exp(lngamma);
	} 
	
	//public static double stirlingApprox(double x) {
	//	if(x<0)throw new ArgumentException("Parameter cannot be negative", nameof(x));
	//	return Sqrt(2*PI*x)*Pow(x,x)*Exp(-x);
	//}
		
	public static void Main() {
		for(double x=0.1; x<=5; x+=1.0/20) {
			WriteLine($"{x} {gamma(x)}");
		}                             
	}
}
