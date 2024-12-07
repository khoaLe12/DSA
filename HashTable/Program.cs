// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

int a = 10;

unsafe
{
    int* p = &a;
    Console.WriteLine("Address: " + new IntPtr(p));
}