using Trainer.Models;

namespace Trainer.Services;

public class LinearRegressionTrainer<T> where T : ITrainableData
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

    public void Train(List<T> data)
    {
        int m = data.Count;

        for (int iteration = 0; iteration < _iterations; iteration++)
        {
            double sumErrors = 0;
            double sumErrorsWeighted = 0;

            foreach (var item in data)
            {
                double feature = item.GetFeature();
                double target = item.GetTarget();

                double prediction = _theta0 + (_theta1 * feature);
                double error = prediction - target;
                sumErrors += error;
                sumErrorsWeighted += error * feature;
            }

            double tmpTheta0 = _learningRate * (sumErrors / m);
            double tmpTheta1 = _learningRate * (sumErrorsWeighted / m);

            _theta0 = _theta0 - tmpTheta0;
            _theta1 = _theta1 - tmpTheta1;
        }
    }
}
