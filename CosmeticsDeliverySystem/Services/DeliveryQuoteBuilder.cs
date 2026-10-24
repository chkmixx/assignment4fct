using CosmeticsDeliverySystem.Functional;
using CosmeticsDeliverySystem.Models;

namespace CosmeticsDeliverySystem.Services;

public static class DeliveryQuoteBuilder
{
    public static DeliveryQuote CreateQuote(
    DeliveryRequest order,
    Func<decimal, decimal> deliveryPolicy)
    {
        decimal deliveryCost = DeliveryCalculator.Calculate(
        order.DistanceKm,
        deliveryPolicy
        );


    decimal totalAmount = order.OrderAmount + deliveryCost;

        string deliveryType = order.IsExpress
            ? "Express"
            : "Standard";

        return new DeliveryQuote(
            order.OrderId,
            order.CustomerName,
            order.OrderAmount,
            deliveryCost,
            totalAmount,
            deliveryType
        );
    }


}
