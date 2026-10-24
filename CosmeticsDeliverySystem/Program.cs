using CosmeticsDeliverySystem.Data;
using CosmeticsDeliverySystem.Functional;
using CosmeticsDeliverySystem.Models;
using CosmeticsDeliverySystem.Services;

namespace CosmeticsDeliverySystem;

internal class Program
{
    private static void Main()
    {
        var orders = SampleData.GetOrders();


Console.WriteLine("=== COSMETICS DELIVERY SYSTEM ===");
        Console.WriteLine();

        Console.WriteLine("--- VALIDATION RESULTS ---");

        foreach (var order in orders)
        {
            if (OrderValidator.IsValid(order))
            {
                Console.WriteLine($"Order #{order.OrderId}: Accepted");
            }
            else
            {
                Console.WriteLine(
                    $"Order #{order.OrderId}: Rejected - " +
                    OrderValidator.GetErrorMessage(order));
            }
        }

        Console.WriteLine();

        RunPolicy("STANDARD DELIVERY", DeliveryPolicies.Standard, orders);
        RunPolicy("EXPRESS DELIVERY", DeliveryPolicies.Express, orders);

        Console.WriteLine();
        Console.WriteLine("Program finished.");


}


    private static void RunPolicy(
        string policyName,
        Func<decimal, decimal> policy,
        IReadOnlyList<DeliveryRequest> orders)
    {
        Console.WriteLine($"--- {policyName} ---");

        var quotes = DeliveryPipeline.Run(orders, policy);

        foreach (var quote in quotes)
        {
            Console.WriteLine(
                $"Order #{quote.OrderId} | " +
                $"Customer: {quote.CustomerName} | " +
                $"Cosmetics: {quote.OrderAmount:F2} KZT | " +
                $"Delivery: {quote.DeliveryCost:F2} KZT | " +
                $"Total: {quote.TotalAmount:F2} KZT");
        }

        Console.WriteLine($"Accepted orders: {quotes.Count}");
        Console.WriteLine();
    }


}
