//Example of a generic List class. A generic list just means that a list can be made of any type, strings,
//doubles, vecs etc.
public class genlist<T> {
	public T[] data;
	//data is actually an array of the type T
       	
	public int size {get{return data.Length;}} //get the size of the list
	
	public genlist() {
		data = new T[0];
		//initialize an empty list
	}
	//method for adding an item to the list
	public void push(T item) {
		T[] newdata = new T[size+1];
		//make a new list with the size being 1 bigger
		for(int i=0; i<size; i++) {
			newdata[i] = data[i];
		}
		//guarantees that the new list has the same elements at every index as the previous list
		newdata[size] = item;
		//adds the item we want to push at the end of the list
		data = newdata;
	}
}	
