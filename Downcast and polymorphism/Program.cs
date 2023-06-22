using System;
namespace Downcast_and_polymorphism
{
    public abstract class Vehicle
    {
        public Vehicle()
        {
        }
        public Vehicle(int n)
        {
        }
        public abstract void Drive();

        public virtual void Display()
        {
            Console.WriteLine("Superclass Display method....");
        }
    }
    public class Car : Vehicle
    {

        public override void Drive()
        {
            Console.WriteLine("Pushing accelerator...");
        }



    }
    public class Rowboat : Vehicle
    {
        public override void Drive()
        {
            Console.WriteLine("Rowing...");
        }
        public override void Display()
        {
            Console.WriteLine("Rowboat Display method....");
        }
    }

    public class Motorcycle : Vehicle
    {
        private int engineCapacity;

        public Motorcycle(int engineCapacity)
        {
            this.engineCapacity = engineCapacity;
        }

        public override void Drive()
        {
            Console.WriteLine($"Revving the motorcycle's {engineCapacity}L engine...");
        }
    }

    public class Truck : Vehicle
    {
        protected int payloadCapacity;

        public Truck(int payloadCapacity)
        {
            this.payloadCapacity = payloadCapacity;
        }

        public override void Drive()
        {
            Console.WriteLine("Driving the truck...");
        }
    }

    public class PickupTruck : Truck
    {
        private bool isFourWheelDrive;

        protected int currentPayload;

        public PickupTruck(int payloadCapacity, int currentPayload, bool isFourWheelDrive)
            : base(payloadCapacity)
        {
            this.isFourWheelDrive = isFourWheelDrive;
            this.currentPayload = currentPayload;
        }

        public override void Drive()
        {
            if (!isFourWheelDrive)
            {
                Console.WriteLine("Driving this 2WD pickup truck...");
            }
            else
            {
                Console.WriteLine("Driving the 4WD pickup truck...");
            }

        }

        public override void Display()
        {
            Console.WriteLine("This Pickup is a REAL man's truck");
        }

        public void DisplayPayload()
        {
            Console.WriteLine($"Don't go over this Payload limit: {payloadCapacity}");
            if (payloadCapacity < currentPayload)
            {
                Console.WriteLine($"Bro! I just said not to go over the limit\nyou're {currentPayload - payloadCapacity} over!");
            }
            else
            {
                Console.WriteLine("Good driving...");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Vehicle v = new Motorcycle(4);//Superclass --- Derived class
            v.Drive();
            v.Display();
            Console.WriteLine();
            Vehicle lt = new PickupTruck(4800, 5900, false);//Superclass --- Derived class
            lt.Drive();
            lt.Display();

            if (lt is PickupTruck pickupTruck) // THIS 'IS' OPERATOR ALLOW ME TO USE DisplayPayload METHOD EVEN THOUGH THIS IS NOT IN THE SUPERCLASS
            {
                pickupTruck.DisplayPayload();
            }

        }
    }
}