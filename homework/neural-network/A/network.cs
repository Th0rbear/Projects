/*Implementation of a simple artificial neural network (ANN) which will be trained to interpolate a tabulated function.
 *An ANN is simply a mathematical function, F, with a vector argument x (input signal), and a vector p with the set of
 *internal parameters of the network. It is an ordinary three layer neural network with one neuron in the input layer,
 *several neurons in the hidden layer, and one neuron in the output layer. The input neuron sends the input, a real 
 *number x, to all hidden neurons without modifications. The output neuron sums the outputs of the hidden neurons and 
 *sends the result to the output. The neuron number i transforms its input signal, x, into its output signal, y_i. */
using System;
using static System.Math;

public class Ann {

	public int n;			//number of hidden neurons
	public Func<double, double> f;	//activation function
	public vector p;		//parameters for the network

	//Constructor
	public Ann(int n, Func<double, double> f) {
		this.n = n;
		this.f = f;
		this.p = new vector(3*n);
	}
	
	//Function that gives the return value of the network response F_p(x)
	public double response(double x) {
		double Fpx = 0;
		
		for(int i = 0; i<n; i++) {
			//accessing the parameters a, b and w from p, then evaluate F_p(x)
			double a = p[3*i + 0];
			double b = p[3*i + 1];
			double w = p[3*i + 2];
			Fpx += f((x - a)/b)*w;
		}
		return Fpx;
	}
	
	//Training is tuning the network's parameters p to better handle a given task. It typically involves minimiza-
	//tion of a certain cost function of network parameters C(p). Here we use the one-dimensional interpolation
	//where the input data is pairs {x_i, y_i} for i to n, and the cost function is the average squared deviation.
	public void train(vector x, vector y) {
		Func<vector, double> costFunc = C => {
			p = C;
			int m = x.size; //should be the same size as n
			double Cp = 0;

			for(int i = 0; i<m; i++) Cp += Pow(response(x[i]) - y[i], 2);

			return Cp/m;
		};
		var (xSol, steps) = Minim.qnewton(costFunc, p);
		p = xSol;
	}
}
