all: gamma.pyxplot.png lngamma.pyxplot.png

gamma.pyxplot.png: gamma.data.txt gamma.tab.txt Makefile
	echo "\
	set terminal png \n\
	set output '$@' \n\
	set xlabel 'x' \n\
	set ylabel 'y' \n\
	set title 'gamma function' \n\
	set key left top \n\
	plot '$<' index 0 using 1:2 with lines col 2 title 'gamma',\
	'gamma.tab.txt' with points pointtype 17 col 3 title 'tab' \n\
	"| tee log.gpi | pyxplot

lngamma.pyxplot.png: lngamma.data.txt Makefile
	echo "\
	set terminal png \n\
	set output '$@' \n\
	set xlabel 'x' \n\
	set ylabel 'y' \n\
	set title 'lngamma function' \n\
	set key right top \n\
	plot '$<' index 0 using 1:2 with lines col 2 title 'lngamma' \n\
	"| tee log2.gpi | pyxplot

lngamma.data.txt: lngamma.exe
	mono $< > $@

gamma.data.txt: gamma.exe
	mono $< > $@

lngamma.exe: lngamma.cs
	mcs $<

%.exe: %.cs
	mcs $<

clean:
	$(RM) *.png log* *.exe *.txt
