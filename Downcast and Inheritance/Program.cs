using System;
using System.Collections.Generic;

namespace Downcast_and_Inheritance
{


class Animal
{
    public string Name { get; set; }

    public Animal(string name)
    {
        Name = name;
    }

    public virtual void MakeSound()
    {
        Console.WriteLine("Generic animal sound");
    }
}

class Dog : Animal
{
    public Dog(string name) : base(name)
    {

    }
    public override void MakeSound()
    {
        Console.WriteLine("Woof!");
    }

    public void Bark()
    {
        Console.WriteLine("Dog is barking");
    }
}

class Cat : Animal
{
    public Cat(string name) : base(name)
    {

    }
    public override void MakeSound()
    {
        Console.WriteLine("Meow!");
    }

    public void Purr()
    {
        Console.WriteLine("Cat is purring");
    }
}

class Lion : Animal
{

    public Lion(string name) : base(name)
    {

    }
    public override void MakeSound()
    {
        Console.WriteLine("Roar!");
    }

    public void Roar()
    {
        Console.WriteLine("This Lion is roaring!");
    }
    public void Ate(Animal animal)
    {
        Console.WriteLine($"This Lion ate a {animal.Name}!");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Animal lastAnimal = null;
        List<Animal> animals = new List<Animal>();
        Animal lion = new Lion("lion");
        animals.Add(new Dog("husky"));
        animals.Add(new Cat("black cat"));
        animals.Add(new Cat("white cat"));
        animals.Add(lion);

        foreach (Animal animal in animals)
        {
            animal.MakeSound();

            if (animal is Dog)
            {
                Dog dog = (Dog)animal; // Downcasting from Animal to Dog
                dog.Bark();
                lastAnimal = dog;// Accessing the specialized method of Dog
            }
            else if (animal is Cat)
            {
                Cat cat = (Cat)animal; // Downcasting from Animal to Cat
                cat.Purr();
                lastAnimal = cat;// Accessing the specialized method of Cat
            }
            else if (lion is Lion Lion) // Downcasting from Animal to Cat
            {
                Lion.Roar();

                Lion.Ate(lastAnimal);// Accessing the specialized method of Cat
            }

            Console.WriteLine();
        }
    }
}
}