using CosmeticsDeliverySystem.Models;

namespace CosmeticsDeliverySystem.Data;

public static class SampleData
{
    public static IReadOnlyList<DeliveryRequest> GetOrders()
    {
        return new List<DeliveryRequest>
{
new(1, "Aruzhan", 10000m, 2.0, false),
new(2, "Dana", 15000m, 5.0, true),
new(3, "Amina", 7000m, 0.0, false),
new(4, "Madina", 12000m, -3.0, false),
new(5, "Aliya", 5000m, 1.0, true)
}.AsReadOnly();
    }
}
