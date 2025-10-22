using LinearRegression;

namespace LinearRegression
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Linear Regression in C# ===\n");

            if (args.Length > 0 && args[0] == "predict")
            {
                RunPrediction();
            }
            else if (args.Length > 0 && args[0] == "train")
            {
                RunTraining();
            }
            else
            {
                ShowMenu();
            }
        }

        static void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. Train model");
                Console.WriteLine("2. Make prediction");
                Console.WriteLine("3. Exit");
                Console.Write("\nYour choice: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RunTraining();
                        break;
                    case "2":
                        RunPrediction();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void RunTraining()
        {
            try
            {
                Console.WriteLine("\n=== Training Model ===");
                
                // Get the data file path
                string dataPath = "../../data/data.csv";
                
                // If not found, try other possible locations
                if (!File.Exists(dataPath))
                {
                    dataPath = "../../../data/data.csv";
                }
                
                if (!File.Exists(dataPath))
                {
                    dataPath = "data/data.csv";
                }

                Console.WriteLine($"Loading data from: {dataPath}");
                var (x, y) = DataLoader.LoadFromCsv(dataPath);
                Console.WriteLine($"Loaded {x.Length} data points");

                // Train the model
                var model = new LinearRegressionModel();
                Console.WriteLine("Training model...");
                model.Train(x, y, learningRate: 0.01, iterations: 1000);

                Console.WriteLine($"\nTraining complete!");
                Console.WriteLine($"Theta0 (Intercept): {model.Theta0:F2}");
                Console.WriteLine($"Theta1 (Slope): {model.Theta1:F6}");

                // Save the model
                string modelPath = "model.txt";
                DataLoader.SaveModel(modelPath, model.Theta0, model.Theta1);
                Console.WriteLine($"Model saved to {modelPath}");

                // Show some example predictions
                Console.WriteLine("\nExample predictions:");
                Console.WriteLine($"Mileage: 50000 km -> Predicted price: {model.Predict(50000):F2}");
                Console.WriteLine($"Mileage: 100000 km -> Predicted price: {model.Predict(100000):F2}");
                Console.WriteLine($"Mileage: 150000 km -> Predicted price: {model.Predict(150000):F2}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during training: {ex.Message}");
            }
        }

        static void RunPrediction()
        {
            try
            {
                Console.WriteLine("\n=== Making Prediction ===");
                
                // Load the model
                string modelPath = "model.txt";
                if (!File.Exists(modelPath))
                {
                    Console.WriteLine("Model file not found. Please train the model first.");
                    return;
                }

                var (theta0, theta1) = DataLoader.LoadModel(modelPath);
                Console.WriteLine($"Model loaded (Theta0: {theta0:F2}, Theta1: {theta1:F6})");

                // Get input from user
                Console.Write("\nEnter mileage (km): ");
                string? input = Console.ReadLine();
                
                if (double.TryParse(input, out double mileage))
                {
                    double prediction = theta0 + theta1 * mileage;
                    Console.WriteLine($"\nPredicted price for {mileage} km: {prediction:F2}");
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during prediction: {ex.Message}");
            }
        }
    }
}
