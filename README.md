# NTRUMLS-Sharp
NTRUMLS C# Wrapper

This wrapper implements an interface with [*NTRUMLS C*] (https://github.com/NTRUOpenSourceProject/NTRUMLS) It is currently under development the documentation is coming soon.

This wrapper intends to use only Mono 2.0  libraries to make it compatible with with other software packages such as [*Unity3d*] (http://unity3d.com/).   

### TODO
- [x] Generate Keys
- [x] Sign message Keys
- [x] Verify message
- [ ] Documentation

## Dependencies

### NTRUMLS C Source

Download NTRUMLS source [here] (https://github.com/NTRUOpenSourceProject/NTRUMLS)

### Mono

Download and install latest distribution [here] (http://www.mono-project.com/download/)

## Compiling NTRUMLS Shared C Library

In a shell terminal, navigate to the directory where you extracted NTRUMLS-master

for example

`cd ~/Downloads/NTRUMLS-master`

### LINUX
`gcc -c -fpic src/crypto_hash_sha512.c src/crypto_stream.c src/randombytes.c src/fastrandombytes.c src/shred.c src/convert.c src/pack.c src/pol.c src/params.c src/pqntrusign.c`

`gcc -shared -o libntrumls.so crypto_hash_sha512.o crypto_stream.o randombytes.o fastrandombytes.o shred.o convert.o pack.o pol.o params.o pqntrusign.o`

### WINDOWS
`gcc -c -fpic src\crypto_hash_sha512.c src\crypto_stream.c src\randombytes-vs.c src\fastrandombytes.c src\shred.c src\convert.c src\pack.c src\pol.c src\params.c src\pqntrusign.c`

`gcc -shared -o ntrumls.dll crypto_hash_sha512.o crypto_stream.o randombytes-vs.o fastrandombytes.o shred.o convert.o pack.o pol.o params.o pqntrusign.o`

## Compiling & Testing NTRUMLS-Sharp

In a shell terminal, navigate to the directory where you extracted NTRUMLS-Sharp

for example

`cd ~/Downloads/NTRUMLS-Sharp`

copy the compiled NTRUMLS shared library into NTRUMLS-Sharp directory

`cp ~/Downloads/NTRUMLS-master/libntrumls.so libntrumls.so`

compile the C# code

`mcs Program.cs ffi.cs params.cs NTRUMLSWrapper.cs`

execute the test program

`mono Program.exe`



# License

This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
