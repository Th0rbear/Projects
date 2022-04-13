all: out.txt Fig.scipyex.png
	cat $<

Fig.scipyex.png: fig.scipyex.gpi out.txt
	pyxplot $<

out.txt: main.exe
	mono $< > $@

main.exe: main.cs ODE.dll
	mcs $< -target:exe -reference:ODE.dll

LIBDIR = ~/repos/ppnm/exercises/matlib
GENDIR = ~/repos/ppnm/homework/genlist/B
ODE.dll: \
	$(LIBDIR)/vector.cs \
	$(GENDIR)/genlist.cs \
	ODEsolver.cs
	mcs -target:library -out:./$@ $^

clean:
	$(RM) *.dll *.exe *.txt
