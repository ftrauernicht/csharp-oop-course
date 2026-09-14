// Course 1 - Basics: a working reference for everything Chapter 1 covers.
// Type this in yourself as you go through the chapter -- this file is here
// so you can compare your own Program.cs once you're done, especially for
// the FizzBuzz challenge at the end.

Console.WriteLine("What's your name?");
var name = Console.ReadLine();

Console.WriteLine($"Hello, {name}!");

// --- Values and variables ---
int age = 16;
double price = 4.5;
bool isStudent = true;
string city = "Berlin";

Console.WriteLine($"{name} is {age} years old, lives in {city}, and is a student: {isStudent}");
Console.WriteLine($"A coffee here costs {price} EUR.");

// --- Operators ---
int score = 10;
score += 5; // 15
score *= 2; // 30
Console.WriteLine($"Score after bonuses: {score}");

bool isAdult = age >= 18;
string label = isAdult ? "adult" : "minor";
Console.WriteLine($"{name} counts as: {label}");

// --- A method (local function) ---
int Add(int a, int b)
{
    return a + b;
}

Console.WriteLine($"3 + 4 = {Add(3, 4)}");

// --- Decisions ---
int temperature = 8;
if (temperature < 10)
{
    Console.WriteLine("Wear a jacket.");
}
else if (temperature < 20)
{
    Console.WriteLine("A light sweater will do.");
}
else
{
    Console.WriteLine("Shorts weather!");
}

// --- Loops ---
for (int i = 1; i <= 5; i++)
{
    Console.WriteLine(i);
}

int count = 0;
while (count < 3)
{
    Console.WriteLine("Hello!");
    count++;
}

// --- Try it yourself: even numbers 1-10 ---
for (int i = 1; i <= 10; i++)
{
    if (i % 2 == 0)
    {
        Console.WriteLine(i);
    }
}

// --- Challenge: FizzBuzz ---
for (int i = 1; i <= 15; i++)
{
    if (i % 3 == 0 && i % 5 == 0)
    {
        Console.WriteLine("FizzBuzz");
    }
    else if (i % 3 == 0)
    {
        Console.WriteLine("Fizz");
    }
    else if (i % 5 == 0)
    {
        Console.WriteLine("Buzz");
    }
    else
    {
        Console.WriteLine(i);
    }
}
