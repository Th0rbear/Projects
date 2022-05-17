/*A program for testing the quasi-Newton minimization method. This is done by finding a minimum of the Rosenbrock's
 *valley function, and finding a minimum of the Himmelblau's function. */
using System;
using static System.Console;
using static System.Math;

public class main {
	
	public static void Main() {
		//Determining the minimum of Rosenbrock's valley function: f(x,y) = (1 - x)^2 + 100(y - x^2)^2
		Func<vector, double> rosenbrock = x => Pow(1 - x[0], 2) + 100*Pow(x[1] - Pow(x[0], 2), 2);
		vector x0 = new vector(0.1, 0.1);
		
		var (xR, stepsR) = Minim.qnewton(rosenbrock, x0, acc: 1e-4); //changing acc from 0.01 to 0.0001 the algorithm is repeated two more times

		WriteLine("\nFinding minimum of f(x,y) = (1 - x)^2 + 100(y - x^2)^2");
		WriteLine("The minimum found has the following coordinates:");
		WriteLine($"\n({xR[0]},{xR[1]}) with the algorithm running in {stepsR} iterations");
		WriteLine($"With these coordinates the value of the function is f(x,y) = {rosenbrock(xR)}");
		WriteLine("\nThe minimum should have the coordinates (1,1) where f(x,y) = 0");
		
		//Determining the minimum of Himmelblau's function: f(x,y) = (x^2 + y - 11)^2 + (x + y^2 - 7)^2
		Func<vector, double> himmelblau = x => Pow(Pow(x[0], 2)+x[1]-11, 2)+Pow(x[0]+Pow(x[1], 2)-7, 2);  
		//vector x02 = new vector(0.1, 0.1);
		var (xH, stepsH) = Minim.qnewton(himmelblau, x0, acc: 1e-4); //changing acc from 0.01 to 0.0001 the algorithm is repeated one more time
		//when lowering the accuracy goal, the numerical value approaches the analytical, but does not change
		//significantly.
		WriteLine("\nFinding minimum of f(x,y) =  (x^2 + y - 11)^2 + (x + y^2 - 7)^2");
		WriteLine("The minimum found has the following coordinates:");
		WriteLine($"\n({xH[0]},{xH[1]}) with the algorithm running in {stepsH} iterations");
		WriteLine($"The value of the function is then f(x,y) = {himmelblau(xH)}");
		WriteLine("\nHimmelblau's function has four identical local minima one them have the coordinates (3, 2) where f(x,y) = 0");
	}
}
