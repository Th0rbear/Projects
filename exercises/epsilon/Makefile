all: Out.txt
	cat Out.txt

Out.txt: main.exe
	mono main.exe > Out.txt

main.exe: main.cs epsilon.dll
	mcs main.cs -target:exe -reference:epsilon.dll
	#library file linked during compilation of main.cs

epsilon.dll: epsilon.cs
	mcs epsilon.cs -target:library -out:epsilon.dll
	#source code compiled and resulting bytecode is placed in library file epsilon.dll
