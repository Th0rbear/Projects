/*Here matrices are QR decomposed via modified Gram-Schmidt (GS) orthogonalization to solve linear equations. The
 *  GS orthogonalization is used to make the matrix Q orthogonal and also making a right triangular matrix R. */
using System;
using static System.Math;

public class QRGS {
	/* field variables the Q and R matrices */
	private matrix Q { get; }
	private matrix R { get; }
	
	/* constructor: run the Gram-Schmidt process and creat Q and R */
	public QRGS(matrix A) {
		int m = A.size2;
		int n = A.size1;
		if(!(n>=m)) throw new Exception ("Columns n should be larger or equal to rows m");
		Q = A.copy();
		R = new matrix(m,m);
		//We define the diagonal of R to be norm of the a_i column vectors we can thus create the R
		//and Q matrix in the GS
		for(int i=0; i<m; i++) {
			R[i,i] = Q[i].norm(); 
			//determines the norm of the column vectors in A via the vector class
			Q[i]/= R[i,i];
		       for(int j=i+1; j<m; j++) {
				R[i,j] = Q[i].dot(Q[j]);
				Q[j]-= Q[i]*R[i,j];
		       }
		}	
	}
	
	/* given the matrices Q and R, solve the equation QRx=b by applying Q^T to the vector b (saving the 
	 * result in a new vector x) and then performing in-place back substitution on x and returning it. */
	public vector solve(vector b) {
		vector x;
		x = Q.transpose()*b;
		for(int i=x.size-1; i>=0; i--) {
			double sum=0;
			for(int j=i+1; j<x.size; j++) {
				sum+= R[i,j]*x[j];
			}
			x[i] = (x[i]-sum)/R[i,i];
		}
		return x;
	}

	/*Given the matrices Q and R from QRGS decomposition calculates the inverse of the matrix A */
	public matrix inverse() {
		int n = Q.size1;
		//the matrix should be a square matrix
		if(n != Q.size2) throw new Exception("QRGS inverse: n != m");
		var B = new matrix(n,n);
		var e = new vector(n);
		//think of it as n linear equations of the form Ax_i=e_i and then we solve for x_i, then the solution
		//will be the i'th column vector in B which is the inverse of A. 
		for(int i=0; i<n; i++) {
			e[i] = 1;
			B[i] = solve(e);
			e[i] = 0;
		}
		return B;
	}

	/* accessor to Q */
	public matrix GetQ() { return Q;}
	
	/* accessor to R */
	public matrix GetR() { return R;}
}
