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
	public void removeItem(int i){
		T[] newdata = new T[size-1];
		int j=0;
		int k=0;
		while (j < data.Length) {
			if(j != i) {
				newdata[k] = data[j];
				k++;
			}
			j++;
		}
		data = newdata;
		if(data.Length<i || i<0) 
			throw new ArgumentException("Index is out of range", nameof(i));

	}	
}
