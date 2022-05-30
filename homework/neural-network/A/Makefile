all: out.txt Fig.plot.png
	cat $<

Fig.plot.png: fig.plot.gpi out.txt
	pyxplot $<

out.txt: main.exe
	mono $< > $@

main.exe: main.cs matlib.dll
	mcs $< -target:exe -reference:matlib.dll

LIBDIR = ~/repos/ppnm/exercises/matlib
matlib.dll: \
	$(LIBDIR)/matrix.cs \
	$(LIBDIR)/vector.cs \
	$(LIBDIR)/minim.cs \
	network.cs
	mcs -target:library -out:./$@ $^

clean:
	$(RM) *.dll *.exe *.txt *.png
