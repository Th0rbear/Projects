/*Using the Jacobi algorithm the lowest eigenvalues and eigenfunctions of the s-wave hydrogen atom will be calculated
 *and compared with the exact results. */
using System;
using static System.Console;
using static System.Math;

class main {
	static double dr = 0.2;
	static double rmax = 30;

	public static void Main() {
		//determining the eigenvalues / energies and eigenfunctions for hydrogen by using the Jacobi algorithm 
		//on H
		(matrix H, vector r) = buildH(150);
		(vector e, matrix V) = Jevd.JacobiAlg(H);

		//comparing the numerical solutions with the exact analytical solutions, for the radial wavefunctions,
		//for the three lowest energy levels.
		//The wavefunctions are in units of Bohr radius a, and the energies in units of Hartree.
		var radialWaveFuncs = new Func<double, double>[] {
			x => 2*Exp(-x),
			x => 1/Sqrt(2)*(1 - x/2.0)*Exp(-x/2),
			x => 2/(3*Sqrt(3))*(1 - 2*x/3.0 + 2/27.0*x*x)*Exp(-x/3)};
		
		int nrEnergies = 2;
		for(int ei=0; ei<=nrEnergies; ei++) {
			for(int i=0; i<V.size1; i++) {
				// Remember that the analytical functions are normalized
				double unnorm = V[ei][4]/radialWaveFuncs[ei](r[4]);
				WriteLine($"{r[i]} {V[ei][i]} {r[i]*radialWaveFuncs[ei](r[i])*unnorm}");
			}
			WriteLine("\n");
		}
		rmaxConvergence();
		drConvergence();
	}

	/*Method for examining convergence of energies with respect to rmax*/
	public static void rmaxConvergence() {
		//1 Hatree is about 27.2 eV so the groundstate of hydrogen is -0.5 Hatree and so on 
		double[] energies = new double[] {-1.0/2, -1.0/8, -1.0/18};
		using(var outfile = new System.IO.StreamWriter("rmax-conv.txt")) {
			for(int ei=0; ei<=2; ei++) {
				for(int r=1; r<=rmax; r++) {
					int npoints = (int)(r/dr-1);
					(matrix H, vector rs) = buildH(npoints);
					(vector e, matrix V) = Jevd.JacobiAlg(H);
					outfile.WriteLine($"{r} {e[ei]} {energies[ei]}");
				}
				outfile.WriteLine("\n");
			}
			outfile.WriteLine("As expected the numerical energies converge towards the analytical values for large r's");
		}
	}
	
	/*Method for examining convergence of energies with respect to dr*/
	public static void drConvergence() {
		double[] energies = new double[] {-1.0/2, -1.0/8, -1.0/18};
		using(var outfile = new System.IO.StreamWriter("dr-conv.txt")) {
			for(int ei=0; ei<=2; ei++) {
				for(double dr = 0.1; dr<6; dr+=0.1) {
					int npoints = (int)(rmax/dr-1);
					vector r = new vector(npoints);
					for(int i=0; i<npoints; i++) r[i]=dr*(i+1);
					matrix H = new matrix(npoints, npoints);
					for(int i=0; i<npoints-1; i++){
						matrix.set(H,i,i,-2);
						matrix.set(H,i,i+1,1);
						matrix.set(H,i+1,i,1);
					}
					matrix.set(H,npoints-1,npoints-1,-2);
					H*=-0.5/dr/dr;
					for(int i=0; i<npoints; i++) H[i,i] += -1/r[i];

					(vector e, matrix V) = Jevd.JacobiAlg(H);
					outfile.WriteLine($"{dr} {e[ei]} {energies[ei]}");
				}
				outfile.WriteLine("\n");
			}
			outfile.WriteLine("The numerical energies converge toward the analytical values for smaller dr's, which is to be expected.");
		}
	}

	/*Method for building the Hamiltonian matrix, H, for the hydrogen atom*/
	public static (matrix, vector) buildH(int npoints) {
		vector r = new vector(npoints);
		for(int i=0; i<npoints; i++) {
			r[i]=dr*(i+1);
		}
		matrix H = new matrix(npoints, npoints);
		for(int i=0; i<npoints-1; i++) {
			matrix.set(H,i,i,-2);
			matrix.set(H,i,i+1,1);
			matrix.set(H,i+1,i,1);
		}
		matrix.set(H,npoints-1,npoints-1,-2);
		H*=-0.5/dr/dr;
		for(int i=0; i<npoints; i++) {
			H[i,i] += -1/r[i];
		}
		return (H, r);
	}
}
