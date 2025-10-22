# ft_linear_regression

A simple linear regression algorithm implementation in C#. This project demonstrates gradient descent optimization to fit a linear model to data.

## Description

This project implements a linear regression model from scratch using gradient descent. It includes:
- Training functionality with data normalization
- Prediction capabilities
- CSV data loading
- Model persistence (save/load)
- Comprehensive unit tests

## Features

- **Linear Regression Model**: Implementation using gradient descent algorithm
- **Data Normalization**: Automatic feature scaling for better convergence
- **Model Persistence**: Save and load trained models
- **Interactive CLI**: User-friendly command-line interface
- **Unit Tests**: Comprehensive test coverage with xUnit

## Requirements

- .NET 9.0 SDK or later

## Installation

1. Clone the repository:
```bash
git clone https://github.com/Azaleee/ft_linear_regression.git
cd ft_linear_regression
```

2. Build the project:
```bash
cd src/LinearRegression
dotnet build
```

## Usage

### Training the Model

To train the model on the provided dataset:

```bash
cd src/LinearRegression
dotnet run train
```

This will:
- Load data from `data/data.csv`
- Train the linear regression model
- Display the learned parameters (Theta0 and Theta1)
- Save the model to `model.txt`
- Show example predictions

### Making Predictions

To make a prediction using a trained model:

```bash
cd src/LinearRegression
dotnet run predict
```

Then enter the mileage when prompted.

### Interactive Mode

Run without arguments for an interactive menu:

```bash
cd src/LinearRegression
dotnet run
```

## Data Format

The training data should be in CSV format with two columns:
- First column: Input feature (e.g., mileage in km)
- Second column: Target value (e.g., price)

Example `data/data.csv`:
```csv
km,price
240000,3650
139800,3800
150500,4400
...
```

## Running Tests

To run the unit tests:

```bash
cd tests/LinearRegression.Tests
dotnet test
```

## Project Structure

```
ft_linear_regression/
├── data/
│   └── data.csv                    # Training data
├── src/
│   └── LinearRegression/
│       ├── LinearRegressionModel.cs # Core algorithm implementation
│       ├── DataLoader.cs            # CSV and model I/O utilities
│       ├── Program.cs               # CLI interface
│       └── LinearRegression.csproj  # Project file
└── tests/
    └── LinearRegression.Tests/
        ├── LinearRegressionModelTests.cs # Model tests
        ├── DataLoaderTests.cs            # Data loader tests
        └── LinearRegression.Tests.csproj # Test project file
```

## Algorithm Details

The implementation uses:
- **Gradient Descent**: Iterative optimization algorithm
- **Data Normalization**: Z-score normalization for features and targets
- **Mean Squared Error**: Cost function minimization
- **Configurable Hyperparameters**: Learning rate and iterations

The model learns parameters:
- `Theta0` (intercept): The base value
- `Theta1` (slope): The rate of change

Prediction formula: `y = Theta0 + Theta1 * x`

## Example Output

```
=== Training Model ===
Loading data from: ../../data/data.csv
Loaded 20 data points
Training model...

Training complete!
Theta0 (Intercept): 9323.56
Theta1 (Slope): -0.025036
Model saved to model.txt

Example predictions:
Mileage: 50000 km -> Predicted price: 8071.76
Mileage: 100000 km -> Predicted price: 6819.96
Mileage: 150000 km -> Predicted price: 5568.15
```

## License

MIT License
