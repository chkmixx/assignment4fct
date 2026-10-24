using CosmeticsDeliverySystem.Data;
using CosmeticsDeliverySystem.Functional;
using CosmeticsDeliverySystem.Models;
using CosmeticsDeliverySystem.Services;

namespace CosmeticsDeliverySystem.Tests;

public class UnitTest1
{
    
    [Fact]
    public void ValidOrder_ShouldPassValidation()
    {
        var order = new DeliveryRequest(
        1, "Aruzhan", 10000m, 2.0, false);


    Assert.True(OrderValidator.IsValid(order));
    }

    [Fact]
    public void NegativeDistance_ShouldFailValidation()
    {
        var order = new DeliveryRequest(
            4, "Madina", 12000m, -3.0, false);

        Assert.False(OrderValidator.IsValid(order));

        Assert.Equal(
            "Distance must be a finite non-negative number.",
            OrderValidator.GetErrorMessage(order));
    }
    [Fact]
    public void ZeroDistance_ShouldBeAccepted()
    {
        var order = new DeliveryRequest(
            3, "Amina", 7000m, 0.0, false);

        Assert.True(OrderValidator.IsValid(order));
    }

   
    [Fact]
    public void StandardAndExpress_ShouldProduceDifferentPrices()
    {
        decimal standard = DeliveryCalculator.Calculate(
            2.0, DeliveryPolicies.Standard);

        decimal express = DeliveryCalculator.Calculate(
            2.0, DeliveryPolicies.Express);

        Assert.Equal(700m, standard);
        Assert.Equal(1100m, express);
        Assert.NotEqual(standard, express);
    }

    
    [Fact]
    public void Pipeline_ShouldRejectInvalidOrder()
    {
        var orders = new List<DeliveryRequest>
    {
        new(1, "Aruzhan", 10000m, 2.0, false),
        new(4, "Madina", 12000m, -3.0, false)
    };

        var result = DeliveryPipeline.Run(
            orders, DeliveryPolicies.Standard);

        Assert.Single(result);
        Assert.Equal(1, result[0].OrderId);
    }

    
    [Fact]
    public void Pipeline_ShouldNotModifyOriginalCollection()
    {
        var orders = new List<DeliveryRequest>
    {
        new(1, "Aruzhan", 10000m, 2.0, false),
        new(3, "Amina", 7000m, 0.0, false)
    };

        int originalCount = orders.Count;
        var originalOrders = orders.ToArray();

        DeliveryPipeline.Run(
            orders, DeliveryPolicies.Standard);

        Assert.Equal(originalCount, orders.Count);
        Assert.Equal(originalOrders, orders);
    }

  
    [Fact]
    public void EmptyDataset_ShouldReturnEmptyResult()
    {
        var orders = new List<DeliveryRequest>();

        var result = DeliveryPipeline.Run(
            orders, DeliveryPolicies.Standard);

        Assert.Empty(result);
    }

    [Fact]
    public void MissingDataset_ShouldThrowException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            DeliveryPipeline.Run(
                null!, DeliveryPolicies.Standard));
    }


}
