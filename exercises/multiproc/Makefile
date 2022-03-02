all: Out.txt
	cat Out.txt

Out.txt: main.exe Makefile
	/bin/time --portability --output Out.txt --append mono main.exe 2e8 > Out.txt

main.exe: main.cs
	mcs main.cs

clean:
	rm *.exe [Oo]ut*
