all: out.txt Fig.higgs.png
	cat $<

Fig.higgs.png: fig.higgs.gpi higgs.fit.txt
	pyxplot $<

out.txt: main.exe higgs.data.txt
	mono $< < higgs.data.txt 1> $@ 2> log

main.exe: main.cs matlib.dll
	mcs $< -target:exe -reference:matlib.dll

LIBDIR = ~/repos/ppnm/exercises/matlib
GENDIR = ~/repos/ppnm/homework/genlist/B
matlib.dll: \
	$(LIBDIR)/matrix.cs \
	$(LIBDIR)/vector.cs \
	$(GENDIR)/genlist.cs \
	minim.cs
	mcs -target:library -out:./$@ $^

clean:
	$(RM) *.dll *.exe *.txt *.png
