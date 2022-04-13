all: out.txt Fig.oscillator.png Fig.scipyex.png
	cat $<

Fig.scipyex.png: fig.scipyex.gpi out.txt
	pyxplot $<

Fig.oscillator.png: fig.oscillator.gpi oscillator.txt
	pyxplot $<

out.txt: main.exe
	mono $< > $@

main.exe: main.cs matlib.dll
	mcs $< -target:exe -reference:matlib.dll

LIBDIR = ~/repos/ppnm/exercises/matlib
matlib.dll: \
	$(LIBDIR)/vector.cs \
	ODEsolver.cs
	mcs -target:library -out:./$@ $^

clean:
	$(RM) *.dll *.exe *.txt
