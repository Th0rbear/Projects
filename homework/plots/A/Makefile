erf.pyxplot.png: erf.data.txt erf.tab.txt Makefile
		echo "\
		set terminal png \n\
		set output '$@' \n\
		set xlabel 'x' \n\
		set ylabel 'erf(x)' \n\
		set grid \n\
		set tics out \n\
		set title 'error function' \n\
		set key left top \n\
		plot '$<' index 0 using 1:2 with lines col 2 title 'erf',\
		'erf.tab.txt' with points col 1 title 'tab' \n\
		"| tee log.gpi | pyxplot	


erf.data.txt: A-Plots.exe
	mono $< > $@

%.exe: %.cs
	mcs $<

clean:
	$(RM) *.png log* *.exe *.txt
