using System.Collections;
using HashTablee = HashTable.HashTable; 


int a = 10;
unsafe
{
    int* p = &a;
    //Console.WriteLine("Address: " + new IntPtr(p));
}


HashTablee hashTable = new HashTablee(3);
hashTable.Add("First Name", "Doe");
hashTable.Add("Last Name", "John");
hashTable.Add("Age", 32);
Console.WriteLine($"First name: {hashTable["First Name"]}");
Console.WriteLine($"Last name: {hashTable["Last Name"]}");
Console.WriteLine($"Age: {hashTable["Age"]}\n\n");


hashTable["First Name"] = "Doe 1";
hashTable["Last Name"] = "John 1";
hashTable["Age"] = 30;
Console.WriteLine($"Set new value: {hashTable["First Name"]}, {hashTable["Last Name"]}, {hashTable["Age"]}\n\n");


hashTable.Add("Job", "Software engineering");
hashTable.Add("Salary", 1000);
Console.WriteLine($"Job: {hashTable["Job"]}");
Console.WriteLine($"Salary: {hashTable["Salary"]}\n\n");


Console.WriteLine($"Hash table size: {hashTable.Size()}");
Console.WriteLine($"Hash table count: {hashTable.Count()}\n\n");


Console.WriteLine($"Contains keys 'Job': {hashTable.Contains("Job")}\n\n");


var salaryValue = hashTable["Salary"];
Console.WriteLine($"Contains value 'Salary object:1000': {hashTable.ContainsValue(salaryValue)}\n\n");
Console.WriteLine($"Contains value '1000': {hashTable.ContainsValue(1000)}\n\n");


hashTable.Remove("Job");
hashTable.Remove("Salary");
Console.WriteLine($"Removed Job and Salary, checked: {hashTable["Job"]}, {hashTable["Salary"]}");


Console.WriteLine($"Hash table size after remove: {hashTable.Size()}");
Console.WriteLine($"Hash table count after remove: {hashTable.Count()}\n\n");


hashTable.Clear();
Console.WriteLine($"Hash table size after clear: {hashTable.Size()}");
Console.WriteLine($"Hash table count after clear: {hashTable.Count()}\n\n");