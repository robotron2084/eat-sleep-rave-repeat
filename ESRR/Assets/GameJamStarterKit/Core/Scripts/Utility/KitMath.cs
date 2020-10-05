namespace GameJamStarterKit
{
    public static class KitMath
    {
        public static float NormalizeToRange(float currentValue,
            float currentMin,
            float currentMax,
            float targetMin,
            float targetMax)
        {
            var currentRange = currentMax - currentMin;
            var targetRange = targetMax - targetMin;
            var currentValueDistance = currentValue - currentMin;

            return currentValueDistance * targetRange / currentRange + targetMin;
        }
    }
}