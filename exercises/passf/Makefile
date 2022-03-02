all: Out.txt
	cat Out.txt

Out.txt: main.exe
	mono main.exe > Out.txt

main.exe: main.cs passf.dll
	mcs main.cs -target:exe -reference:passf.dll

passf.dll: passf.cs
	mcs passf.cs -target:library -out:passf.dll
