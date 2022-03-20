all: out.linspline.txt sin.data.txt Fig.linspline.png
	cat $<

Fig.linspline.png: fig.linspline.gpi sin.data.txt out.linspline.txt
	pyxplot $<

sin.data.txt out.linspline.txt: main.exe
	mono $< 1> sin.data.txt 2> out.linspline.txt

main.exe: main.cs linspline.dll
	mcs $< -target:exe -reference:linspline.dll

linspline.dll: linspline.cs
	mcs $< -target:library -out:$@

clean:
	$(RM) *.dll *.exe *.txt
