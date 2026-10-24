namespace CosmeticsDeliverySystem.Models;

public record DeliveryQuote(
int OrderId,
string CustomerName,
decimal OrderAmount,
decimal DeliveryCost,
decimal TotalAmount,
string DeliveryType
);
