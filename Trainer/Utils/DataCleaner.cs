using Trainer.Models;

namespace Trainer.Utils;

public static class DataCleaner
{
    public static int RemoveOutliers(
        List<Sample> data,
        bool removeFeatureOutliers = true,
        bool removeTargetOutliers = true,
        double multiplier = 1.5)
    {
        if (data == null || data.Count == 0)
            throw new ArgumentException("Dataset is empty.");

        int originalCount = data.Count;

        double featureLower = double.MinValue;
        double featureUpper = double.MaxValue;
        if (removeFeatureOutliers)
        {
            var features = data.Select(s => s.Feature);
            (featureLower, featureUpper) = MathUtils.GetOutlierBounds(features, multiplier);
        }

        double targetLower = double.MinValue;
        double targetUpper = double.MaxValue;
        if (removeTargetOutliers)
        {
            var targets = data.Select(s => s.Target);
            (targetLower, targetUpper) = MathUtils.GetOutlierBounds(targets, multiplier);
        }

        data.RemoveAll(sample =>
        {
            bool featureIsOutlier = removeFeatureOutliers && 
                                   MathUtils.IsOutlier(sample.Feature, featureLower, featureUpper);
            bool targetIsOutlier = removeTargetOutliers && 
                                  MathUtils.IsOutlier(sample.Target, targetLower, targetUpper);

            return featureIsOutlier || targetIsOutlier;
        });

        return originalCount - data.Count;
    }
}
