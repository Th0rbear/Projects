all: out.data
	cat $<

out.data: main.exe input.data
	> $@
	echo '\nExercise "genlist" part A: simple generic list' >> $@
	echo '\ninput: irregular table:' >> $@
	cat input.data >> $@
	echo '\noutput: regualar table:' >> $@
	mono main.exe < input.data >> $@

main.exe: main.cs genlist.dll
	mcs main.cs -target:exe -reference:genlist.dll

genlist.dll: genlist.cs
	mcs genlist.cs -target:library -out:genlist.dll

clean:
	$(RM) *.dll *.exe *.data
