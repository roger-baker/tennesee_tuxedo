CC=mcs
CFLAGS=-pkg:dotnet -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:./MySql.Data.dll -target:winexe

.PHONY : clean


equip.exe: equip.cs FilterWindow.dll AddEquip.dll AddFilter.dll MechManager.dll FilterListManager.dll DotWindow.dll DotList.dll
	$(CC) $(CFLAGS) -target:winexe -r:./FilterWindow.dll -r:./AddEquip.dll -r:./AddFilter.dll -r:./MechManager.dll -r:./FilterListManager.dll -r:./DotList.dll -r:./DotWindow.dll equip.cs -out:equip.exe

FilterWindow.dll: FilterWindow.cs
	$(CC) $(CFLAGS) -target:library FilterWindow.cs -out:FilterWindow.dll

AddFilter.dll: AddFilter.cs
	$(CC) $(CFLAGS) -target:library AddFilter.cs -out:AddFilter.dll

AddEquip.dll: AddEquip.cs AddFilter.dll
	$(CC) $(CFLAGS) -target:library -r:./AddFilter.dll AddEquip.cs -out:AddEquip.dll

MechManager.dll: MechManager.cs
	$(CC) $(CFLAGS) -target:library MechManager.cs -out:MechManager.dll

FilterListManager.dll:	FilterListManager.cs
	$(CC) $(CFLAGS) -target:library FilterListManager.cs -out:FilterListManager.dll
DotList.dll:	dot_list.cs
	$(CC) $(CFLAGS) -target:library dot_list.cs -out:DotList.dll
DotWindow.dll:	dot.cs
	$(CC) $(CFLAGS) -target:library dot.cs -out:DotWindow.dll

clean :
	rm *.exe
	rm *.dll
