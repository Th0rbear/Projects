all: out.txt
	cat $<

out.txt: main.exe
	mono $< > $@

main.exe: main.cs matlib.dll
	mcs $< -target:exe -reference:matlib.dll

LIBDIR = ~/repos/ppnm/exercises/matlib
matlib.dll: \
	$(LIBDIR)/vector.cs \
	~/repos/ppnm/homework/plots/B/gamma.cs \
	mcintegrate.cs
	mcs -target:library -out:./$@ $^

clean:
	$(RM) *.dll *.exe *.txt
