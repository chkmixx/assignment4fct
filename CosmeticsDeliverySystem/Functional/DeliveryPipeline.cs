using CosmeticsDeliverySystem.Models;
using CosmeticsDeliverySystem.Services;

namespace CosmeticsDeliverySystem.Functional;

public static class DeliveryPipeline
{
    public static IReadOnlyList<DeliveryQuote> Run(
    IReadOnlyList<DeliveryRequest> orders,
    Func<decimal, decimal> deliveryPolicy)
    {
        ArgumentNullException.ThrowIfNull(orders);
        ArgumentNullException.ThrowIfNull(deliveryPolicy);


    // Stage 1: Select valid orders.
    var validOrders = orders
        .Where(OrderValidator.IsValid)
        .ToList();

        // Stage 2: Calculate delivery costs and create quotes.
        var quotes = validOrders
            .Select(order => DeliveryQuoteBuilder.CreateQuote(
                order,
                deliveryPolicy))
            .ToList();

        // Stage 3: Sort quotes by total amount.
        var result = quotes
            .OrderBy(quote => quote.TotalAmount)
            .ToList();

        return result.AsReadOnly();
    }


}
