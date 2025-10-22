double theta0 = 0;
double theta1 = 0;

try
{
    string[] lines = File.ReadAllLines("../model.txt");
    theta0 = double.Parse(lines[0]);
    theta1 = double.Parse(lines[1]);
}
catch (Exception e)
{
    Console.WriteLine($"Error: {e.Message}");
}

Console.WriteLine("Enter mileage: ");
string? input = Console.ReadLine();
if (input == null)
{
    Console.WriteLine("Read error.");
    return;
}

if (double.TryParse(input, out double mileage))
{
    double estimatedPrice = theta0 + (theta1 * mileage);
    Console.WriteLine($"Estimated price : {estimatedPrice:F2} $");
}
else
{
    Console.WriteLine("Invalid input.");
}
