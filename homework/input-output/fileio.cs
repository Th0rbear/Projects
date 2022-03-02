//Program that reads a set of numbers separated by newlines from "inputfile" and writes them together with their sines
//and cosines to "outputfile". The program reads the names of the "inputfile" and "outputfile" from the command-line.
using static System.Console;
using static System.Math;
class main{
	public static int Main(string[] args) {
		string infile=null, outfile=null;
		foreach(var arg in args) {
			var words = arg.Split(':');
			if(words[0]=="-input") {infile=words[1];}
			else if(words[0]=="-output") {outfile=words[1];}
			else { Error.WriteLine("wrong argument"); return 1;}
		}
		var inStream = new System.IO.StreamReader(infile);
		var outStream = new System.IO.StreamWriter(outfile);
		for(string line=inStream.ReadLine(); line!=null; line=inStream.ReadLine()) {
			double x=double.Parse(line);
			outStream.WriteLine($"{x} {Sin(x)} {Cos(x)}");
		}
		inStream.Close();
		outStream.Close();
		return 0;
	}
}
