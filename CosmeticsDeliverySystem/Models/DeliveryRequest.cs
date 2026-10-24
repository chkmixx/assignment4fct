namespace CosmeticsDeliverySystem.Models;

public record DeliveryRequest(
int OrderId,
string CustomerName,
decimal OrderAmount,
double DistanceKm,
bool IsExpress
);
