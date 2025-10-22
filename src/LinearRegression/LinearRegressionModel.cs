namespace LinearRegression;

/// <summary>
/// Simple linear regression implementation using gradient descent
/// </summary>
public class LinearRegressionModel
{
    private double _theta0; // Intercept
    private double _theta1; // Slope
    
    public double Theta0 => _theta0;
    public double Theta1 => _theta1;

    public LinearRegressionModel()
    {
        _theta0 = 0;
        _theta1 = 0;
    }

    /// <summary>
    /// Train the linear regression model using gradient descent
    /// </summary>
    /// <param name="x">Input features</param>
    /// <param name="y">Target values</param>
    /// <param name="learningRate">Learning rate for gradient descent</param>
    /// <param name="iterations">Number of iterations</param>
    public void Train(double[] x, double[] y, double learningRate = 0.01, int iterations = 1000)
    {
        if (x.Length != y.Length)
        {
            throw new ArgumentException("X and Y arrays must have the same length");
        }

        int m = x.Length;

        // Normalize the data
        double xMean = x.Average();
        double xStd = CalculateStandardDeviation(x, xMean);
        
        double yMean = y.Average();
        double yStd = CalculateStandardDeviation(y, yMean);

        double[] xNorm = x.Select(val => (val - xMean) / xStd).ToArray();
        double[] yNorm = y.Select(val => (val - yMean) / yStd).ToArray();

        double theta0 = 0;
        double theta1 = 0;

        // Gradient descent
        for (int i = 0; i < iterations; i++)
        {
            double sumError0 = 0;
            double sumError1 = 0;

            for (int j = 0; j < m; j++)
            {
                double prediction = theta0 + theta1 * xNorm[j];
                double error = prediction - yNorm[j];
                sumError0 += error;
                sumError1 += error * xNorm[j];
            }

            theta0 -= learningRate * (sumError0 / m);
            theta1 -= learningRate * (sumError1 / m);
        }

        // Denormalize the parameters
        _theta1 = theta1 * (yStd / xStd);
        _theta0 = yMean - _theta1 * xMean;
    }

    /// <summary>
    /// Predict the value for a given input
    /// </summary>
    /// <param name="x">Input value</param>
    /// <returns>Predicted value</returns>
    public double Predict(double x)
    {
        return _theta0 + _theta1 * x;
    }

    private double CalculateStandardDeviation(double[] values, double mean)
    {
        double sumSquaredDiff = values.Sum(val => Math.Pow(val - mean, 2));
        return Math.Sqrt(sumSquaredDiff / values.Length);
    }
}
