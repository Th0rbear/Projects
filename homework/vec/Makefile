all: Out.txt
	cat Out.txt

Out.txt: main.exe
	mono main.exe > Out.txt

main.exe: main.cs vec.dll
	mcs main.cs -target:exe -reference:vec.dll                                                                                                                                           
vec.dll: vec.cs 
	mcs vec.cs -target:library -out:vec.dll 

clean:
	$(RM) *.exe *.dll *.txt
