all: out.qspline.txt out.coefficients.txt Fig.qspline.png
		cat $<

Fig.qspline.png: fig.qspline.gpi out.qspline.txt
	pyxplot $<

out.coefficients.txt out.qspline.txt: main.exe
		mono $< 1> out.coefficients.txt 2> out.qspline.txt

main.exe: main.cs qspline.dll
		mcs $< -target:exe -reference:qspline.dll

qspline.dll: qspline.cs
		mcs $< -target:library -out:$@

clean:
		$(RM) *.dll *.exe *.txt *.png
