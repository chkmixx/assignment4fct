using CosmeticsDeliverySystem.Models;

namespace CosmeticsDeliverySystem.Services;

public static class OrderValidator
{
    public static bool IsValid(DeliveryRequest order)
    {
        return order is not null
        && order.OrderId > 0
        && !string.IsNullOrWhiteSpace(order.CustomerName)
        && order.OrderAmount > 0
        && double.IsFinite(order.DistanceKm)
        && order.DistanceKm >= 0;
    }


public static string GetErrorMessage(DeliveryRequest order)
    {
        if (order is null)
            return "Order is missing.";

        if (order.OrderId <= 0)
            return "Order ID must be greater than zero.";

        if (string.IsNullOrWhiteSpace(order.CustomerName))
            return "Customer name is required.";

        if (order.OrderAmount <= 0)
            return "Order amount must be greater than zero.";

        if (!double.IsFinite(order.DistanceKm) || order.DistanceKm < 0)
            return "Distance must be a finite non-negative number.";

        return "Order is valid.";
    }


}
