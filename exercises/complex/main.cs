using static System.Math;
using static System.Console;

class main{
	static void Main(){
		//calculate sqrt to -1
		//remember in the implementation of a complex number -1 would be complex(-1,0)
		complex minusOne = new complex(-1);
		Write($"sqrt(-1) = {cmath.sqrt(minusOne)} (should be i)\n");
		//calculate sqrt(i), can use cmath.I or complex.I
		Write($"sqrt(i) = {cmath.sqrt(cmath.I)} (should be sqrt(1/2) + i*sqrt(1/2))\n");
		Write($"exp(i) = {cmath.exp(cmath.I)} (should be cos(1) + i*sin(1))\n");
		Write($"exp(i*pi) = {cmath.exp(cmath.I*PI)} (should be -1)\n");
		Write($"i^(i) = {cmath.pow(cmath.I, cmath.I)} (should be 0.208)\n");
		Write($"ln(i) = {cmath.log(cmath.I)} (should be i*pi/2)\n");
		Write($"sin(i*pi) = {cmath.sin(cmath.I*PI)} (should be 11.5*i)\n");
		//testing sinh & cosh in cmath by calculating sinh(i) and cosh(i)
		Write($"sinh(i) = {cmath.sinh(cmath.I)} (should be i*sin(1))\n");
		Write($"cosh(i) = {cmath.cosh(cmath.I)} (should be cos(1))\n");
	}
}
