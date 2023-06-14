using System;

namespace AbstractClassExample
{
    public abstract class Shape
    {
        public abstract double area();
    }
    public class Square : Shape
    {
        private int side;
        public Square(int s) { side = s; }
        public override double area() { return side * side; }
    }

    public class Circle : Shape
    {
        private int radius;
        public Circle(int r) { radius = r; }
        public override double area()
        {
            return System.Math.PI * radius * radius;
        }
    }
    class ShapeTest
    {
        static void Main(string[] args)
        {
            Shape sq = new Square(10);
            Shape c = new Circle(5);
            System.Console.WriteLine("Area of square= " + sq.area());
            System.Console.WriteLine("Area of circle= " + c.area());
            Console.Read();
        }
    }
}
