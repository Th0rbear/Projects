using System;
using static System.Console;

public class test{
	
	public static void Main() {

		genlist<int> intlist = new genlist<int>();
		intlist.push(2);
		intlist.push(5);
		intlist.push(7);
		intlist.push(8);
		intlist.removeItem(1);
	}
}
