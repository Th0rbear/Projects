all: out.txt
	cat $<

out.txt: main.exe
	mono $< > out.txt

main.exe: main.cs matlib.dll
	mcs $< -target:exe -reference:matlib.dll

LIBDIR = ~/repos/ppnm/exercises/matlib
matlib.dll: \
	$(LIBDIR)/matrix.cs \
	$(LIBDIR)/vector.cs \
	jacobi-evd.cs
	mcs -target:library -out:./$@ $^

clean:
	$(RM) *.dll *.exe *.txt
