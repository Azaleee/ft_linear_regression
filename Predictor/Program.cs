using Trainer.Utils;

namespace Predictor;

class Program
{
    static void Main(string[] args)
    {
        var model = new ModelSaver.LinRegModel(0.0, 0.0);
        try
        {
            model = ModelSaver.Load("model.txt");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }

        Console.WriteLine($"Theta0 : {model.Theta0} Theta1 : {model.Theta1}");

        Console.WriteLine("Enter mileage: ");
        string? input = Console.ReadLine();
        if (input == null)
        {
            Console.WriteLine("Read error.");
            return;
        }

        if (double.TryParse(input, out double mileage))
        {
            double estimatedPrice = model.Theta0 + (model.Theta1 * mileage);
            Console.WriteLine($"Estimated price : {estimatedPrice:F2} $");
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }
    }
}

