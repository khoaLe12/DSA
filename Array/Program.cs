// See https://aka.ms/new-console-template for more information

using Array;
using ArrayInt = Array.Array<int>;

ArrayInt array = new ArrayInt(10);

array.Add(1);
array.Add(2);
array.Add(3);
array.Add(4);
array.Add(5);
array.Add(6);
array.Add(7);

Console.WriteLine($"At index 0: {array[0]}");
Console.WriteLine($"At index 1: {array[1]}");
Console.WriteLine($"At index 2: {array[2]}");
Console.WriteLine($"At index 3: {array[3]}");
Console.WriteLine($"At index 4: {array[4]}\n\n");

array[0] = 10;
Console.WriteLine($"Update at index 0 to 10: {array[0]}\n\n");


array.RemoveAt(0);
Console.WriteLine("Remove at index 0");
Console.WriteLine($"At index 0: {array[0]}");
Console.WriteLine($"At index 1: {array[1]}");
Console.WriteLine($"At index 2: {array[2]}");
Console.WriteLine($"At index 3: {array[3]}");
Console.WriteLine($"At index 4: {array[4]}\n\n");

array.Dispose();
Console.WriteLine("Clear array");
Console.WriteLine($"Count: {array.Count()}");
Console.WriteLine($"Capacity: {array.Capacity()}");