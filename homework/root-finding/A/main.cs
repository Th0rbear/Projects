/*Here Newtons root-finding algorithm will be tested on a few examples, like a 1D equation and two 2D equations */
using System;
using static System.Console;
using static System.Math;

public class main {
	
	public static void Main() {
		//testing a cubic polynomial (third degree): f(x) = x^3+3x^2-x-3
		//If we want to find its extremums we need to determine the roots of its gradient: f'(x) = 3x^2+6x-1
		Func<vector, vector> f = x => new vector(3*Pow(x[0],2)+6*x[0]-1);
		vector x0 = new vector(0.1);
		vector sol = Roots.newton(f, x0, 1e-5);
		WriteLine("\nDetermining extremums of f(x) = x^3+3x^2-x-3");
		WriteLine("Thus roots of its gradient - f'(x) = 3x^2+6x-1 - are determined");
		WriteLine("There should be two extremums, the first is a maximum and the second is a minimum");
		WriteLine($"\nThe minimum is found at:	{sol[0]}");
		WriteLine($"The analytical result is:	0.1547");
		
		//Testing the simple function of two variable: f(x,y) = x^2+y^2+1
		//To find its extremums we determine the roots of its gradient: Nabla(f(x,y)) = (2x, 2y)
		Func<vector, vector> f2 = x => new vector(2*x[0], 2*x[1]);
		vector x02 = new vector(0.1, 0.1);
		vector sol2 = Roots.newton(f2, x02, 1e-6);
		//when lowering ε, the closer the numerical value is to the analytical
		WriteLine("\nDetermining extremums of f(x,y) = x^2+y^2+1");
		WriteLine("Its gradient is Nabla(f(x,y)) = (2x, 2y)");
		WriteLine($"\nThe roots of the gradient of f are:	{sol2[0]}	{sol2[1]}");
		WriteLine($"The analytical result is:		0			0");

		//lastly the extremum of Rosenbrock's valley function is determined: f(x,y) = (1-x)^2+100(y-x^2)^2
		//The gradient of this function is: Nabla(f(x,y)) = (2*[200*x^3 - 200*y*x + x - 1], 200*[y - x^2])
		Func<vector, vector> rosenbrock = x => new vector(2*(200*Pow(x[0],3) - 200*x[1]*x[0] + x[0] - 1), 200*(x[1] - Pow(x[0],2)));
		vector solRosenb = Roots.newton(rosenbrock, x02, 1e-6);
		WriteLine("\nDetermining extremums of f(x,y) = (1-x)^2+100(y-x^2)^2");
		WriteLine("Its gradient is Nabla(f(x,y)) = (2*[200*x^3 - 200*y*x + x - 1], 200*[y - x^2])");
		WriteLine($"\nThe roots of the gradient of f are:	{solRosenb[0]}	{solRosenb[1]}");
		WriteLine($"The analytical result is:		1			1");
	}
}
