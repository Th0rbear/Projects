// C# program to demonstrate the use of the System.Math class  
using System;

class math{
	static void Main(){
		//find the squareroot of 2
		double sqrt2 = Math.Sqrt(2.0);
		//print the result
		Console.Write($"sqrt(2) = {sqrt2}\n");
		//check the result
		Console.Write($"sqrt(2)*sqrt(2) = {sqrt2*sqrt2} (should be equal 2)\n");
		//calculate exponential function with pi in the exponent
		double expPi = Math.Pow(Math.E, Math.PI);
		//the inverse of expPi is made to check the result
		double expMinusPi = Math.Pow(Math.E, -Math.PI);
		Console.Write($"exp(pi) = {expPi}\n");
		Console.Write($"exp(pi)*exp(-pi) = {expPi*expMinusPi} (should be equal 1)\n");
		//calculate pi to the power of Euler's number
		double piPowE = Math.Pow(Math.PI, Math.E);
		double piPowMinusE = Math.Pow(Math.PI, -Math.E);
		Console.Write($"pi^(e) = {piPowE}\n");
		Console.Write($"pi^(e)*pi^(-e) = {piPowE*piPowMinusE} (should be equal 1)\n");
	}
}	
