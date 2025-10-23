using Trainer.Configuration;
using Trainer.Models;

namespace Trainer.Services;

public class LinearRegressionTrainer
{
    private double _theta0;
    private double _theta1;
    private readonly TrainingConfig _config;

    public double Theta0 => _theta0;
    public double Theta1 => _theta1;

    public LinearRegressionTrainer(TrainingConfig config)
    {
        _config = config;
        _theta0 = 0;
        _theta1 = 0;
    }

    /// <summary>
    /// Trains the linear regression model using gradient descent
    /// </summary>
    /// <param name="data">Training data samples</param>
    public void Train(List<Sample> data)
    {
        int m = data.Count;

        for (int iteration = 0; iteration < _config.Iterations; iteration++)
        {
            double sumErrors = 0;
            double sumErrorsWeighted = 0;
            double cost = 0;

            foreach (var sample in data)
            {
                double feature = sample.Feature;
                double target = sample.Target;

                double prediction = _theta0 + (_theta1 * feature);
                double error = prediction - target;
                sumErrors += error;
                sumErrorsWeighted += error * feature;

                if (_config.Verbose)
                    cost += error * error;
            }

            double tmpTheta0 = _config.LearningRate * (sumErrors / m);
            double tmpTheta1 = _config.LearningRate * (sumErrorsWeighted / m);

            _theta0 = _theta0 - tmpTheta0;
            _theta1 = _theta1 - tmpTheta1;

            if (iteration % _config.DisplayEvery == 0)
            {
                if (_config.Verbose)
                {
                    cost = cost / (2 * m);
                    Console.WriteLine($"Iteration {iteration}: θ0={_theta0:F6}, θ1={_theta1:F6}, Cost={cost:F6}");
                }
                else
                {
                    Console.WriteLine($"Iteration {iteration}: θ0={_theta0:F6}, θ1={_theta1:F6}");
                }
            }
        }
    }
}
