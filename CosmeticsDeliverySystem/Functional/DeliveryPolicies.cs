namespace CosmeticsDeliverySystem.Functional;

public static class DeliveryPolicies
{
    public static decimal Standard(decimal distanceKm)
    {
        return 500m + 100m * distanceKm;
    }


public static decimal Express(decimal distanceKm)
    {
        return 800m + 150m * distanceKm;
    }


}
