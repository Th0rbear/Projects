all: out.cspline.txt Fig.cspline.png
	cat $<

Fig.cspline.png: fig.cspline.gpi out.cspline.txt
	pyxplot $<

out.cspline.txt: main.exe
	mono $< > out.cspline.txt

main.exe: main.cs cspline.dll
	mcs $< -target:exe -reference:cspline.dll

cspline.dll: cspline.cs
	mcs $< -target:library -out:$@

clean:
	$(RM) *.dll *.exe *.txt *.png
