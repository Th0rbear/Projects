/*A program for testing the ANN. For this purpose the function cos(5*x-1)*Exp(-x^2) is used in the domain of [-1,1], 
 *while using the gaussian wavelet as the activation function, i.e. x*Exp(-x^2). */
using System;
using static System.Console;
using static System.Math;

public class main {
	
	public static void Main() {
		Func<double, double> activFunc = x => x*Exp(-Pow(x,2));
		Func<double, double> g = x => Cos(5*x - 1)*Exp(-Pow(x,2));

		int n = 3; 				//# hidden neurons
		Ann network = new Ann(n, activFunc);
		double a = -1, b = 1;			//The domain [-1,1]
		int nx = 25;				//# points
		vector xs = new vector(nx);
		vector ys = new vector(nx);

		for(int i = 0; i<nx; i++) {
			xs[i] = a + (b-a)*i/(nx-1);	//making equally spaced points with a total of nx points
			ys[i] = g(xs[i]);		//the corresponding y-points
			WriteLine($"{xs[i]} {ys[i]}");
		}

		WriteLine("\n");
		
		//the initial values for the parameters in the p vector are assigned
		for(int i = 0; i<network.n; i++) {
			network.p[3*i + 0] = a + (b-a)*i/(network.n-1);
			network.p[3*i + 1] = 1; // b_i is initially 1
			network.p[3*i + 2] = 1; // w_i is initially 1
		}

		network.train(xs, ys); //Training the network given the supervising points (the {x_i, y_i} from i to nx)
		//After the network training the optimal parameters for p should be determined to interpolate g(x)
		for(double t = a; t<=b; t += 1.0/100) {
			WriteLine($"{t} {network.response(t)}"); //Creating the interpolated points
		}
		//The interpolation of g can be seen in plot.png
	}
}
