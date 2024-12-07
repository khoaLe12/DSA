using Queuee = Queue.Queue;

var queue = new Queuee();

queue.Enqueue(5);
queue.Enqueue(6);
queue.Enqueue(7);
queue.Enqueue(8);
queue.Enqueue(9);
queue.Enqueue(10);

Console.WriteLine($"Count: {queue.Count()}\n");

Console.WriteLine($"Peek: {queue.Peek()?.Data}\n");

Console.WriteLine($"Dequeue 1st: {queue.Dequeue()?.Data}\n");

Console.WriteLine($"Dequeue 2nd: {queue.Dequeue()?.Data}\n");

Console.WriteLine($"Dequeue 3th: {queue.Dequeue()?.Data}\n");

Console.WriteLine($"Dequeue 4th: {queue.Dequeue()?.Data}\n");

Console.WriteLine($"Dequeue 5th: {queue.Dequeue()?.Data}\n");

Console.WriteLine($"Count: {queue.Count()}\n");

Console.WriteLine($"Contains 10: {queue.Contains(10)}\n");

Console.WriteLine($"Contains 9: {queue.Contains(9)}\n");

Console.WriteLine($"Contains 8: {queue.Contains(8)}\n");

queue.Clear();
Console.WriteLine($"Count after clear: {queue.Count()}\n");