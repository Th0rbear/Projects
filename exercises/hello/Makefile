MCS = mcs -optimize+ -platform:arm

out.txt : hello.exe                                              # out.txt depends on hello.exe
	mono hello.exe > out.txt                                 # run hello.exe, send output to out.txt

hello.exe : hello.cs                                             # hello.exe depends on hello.cs
	$ (MCS) hello.cs -target:exe -out:hello.exe              # compile hello.cs into hello.exe

clean:                                                           # a phoney target, no dependencies
	rm -f out.text hello.exe                                 # remove secondary files
test:
	echo $ (MCS)
