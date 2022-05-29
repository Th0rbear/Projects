all: Out.txt
	cat Out.txt

Out.txt: main.exe
	mono main.exe > Out.txt

main.exe: main.cs
	mcs main.cs

clean:
	$(RM) *.exe *.txt
