// See https://aka.ms/new-console-template for more information
using Stackk = Stack.Stack;

var stack = new Stackk();

stack.Push(5);
stack.Push(6);
stack.Push(7);

Console.WriteLine($"Count: {stack.Count()}");

Console.WriteLine($"Pop first: {stack.Pop()?.Data}");
Console.WriteLine($"Count again: {stack.Count()}\n");

Console.WriteLine($"Peek: {stack.Peek()?.Data}");
Console.WriteLine($"Count again: {stack.Count()}\n");

Console.WriteLine($"Pop second: {stack.Pop()?.Data}");
Console.WriteLine($"Count again: {stack.Count()}\n");

Console.WriteLine($"Check contains 5: {stack.Contains(5)}");
Console.WriteLine($"Check contains 6: {stack.Contains(6)}");
Console.WriteLine($"Check contains 7: {stack.Contains(7)}\n");

stack.Clear();
Console.WriteLine("Clear");
Console.WriteLine($"Count again: {stack.Count()}\n");

Console.WriteLine($"Pop third: {stack.Pop()?.Data}");
Console.WriteLine($"Count again: {stack.Count()}\n");

