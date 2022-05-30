all: out.txt Fig.radialwavefuncs.png Fig.rmaxconv.png Fig.drconv.png
	cat $<

Fig.drconv.png: fig.drconv.gpi dr-conv.txt
	pyxplot $<

Fig.rmaxconv.png: fig.rmaxconv.gpi rmax-conv.txt
	pyxplot $<

Fig.radialwavefuncs.png: fig.radialwavefuncs.gpi out.txt
	pyxplot $<

out.txt: main.exe
	mono $< > $@

main.exe: main.cs matlib.dll
	mcs $< -target:exe -reference:matlib.dll

LIBDIR = ~/repos/ppnm/exercises/matlib
matlib.dll: \
	$(LIBDIR)/matrix.cs \
	$(LIBDIR)/vector.cs \
	jacobi-evd.cs
	mcs -target:library -out:./$@ $^

clean:
	$(RM) *.dll *.exe *.txt *.png
