using static System.Console;
using static System.Math;

// A class that represents a 3D vector
public class vec{
	//field variables
	public double x, y, z;
	
	//constructors:
	public vec(double x, double y, double z) {
		this.x=x; this.y=y; this.z=z;
	}

	public vec() {
		x=y=z=0;
	}

	//print method: v.print("v=") and then you get the values for x-,y- and z-component
	public void print(string s) {
		Write(s);
		WriteLine($"{x} {y} {z}");
	}

	public void print() {
		this.print("");
	}

	//override ToString method
	public override string ToString() {
		return "x: " + x + " y: " + y + " z: " + z;
	}

	//redefine some arithmetic operators by overloading them
	public static vec operator+(vec v, vec u) {
		return new vec(v.x+u.x, v.y+u.y, v.z+u.z);
	}

	public static vec operator-(vec v) {
		return new vec(-v.x, -v.y, -v.z);
	}

	public static vec operator-(vec v, vec u) {
		return new vec(v.x-u.x, v.y-u.y, v.z-u.z);
	}

	public static vec operator*(vec v, double c) {
		return new vec(c*v.x, c*v.y, c*v.z);
	}

	//scalar multiplication on a vector should be the same from left and right
	public static vec operator*(double c, vec v) {
		return v*c;
	}

	//method that determines the dot product between two vectors
	public double dot(vec other) {
		return this.x*other.x+this.y*other.y+this.z*other.z;
	}

	//method that determines the cross product between two vectors
	public vec cross(vec other) {
		return new vec(this.y*other.z-this.z*other.y, this.z*other.x-this.x*other.z,
			    	this.x*other.y-this.y*other.x);
	}

	//method that determines the length or norm of a vector
	public double norm() {
		return Sqrt(this.dot(this));
	}

	//method to compare two vec's with absolute precision tau and relative precision epsilon
	public bool approx(double a, double b, double tau=1e-9, double eps=1e-9) {
		if(Abs(a-b)<tau) {return true;}
		if(Abs(a-b)/(Abs(a)+Abs(b))<eps) {return true;}
		return false;
	}
	//compare the vec's components with approx. Only if all components pass the condition will we get true
	public bool approx(vec other) {
		if(!approx(this.x, other.x)) {return false;}
		if(!approx(this.y, other.y)) {return false;}
		if(!approx(this.z, other.z)) {return false;}
		return true;
	}
	
	//If you want to write approx(u, v) instead of v.approx(u).
	public static bool approx(vec u, vec v) {return u.approx(v);}

}
