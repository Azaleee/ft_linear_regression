namespace Trainer.Services;

public class LinearRegressionTrainer
{
    private double _theta0;
    private double _theta1;
    private readonly double _learningRate;
    private readonly int _iterations;

    public double Theta0 => _theta0;
    public double Theta1 => _theta1;

    public LinearRegressionTrainer(double learningRate = 0.01, int iterations = 1000)
    {
        _learningRate = learningRate;
        _iterations = iterations;
        _theta0 = 0;
        _theta1 = 0;
    }

    public void Train(List<CarData> data)
    {
        int m = data.Count;

        for (int iteration = 0; iteration < _iterations; iteration++)
        {
            double sumErrors = 0;
            double sumErrorsWeighted = 0;

            foreach (var car in data)
            {
                double prediction = _theta0 + (_theta1 * car.km);
                double error = prediction - car.price;
                sumErrors += error;
                sumErrorsWeighted += error * car.km;
            }

            double tmpTheta0 = _learningRate * (sumErrors / m);
            double tmpTheta1 = _learningRate * (sumErrorsWeighted / m);

            _theta0 = _theta0 - tmpTheta0;
            _theta1 = _theta1 - tmpTheta1;

            if (iteration % 100 == 0)
            {
                Console.WriteLine($"Iteration {iteration}: θ0={_theta0:F6}, θ1={_theta1:F6}");
            }
        }
    }
}
