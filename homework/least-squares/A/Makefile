all: logfit.txt out.txt Fig.lsfit.png
	cat $<

Fig.lsfit.png: fig.lsfit.gpi out.txt
	pyxplot $<

out.txt logfit.txt: main.exe
	mono $< 1> out.txt 2> logfit.txt

main.exe: main.cs matlib.dll
	mcs $< -target:exe -reference:matlib.dll

LIBDIR = ~/repos/ppnm/exercises/matlib
matlib.dll: \
	$(LIBDIR)/matrix.cs \
	$(LIBDIR)/vector.cs \
	$(LIBDIR)/QRGSdecom.cs \
	lsquares-fit.cs
	mcs -target:library -out:./$@ $^

clean:
	$(RM) *.dll *.exe *.txt
