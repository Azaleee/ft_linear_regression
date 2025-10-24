namespace Trainer.Utils;

public static class MathUtils
{
    public static double Quantile(IEnumerable<double> sequence, double percentile)
    {
        if (sequence == null || !sequence.Any())
            throw new ArgumentException("Sequence is empty.");

        if (percentile < 0 || percentile > 100)
            throw new ArgumentOutOfRangeException(nameof(percentile));

        var sorted = sequence.OrderBy(x => x).ToArray();

        double pos = (sorted.Length - 1) * (percentile / 100.0);

        int i = (int)Math.Floor(pos);
        double frac = pos - i;

        if (i + 1 < sorted.Length)
            return sorted[i] + (sorted[i + 1] - sorted[i]) * frac;
        else
            return sorted[i];
    }

    public static (double q1, double q3, double iqr) Iqr(IEnumerable<double> sequence)
    {
        if (sequence == null || !sequence.Any())
            throw new ArgumentException("Sequence is empty.");

        double q1 = Quantile(sequence, 25);
        double q3 = Quantile(sequence, 75);
        double iqr = q3 - q1;

        return (q1, q3, iqr);
    }

    public static (double lowerBound, double upperBound) GetOutlierBounds(IEnumerable<double> sequence, double multiplier = 1.5)
    {
        var (q1, q3, iqr) = Iqr(sequence);

        double lowerBound = q1 - multiplier * iqr;
        double upperBound = q3 + multiplier * iqr;

        return (lowerBound, upperBound);
    }

    public static bool IsOutlier(double value, double lowerBound, double upperBound)
    {
        return value < lowerBound || value > upperBound;
    }
}
