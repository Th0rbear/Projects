// Another class of a generic list which improves the speed of the "push" method by increasing the capacity of the data
// array not by one element, but instead by doubling it. This decreases the amount of array copying significantly for
// the price of increased memory usage.
using System;
public class genlist<T>{
	public T[] data;
	public int size=0, capacity=8;
	
	public genlist(){data = new T[capacity];}
	
	public void push(T item) {
		if(size==capacity) {
			T[] newdata = new T[capacity*=2];
			for(int i=0; i<size; i++) newdata[i]=data[i]; 
			data = newdata;
		}
		data[size]=item;
		size++;
	}
	//method that removes element number "i".
	public void removeItem(int index){
		//This implementation might not be the most effective but it keeps the order of indices
		//and makes the list shorter.
		T[] newdata = new T[data.Length-1]; // ensures that the list becomes shorter
		int j=0;
		int k=0;
		while (j < data.Length) {
			if( j != index) {
				newdata[k] = data[j];
				k++;
			}
			j++;
		}
		capacity--; // this statement is needed, else if we exceed the capacity the push method will give 
		            // an index out of bound error.
		size--;     // ensures that when an element is pushed afterwards, it gets pushed / appended to the
		            // bottom of the list.
		data = newdata;
		//This implementation does not make the list shorter after removing an element
		//for(int i=index;i<size-1;i++){
		//	data[i]=data[i+1];
		//	}
		//size--;
		if(data.Length<index || index<0) throw new ArgumentException("Index is out of range", nameof(index));

	}	
}
