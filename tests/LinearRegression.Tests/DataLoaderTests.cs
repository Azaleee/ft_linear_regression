namespace LinearRegression.Tests;

public class DataLoaderTests
{
    private readonly string _testDataPath = Path.Combine(Path.GetTempPath(), "test_data.csv");
    private readonly string _testModelPath = Path.Combine(Path.GetTempPath(), "test_model.txt");

    [Fact]
    public void LoadFromCsv_WithValidFile_ShouldLoadData()
    {
        // Arrange
        File.WriteAllText(_testDataPath, "x,y\n1,2\n3,4\n5,6\n");

        // Act
        var (x, y) = DataLoader.LoadFromCsv(_testDataPath);

        // Assert
        Assert.Equal(3, x.Length);
        Assert.Equal(3, y.Length);
        Assert.Equal(1, x[0]);
        Assert.Equal(2, y[0]);
        Assert.Equal(5, x[2]);
        Assert.Equal(6, y[2]);

        // Cleanup
        File.Delete(_testDataPath);
    }

    [Fact]
    public void LoadFromCsv_WithNonExistentFile_ShouldThrowFileNotFoundException()
    {
        // Act & Assert
        Assert.Throws<FileNotFoundException>(() => 
            DataLoader.LoadFromCsv("nonexistent.csv"));
    }

    [Fact]
    public void LoadFromCsv_WithInvalidData_ShouldSkipInvalidLines()
    {
        // Arrange
        File.WriteAllText(_testDataPath, "x,y\n1,2\ninvalid,data\n3,4\n");

        // Act
        var (x, y) = DataLoader.LoadFromCsv(_testDataPath);

        // Assert - Should only load valid lines
        Assert.Equal(2, x.Length);
        Assert.Equal(2, y.Length);

        // Cleanup
        File.Delete(_testDataPath);
    }

    [Fact]
    public void SaveModel_ShouldWriteParametersToFile()
    {
        // Arrange
        double theta0 = 3.14;
        double theta1 = 2.71;

        // Act
        DataLoader.SaveModel(_testModelPath, theta0, theta1);

        // Assert
        Assert.True(File.Exists(_testModelPath));
        string content = File.ReadAllText(_testModelPath);
        Assert.Contains("3.14", content);
        Assert.Contains("2.71", content);

        // Cleanup
        File.Delete(_testModelPath);
    }

    [Fact]
    public void LoadModel_WithValidFile_ShouldLoadParameters()
    {
        // Arrange
        File.WriteAllText(_testModelPath, "1.5,2.5");

        // Act
        var (theta0, theta1) = DataLoader.LoadModel(_testModelPath);

        // Assert
        Assert.Equal(1.5, theta0);
        Assert.Equal(2.5, theta1);

        // Cleanup
        File.Delete(_testModelPath);
    }

    [Fact]
    public void LoadModel_WithNonExistentFile_ShouldThrowFileNotFoundException()
    {
        // Act & Assert
        Assert.Throws<FileNotFoundException>(() => 
            DataLoader.LoadModel("nonexistent_model.txt"));
    }

    [Fact]
    public void LoadModel_WithInvalidFormat_ShouldThrowInvalidDataException()
    {
        // Arrange
        File.WriteAllText(_testModelPath, "invalid data");

        // Act & Assert
        Assert.Throws<InvalidDataException>(() => 
            DataLoader.LoadModel(_testModelPath));

        // Cleanup
        File.Delete(_testModelPath);
    }

    [Fact]
    public void SaveAndLoad_ShouldPreserveModelParameters()
    {
        // Arrange
        double originalTheta0 = 5.678;
        double originalTheta1 = -0.123;

        // Act
        DataLoader.SaveModel(_testModelPath, originalTheta0, originalTheta1);
        var (loadedTheta0, loadedTheta1) = DataLoader.LoadModel(_testModelPath);

        // Assert
        Assert.Equal(originalTheta0, loadedTheta0, 6);
        Assert.Equal(originalTheta1, loadedTheta1, 6);

        // Cleanup
        File.Delete(_testModelPath);
    }
}
