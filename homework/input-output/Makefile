all: out-stdin.txt out.cmdline.txt #outfile.txt
	cat out-stdin.txt out.cmdline.txt #outfile.txt

#For part A
out-stdin.txt: stdin.exe
	echo 1 2 3 4 5 | mono stdin.exe > out-stdin.txt  #echo outputs the strings passed to it as arguments. 
	#This command is executed and its output is used as input in 'mono stdin.exe' which is redirected to the txt,

#For part B
out.cmdline.txt: cmdline.exe
	mono cmdline.exe 1 2 3 4 5 > out.cmdline.txt #run executable with input, redirect output into txt.

#meant for part C
#outfile.txt: fileio.exe
#	mono fileio.exe -input:input.txt -output:outfile.txt

stdin.exe: stdin.cs
	mcs stdin.cs

cmdline.exe: cmdline.cs
	mcs cmdline.cs

#meant for part C
#fileio.exe: fileio.cs
#	mcs fileio.cs

clean:
	$(RM) *.exe *.txt
