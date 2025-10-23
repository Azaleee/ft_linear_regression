# ft_linear_regression

A simple implementation of **linear regression** in C#, built as part of the 42 school *WorkStudy* projects.

This project demonstrates how to train and use a linear regression model from scratch, using only basic math (no machine learning libraries). It’s divided into two CLI tools:

- **Trainer** → trains the model using a dataset and saves the parameters (theta₀, theta₁).
- **Predictor** → uses the trained model to make predictions on new input values.

---

## 📊 Project Structure
ft_linear_regression/
├── data.csv # Training dataset
├── house.csv # Example dataset
├── model.txt # Saved model parameters
├── regression_plot.png # Visualization of results
├── Trainer/ # Model training logic
│ ├── Configuration/ # Config & graph settings
│ ├── Models/ # Data model definitions
│ ├── Services/ # Data loader, normalizer, trainer
│ └── Utils/ # Model saver
├── Predictor/ # CLI tool for prediction
└── LinearRegression.sln # Solution file

---

## How it works

1. **Training phase**
   - The Trainer reads the dataset (`data.csv`).
   - It normalizes the data and applies **gradient descent** to find the best-fit line `y = θ₀ + θ₁x`.
   - Once converged, it saves the model parameters in `model.txt`.
   - Optionally, a plot (`regression_plot.png`) shows the line fitting the data.

2. **Prediction phase**
   - The Predictor loads `model.txt`.
   - It takes a new input value (e.g., mileage of a car) and predicts the target (e.g., price).

---

## Usage

### Train the model

From the root of the project:
```bash
dotnet run --project Trainer -- --data ../data.csv
