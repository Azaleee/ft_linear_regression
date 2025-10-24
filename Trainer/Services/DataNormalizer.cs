using Trainer.Models;
using static Trainer.Utils.ModelSaver;

namespace Trainer.Services;

/// <summary>
/// Normalizes data to improve training convergence
/// </summary>
public class DataNormalizer
{
    public double MinFeature { get; private set; }
    public double MaxFeature { get; private set; }
    public double MinTarget { get; private set; }
    public double MaxTarget { get; private set; }

    /// <summary>
    /// Normalizes all samples to [0, 1] range
    /// </summary>
    public List<Sample> Normalize(List<Sample> data)
    {
        if (data.Count == 0)
        throw new InvalidOperationException("Cannot normalize empty dataset");

        MinFeature = data.Min(s => s.Feature);
        MaxFeature = data.Max(s => s.Feature);
        MinTarget = data.Min(s => s.Target);
        MaxTarget = data.Max(s => s.Target);

        if (MaxFeature - MinFeature == 0)
            throw new InvalidOperationException("All feature values are identical. Cannot normalize.");

        if (MaxTarget - MinTarget == 0)
            throw new InvalidOperationException("All target values are identical. Cannot normalize.");

        var normalizedData = data.Select(sample => new Sample
        {
            Feature = (sample.Feature - MinFeature) / (MaxFeature - MinFeature),
            Target = (sample.Target - MinTarget) / (MaxTarget - MinTarget)
        }).ToList();

        return normalizedData;
    }

    /// <summary>
    /// Converts normalized theta values back to original scale
    /// </summary>
    public LinRegModel Denormalize(LinRegModel model)
    {
        double theta1 = model.Theta1 * (MaxTarget - MinTarget) / (MaxFeature - MinFeature);
        double theta0 = MinTarget + model.Theta0 * (MaxTarget - MinTarget) - theta1 * MinFeature;

        return new LinRegModel(theta0, theta1);
    }
}
