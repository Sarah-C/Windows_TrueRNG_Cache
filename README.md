# Windows: TrueRNG Cache
This is a simple threaded cache that collects random numbers while your program doesn't need them, so you're not waiting for the numbers when you do!

It's got functions that return integer values between two ranges, decimal values between two rangers, and a standard RND that returns a number between 0 and 0.9999999

This demo program takes the cached output from the TrueRNG class, and writes it to a file, byte by byte.
Once the file reaches a set size (2MB in the example), a new file is made and more numbers are stored. It continues until you close it.

TrueRNG.vb has comments for the functions you can use.

It's built using Visual Studio 2019, and there's the .exe in there if you just want a program to pull off random numbers into files for you to use.

![image](https://user-images.githubusercontent.com/1586332/169997760-c81497cb-df3c-4e66-b3b8-032e61e6f5c5.png)


