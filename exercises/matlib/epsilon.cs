using static System.Math;
using static System.Console;
//We need only one instance of the class so it is declared static, thus all its methods must be 
//static aswell
public static class machineepsilon{
	//method that gives max/min representable integer
	public static int maxOrMinInt(bool flag) {
		int i=1;
		if (flag) {
			while(i+1>i) {i++;}
			return i;
		}
		else {
			while(i-1<i) {i--;}
			return i;
		}
	}
	
	//Gives a limit for the min value of a double and float type
	public static (double, float) precDAndF() {
		double x=1;
		float y=1F;
		while(1+x!=1) {x/=2;} x*=2; //equivalent to x=x/2
		while((float) (1F+y)!=1F) {y/=2F;} y*=2F; 
		return (x, y);
	}

	public static void sumAAndSumB() {
		int n=(int) 1e6; //need to type cast 1e6 to an int, it is a double by default
		double epsilon=Pow(2,-52);
		double tiny=epsilon/2;
		double sumA=0, sumB=0;
		sumA+=1; for(int i=0; i<n; i++) {sumA+=tiny;}
		for(int i=0; i<n; i++) {sumB+=tiny;} sumB+=1;
		WriteLine($"sumA-1 = {sumA-1:e} should be {n*tiny:e}");
		//when you have a large number and add a very small number you loose information
		//so in the example for sumA the increment with the variable 'tiny' will not
		//accumulate thus we get 0 for sumA
		WriteLine($"sumB-1 = {sumB-1:e} should be {n*tiny:e}");
		//It's no problem when you first add the small numbers and then add the large
		//number at last.
	}
	public static bool approx(double a, double b, double tau=1e-9, double epsilon=1e-9) {
		if (Abs(a-b) < tau|Abs(a-b)/(Abs(a)+Abs(b)) < epsilon) {
			return true;
		}	
		else {
			return false;
		}
	}
}
