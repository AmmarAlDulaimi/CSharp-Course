
public abstract class Shape
{
    public abstract double Area { get; }
    public string color { get; set; }
    public string type { get; set; }
}

public class Circle : Shape
{
    public double radius { get; set; }
    public Circle(double radius, string color)
    {
        this.radius = radius;
        this.color = color;
        this.type = "Circle";
    }
    public override double Area => Math.PI * radius * radius;

}

public class Square : Shape
{
    public double side { get; set; }
    public Square(double side, string color)
    {
        this.side = side;
        this.color = color;
        this.type = "Square";
    }
    public override double Area => side * side;
}

public class Program
{
    public static void Main(string[] args)
    {

        List<Shape> shapes = new List<Shape>();
        shapes.Add(new Circle(5, "Red"));
        shapes.Add(new Circle(10, "Blue"));
        shapes.Add(new Square(10, "Yellow"));
        shapes.Add(new Square(20, "Blue"));

        Console.WriteLine("Examples for OOP:");
        Console.WriteLine("=====================================");

        foreach (Shape shape in shapes)
        {
            Console.WriteLine("===============================================================");
            Console.WriteLine($"Area of {shape.type} with color {shape.color} is {shape.Area}");
        }
    }
}