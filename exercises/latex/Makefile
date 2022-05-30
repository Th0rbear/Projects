all: ms.pdf expfun.pdf

ms.pdf: ms.tex
	pdflatex $<
	pdflatex $<
#	bibtex $<
#	pdflatex $<

expfun.pdf: expfun.tex
	pdflatex $<
	pdflatex $<

out.sfuns.txt:main.exe
	mono $< > $@

main.exe: main.cs sfuns.dll

%.exe: %.cs
	mcs -t:exe $(addprefix -r:,$(filter %.dll,$^)) $(filter %.cs,$^)

%.dll: %.cs
	mcs -t:library $(addprefix -r:,$(filter %.dll,$^)) $(filter %.cs,$^)

clean: 
	$(RM) *.dll *.exe *.txt *.pdf
