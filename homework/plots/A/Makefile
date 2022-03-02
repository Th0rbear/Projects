erf.pyxplot.png: erf.data.txt erf.tab.txt Makefile
		echo "\
		set terminal png \n\
		set output '$@' \n\
		set xlabel 'x' \n\
		set ylabel 'y' \n\
		set title 'error function' \n\
		set key left top \n\
		plot '$<' index 0 using 1:2 with lines title 'erf',\
		'erf.tab.txt' with points title 'tab' \n\
		"| tee log.gpi | pyxplot	


erf.data.txt: A-Plots.exe
	mono $< > $@

%.exe: %.cs
	mcs $<

clean:
	$(RM) *.png log* *.exe *.txt
