MCS = mcs -optimize+ -platform:arm
	
all: Out.txt
	cat Out.txt

Out.txt : hello.exe                                              # out.txt depends on hello.exe
	mono hello.exe > Out.txt                                 # run hello.exe, send output to out.txt

hello.exe : hello.cs                                             # hello.exe depends on hello.cs
	$(MCS) hello.cs -target:exe -out:hello.exe               # compile hello.cs into hello.exe

.PHONEY:clean
clean:                                                           # a phoney target, no dependencies
	rm -f *.txt *.exe                                 	 # remove secondary files
test:
	echo $(MCS)
