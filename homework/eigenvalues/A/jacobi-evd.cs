/*Implementation of Jacobi eigenvalue algorithm, which is an algorithm that determines the eigenvectors and eigenvalues
 * of a real symmetric matrix by a sequence of Jacobi rotations. Essentially it's a way to decompose a real symmetric
 * matrix into a matrix consisting of its eigenvectors and a diagonal matrix consisting of its eigenvalues */

using static System.Math;
using static System.Console;

public static class Jevd {

	/*Function that multiplies the given matrix A and the Jacobi matrix J(p,q,θ) in O(n) operations, A <-- AJ. */
	public static void timesJ(matrix A, int p, int q, double θ) {
		double c = Cos(θ);
		double s = Sin(θ);
		for(int i=0; i<A.size1; i++) {
			double A_ip = A[i,p];
			double A_iq = A[i,q];
			A[i,p] = c*A_ip - s*A_iq;
			A[i,q] = s*A_ip + c*A_iq;
		}
	}

	/*Function that multiplies the Jacobi matrix J(p,q,θ) and the given matrix A in O(n) operations, A <-- JA. */
	public static void Jtimes(matrix A, int p, int q, double θ) {
		double c = Cos(θ);
		double s = Sin(θ);
		for(int j=0; j<A.size1; j++) {
			double A_pj = A[p,j];
			double A_qj = A[q,j];
			A[p,j] = c*A_pj + s*A_qj;
			A[q,j] = -s*A_pj + c*A_qj;
		}
	}

	/*Function that implements the Jacobi egienvalue algorithm for real symmetric matrices using the cyclic
	 *sweeps: eliminates the off-diagonal elements in a predefined sequence which spans all off-diagonal elements,
	 *e.g. row after row, and repeats the sweeps until converged. The convergence criterion could be that the
	 *eigenvalues did not change after a whole sweep. 
	 *The function returns a vector e which consists of the eigenvalues and a matrix V which contains the
	 *corresponding eigenvectors */
	
	public static (vector, matrix) JacobiAlg(matrix A) {
		int n = A.size1;
		vector e = new vector(n);
		matrix V = matrix.id(n);

		for(int i=0; i<n; i++) {e[i] = A[i,i];}

		int sweeps = 0;
		bool changed;
		do{
			sweeps++;
			WriteLine($"number of sweeps: {sweeps}");
			changed = false;
			for(int p=0; p<n-1; p++) {
				for(int q=p+1; q<n; q++) {
					double A_pq = A[p,q];
					double A_pp = A[p,p];
					double A_qq = A[q,q];
					double θ = 0.5*Atan2(2*A_pq, A_qq - A_pp);
					double c = Cos(θ);
					double s = Sin(θ);
					double newA_pp = c*c*A_pp - 2*s*c*A_pq + s*s*A_qq;
					double newA_qq = s*s*A_pp + 2*s*c*A_pq + c*c*A_qq;
					if(newA_pp != A_pp || newA_qq != A_qq) { // do rotation
						changed = true;
						e[p] = newA_pp;
						e[q] = newA_qq;
						timesJ(A,p,q,θ);
						Jtimes(A,p,q,-θ); // A <-- J^T*A*J
						timesJ(V,p,q,θ); // V <-- V*J
					}
				}
			}
		}while(changed);
		
		return (e, V);
	}
}
