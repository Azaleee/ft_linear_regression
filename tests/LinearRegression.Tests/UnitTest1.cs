namespace LinearRegression.Tests;

public class LinearRegressionModelTests
{
    [Fact]
    public void Constructor_ShouldInitializeWithZeroParameters()
    {
        // Arrange & Act
        var model = new LinearRegressionModel();

        // Assert
        Assert.Equal(0, model.Theta0);
        Assert.Equal(0, model.Theta1);
    }

    [Fact]
    public void Train_WithSimpleLinearData_ShouldLearnCorrectParameters()
    {
        // Arrange
        var model = new LinearRegressionModel();
        // Perfect linear relationship: y = 2x + 1
        double[] x = { 1, 2, 3, 4, 5 };
        double[] y = { 3, 5, 7, 9, 11 };

        // Act
        model.Train(x, y, learningRate: 0.01, iterations: 1000);

        // Assert - the parameters should be close to theta0=1 and theta1=2
        Assert.InRange(model.Theta0, 0.5, 1.5);
        Assert.InRange(model.Theta1, 1.5, 2.5);
    }

    [Fact]
    public void Train_WithMismatchedArrays_ShouldThrowArgumentException()
    {
        // Arrange
        var model = new LinearRegressionModel();
        double[] x = { 1, 2, 3 };
        double[] y = { 1, 2 }; // Different length

        // Act & Assert
        Assert.Throws<ArgumentException>(() => model.Train(x, y));
    }

    [Fact]
    public void Predict_AfterTraining_ShouldReturnReasonableValues()
    {
        // Arrange
        var model = new LinearRegressionModel();
        double[] x = { 1, 2, 3, 4, 5 };
        double[] y = { 3, 5, 7, 9, 11 };
        model.Train(x, y, learningRate: 0.01, iterations: 1000);

        // Act
        double prediction = model.Predict(6);

        // Assert - For y = 2x + 1, when x=6, y should be 13
        Assert.InRange(prediction, 12, 14);
    }

    [Fact]
    public void Predict_WithoutTraining_ShouldReturnZero()
    {
        // Arrange
        var model = new LinearRegressionModel();

        // Act
        double prediction = model.Predict(5);

        // Assert
        Assert.Equal(0, prediction);
    }

    [Fact]
    public void Train_WithCarMileageData_ShouldLearnNegativeSlope()
    {
        // Arrange - Simulating car price decreasing with mileage
        var model = new LinearRegressionModel();
        double[] mileage = { 10000, 20000, 30000, 40000, 50000 };
        double[] price = { 10000, 9000, 8000, 7000, 6000 };

        // Act
        model.Train(mileage, price, learningRate: 0.01, iterations: 1000);

        // Assert - Slope should be negative (price decreases with mileage)
        Assert.True(model.Theta1 < 0, "Slope should be negative for decreasing relationship");
    }

    [Fact]
    public void Train_MultipleIterations_ShouldConverge()
    {
        // Arrange
        var model1 = new LinearRegressionModel();
        var model2 = new LinearRegressionModel();
        double[] x = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        double[] y = { 2.1, 4.2, 6.1, 8.3, 10.2, 12.1, 14.3, 16.1, 18.2, 20.1 };

        // Act
        model1.Train(x, y, learningRate: 0.01, iterations: 500);
        model2.Train(x, y, learningRate: 0.01, iterations: 2000);

        // Assert - More iterations should give similar or better results
        double prediction1 = model1.Predict(11);
        double prediction2 = model2.Predict(11);
        
        // Both should be close to 22 (continuing the pattern)
        Assert.InRange(prediction1, 20, 24);
        Assert.InRange(prediction2, 20, 24);
    }
}
