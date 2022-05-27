/*Basically same implementation from A, but with some extensions such that the network, after training can approximate
 *the derivative and the integral of the tabulated function. */
using System;
using static System.Math;

public class Ann {

	public int n;				//number of hidden neurons
	public Func<double, double> f;		//activation function
	public Func<double, double> fPrime;	//derivative of activation function
	public Func<double, double> fInt;	//integral of activation function
	public vector p;			//parameters for the network

	//Constructor
	public Ann(int n, Func<double, double> f, Func<double, double> fPrime, Func<double, double> fInt) {
		this.n = n;
		this.f = f;
		this.fPrime = fPrime;
		this.fInt = fInt;
		this.p = new vector(3*n);
	}
	
	//Function that gives the return value of the network response F_p(x), but also the response for the derivative
	//and the integral of the activation function
	public (double, double, double) response(double x) {
		double Fpx = 0;
		double FpxPrime = 0;
		double FpxInt = 0;
		
		for(int i = 0; i<n; i++) {
			//accessing the parameters a, b and w from p, then evaluate F_p(x)
			double a = p[3*i + 0];
			double b = p[3*i + 1];
			double w = p[3*i + 2];
			Fpx += f((x - a)/b)*w;
			FpxPrime += fPrime((x - a)/b)*w/b;
			FpxInt += fInt((x - a)/b)*w/b;
		}
		return (Fpx, FpxPrime, FpxInt);
	}
	
	//Training is tuning the network's parameters p to better handle a given task. It typically involves minimiza-
	//tion of a certain cost function of network parameters C(p). Here we use the one-dimensional interpolation
	//where the input data is pairs {x_i, y_i} for i to n, and the cost function is the average squared deviation.
	public void train(vector x, vector y) {
		Func<vector, double> costFunc = C => {
			p = C;
			int m = x.size; //should be the same size as n
			double Cp = 0;

			for(int i = 0; i<m; i++) { 
				var resp = response(x[i]);
				Cp += Pow(resp.Item1 - y[i], 2);
			}

			return Cp/m;
		};
		var (xSol, steps) = Minim.qnewton(costFunc, p);
		p = xSol;
	}
}
