all: out.txt
	cat $<

out.txt: main.exe 
	mono main.exe > out.txt

main.exe: main.cs genlist.dll
	mcs main.cs -target:exe -reference:genlist.dll

genlist.dll: genlist.cs
	mcs genlist.cs -target:library -out:genlist.dll

clean:
	$(RM) *.dll *.exe *.txt
