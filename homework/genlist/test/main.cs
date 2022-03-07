using System;
using static System.Console;

public class test{
	
	public static void Main() {

		genlist<int> intlist = new genlist<int>();
		intlist.push(2);
		intlist.push(5);
		intlist.push(7);
		intlist.push(8);
		WriteLine("The list before removing an element:");
		for(int i=0; i<intlist.data.Length; i++){                                                              
			WriteLine($"index {Array.IndexOf(intlist.data, intlist.data[i])} with value {intlist.data[i]}");                }
		intlist.removeItem(3);
		WriteLine("The list after removing the last element:");
		for(int i=0; i<intlist.data.Length; i++){
			WriteLine($"index {Array.IndexOf(intlist.data, intlist.data[i])} with value {intlist.data[i]}");
		}
		intlist.push(1);
		WriteLine("The list after adding an item:"); 
		for(int i=0; i<intlist.data.Length; i++){
			WriteLine($"index {Array.IndexOf(intlist.data, intlist.data[i])} with value {intlist.data[i]}");
		}
		intlist.removeItem(0);
		WriteLine("The list after removing an item again:");
		for(int i=0; i<intlist.data.Length; i++){
			WriteLine($"index {Array.IndexOf(intlist.data, intlist.data[i])} with value {intlist.data[i]}");
		}
		WriteLine("4 elements are added to check that capacity is doubled:");
		intlist.push(3);
		intlist.push(4);
		intlist.push(9);
		intlist.push(10);
		for(int i=0; i<intlist.data.Length; i++){
			WriteLine($"index {Array.IndexOf(intlist.data, intlist.data[i])} with value {intlist.data[i]}");
		} 
	}
}
