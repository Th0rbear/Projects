all: out.txt
	cat $<
	
out.txt: main.exe pyintegrate.py
	mono $< > $@
	python3 pyintegrate.py >> $@

main.exe: main.cs integrator.dll
	mcs $< -target:exe -reference:integrator.dll

integrator.dll: integrate.cs
	mcs -target:library -out:./$@ $<

clean:
	$(RM) *.dll *.exe *.txt
