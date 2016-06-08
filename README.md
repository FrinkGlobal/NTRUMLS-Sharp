# NTRUMLS-Sharp
NTRUMLS C# Wrapper

## Dependencies
NTRUMLS C Source

Download source [here] (https://github.com/jschanck-si/NTRUMLS)

## Compiling NTRUMLS Shared C Library


### LINUX
`gcc -c -fpic src/crypto_hash512.c src/crypto_stream.c src/randombytes.c src/fastrandombytes.c src/shred.c src/convert.c src/pack.c src/pol.c src/params.c src/pqntrusign.c`

`gcc -shared -o libntrumls.so crypto_hash512.o crypto_stream.o randombytes.o fastrandombytes.o shred.o convert.o pack.o pol.o params.o pqntrusign.o`

### WINDOWS
`gcc -c -fpic src/crypto_hash512.c src/crypto_stream.c src/randombytes-vs.c src/fastrandombytes.c src/shred.c src/convert.c src/pack.c src/pol.c src/params.c src/pqntrusign.c`

`gcc -shared -o ntrumls.dll crypto_hash512.o crypto_stream.o randombytes-vs.o fastrandombytes.o shred.o convert.o pack.o pol.o params.o pqntrusign.o`
