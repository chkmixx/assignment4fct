namespace CosmeticsDeliverySystem.Services;

public static class DeliveryCalculator
{
    public static decimal Calculate(
    double distanceKm,
    Func<decimal, decimal> deliveryPolicy)
    {
        decimal distance = (decimal)distanceKm;


    return deliveryPolicy(distance);
    }


}
