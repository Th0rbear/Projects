all: out.integ.txt Fig.erf.png Fig.oscillator.png
	cat $< 

Fig.erf.png: fig.erf.gpi erf.data.txt
	pyxplot $<

Fig.oscillator.png: fig.oscillator.gpi oscillator.data.txt
	pyxplot $<

oscillator.data.txt: main-ode.exe
	mono $< 1> $@ 2> log

main-ode.exe: main-ode.cs matlib.dll

out.integ.txt erf.data.txt: main-integ.exe
	mono $< 1> out.integ.txt 2> erf.data.txt

main-integ.exe: main-integ.cs matlib.dll

LIBDIR = ~/repos/ppnm/exercises/matlib#directory to matlib
matlib.dll: \
	$(LIBDIR)/integrate.cs \
	$(LIBDIR)/ode.cs \
	$(LIBDIR)/vector.cs
	mcs -target:library -out:./$@ $^


%.exe: %.cs
		mcs -t:exe $(addprefix -r:,$(filter %.dll,$^)) $(filter %.cs,$^)

%.dll: %.cs
		mcs -t:library -out./$.dll $(addprefix -r:,$(filter %.dll,$^)) $(filter %.cs,$^)

clean:
	$(RM) *.dll *.exe *.txt *.png
