using static System.Console;

class Test{
	static void Main(){
		WriteLine("\nPart A: testing overloaded mathematical operators for vectors\n");
		vec w = new vec(); //should be a zero-vector
		vec v1 = new vec(3, -2, 7);
		vec v2 = new vec(-1, 4, 9);
		vec u1 = new vec(2, 2, 1);
		vec u2 = new vec(-2, -2, -1);	
		w.print("w = ");
		v1.print("v1 = ");
		v2.print("v2 = ");
		u1.print("u1 = ");
		u2.print("u2 = ");
		(v1+v2).print("v1+v2 = ");
		(-v1).print("-v1 = ");
		(v1-v2).print("v1-v2 = ");
		(v2-v1).print("v2-v1 = ");
		var a = v1*0;
		a.print("v1*0 = ");
		var b = v1*1;
		b.print("v1*1 = ");
		var c1 = v1*3;
		var c2 = 3*v1;
	        c1.print("v1*3 = ");
		c2.print("3*v1 = ");
		Write("-----------------------------------------------------------------------\n");
		WriteLine("Part B: testing implementation of dot product, vector-product, norm");
		WriteLine("and toString method\n");
		Write($"v1 dot w = {v1.dot(w)} (should be 0)\n");
		Write($"v1 dot v1 = {v1.dot(v1)}\n");
		Write($"v1 dot v2 = {v1.dot(v2)}\n");
		var d1 = v1.cross(v1);
		d1.print("v1 x v1 = ");
		var d2 = v1.cross(v2);
		var d3 = -v2.cross(v1);
		d2.print("v1 x v2 = ");
		d3.print("-v2 x v1 = ");
		Write($"v1 dot(v1 x v2) = {v1.dot(v1.cross(v2))} (should be 0)\n");
		Write($"|u1| = {u1.norm()} (should be 3)\n");
		Write($"|u2| = {u2.norm()} (should have same length as u1)\n");
		Write($"|v1| = {v1.norm()}\n");
		//test ToString method
		WriteLine(v1);
		Write("-----------------------------------------------------------------------\n");
		WriteLine("Part C: testing approx method to compare two vec's with absolute");
		WriteLine("precision τ and relative precision ε\n");
		vec e1 = new vec(1, 1, 1);
		vec e2 = new vec(1+1e-9, 1+1e-9, 1+1e-9);
		vec e3 = new vec(1+1e-9, 1+1e-9, 1.1);
		e1.print("e1 = ");
		e2.print("e2 = ");
		e3.print("e3 = ");
		Write($"e1 approximately equal to e2: {e1.approx(e2)}\n");
		Write($"e1 should not be approximately equal to e3: {e1.approx(e3)}\n");

	}
}
