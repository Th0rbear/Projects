all: out.txt Fig.erf.png
	cat $<

Fig.erf.png: fig.erf.gpi erf.txt
	pyxplot $<

out.txt: main.exe
	mono $< > $@

main.exe: main.cs integrator.dll
	mcs $< -target:exe -reference:integrator.dll

integrator.dll: integrate.cs
	mcs -target:library -out:./$@ $<

clean:
	$(RM) *.dll *.exe *.txt
