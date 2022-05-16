all: out.txt Fig.hydrogen.png Fig.convergence.png Fig.convergence-ODE.png
	cat $<

Fig.convergence-ODE.png: fig.convergence-ODE.gpi convergence-ODE.txt
	pyxplot $<

Fig.convergence.png: fig.convergence.gpi convergence.txt
	pyxplot $<

Fig.hydrogen.png: fig.hydrogen.gpi hydrogen.txt
	pyxplot $<

out.txt: main.exe
	mono $< > $@

main.exe: main.cs matlib.dll
	mcs $< -target:exe -reference:matlib.dll

LIBDIR = ~/repos/ppnm/exercises/matlib
matlib.dll: \
	$(LIBDIR)/matrix.cs \
	$(LIBDIR)/vector.cs \
	$(LIBDIR)/QRGSdecom.cs \
	~/repos/ppnm/homework/ODE/A/ODEsolver.cs \
	roots.cs
	mcs -target:library -out:./$@ $^

clean:
	$(RM) *.dll *.exe *.txt *.png
