# CosmeticsDeliverySystem
name:Tomiris Chekalin
group:IT-2503

# 1. Project Overview

Cosmetics Delivery System is a C# console application that calculates delivery costs for cosmetic orders.

The system validates orders, calculates delivery prices using configurable policies, and generates delivery quotes.

Intended users: cosmetics customers and delivery service operators.

# 2. Input Data

Each delivery request contains:

* OrderId: unique order identifier.
* CustomerName: customer's name.
* OrderAmount: total price of cosmetic products.
* DistanceKm: delivery distance in kilometres.
* IsExpress: indicates whether express delivery was requested.

# 3. Business Rules

An order is valid when:

* OrderId is greater than zero.
* CustomerName is not empty.
* OrderAmount is greater than zero.
* DistanceKm is finite and non-negative.

Zero kilometres is accepted as a boundary value.

Orders with invalid data are excluded from delivery calculations.

# 4. Delivery Policies

### Standard Delivery

Delivery cost = 500 + 100 × distance in kilometres.

### Express Delivery

Delivery cost = 800 + 150 × distance in kilometres.

Total amount = order amount + delivery cost.

Example for a 10,000 KZT order delivered over 2 kilometres:

* Standard delivery: 700 KZT.
* Express delivery: 1,100 KZT.
* Standard total: 10,700 KZT.
* Express total: 11,100 KZT.

# 5. Pipeline Stages

### Stage 1: Validation and Filtering

File: Services/OrderValidator.cs

Method: IsValid()

Input: DeliveryRequest.

Output: Boolean validation result.

Rule: Reject orders with invalid identifiers, names, amounts, or distances.

### Stage 2: Delivery Calculation

File: Services/DeliveryCalculator.cs

Method: Calculate()

Input: Distance and a delivery policy delegate.

Output: Delivery cost.

Rule: Invoke the supplied policy to calculate the delivery price.

### Stage 3: Quote Generation and Sorting

Files:

* Services/DeliveryQuoteBuilder.cs
* Functional/DeliveryPipeline.cs

Input: Valid delivery requests and a policy.

Output: DeliveryQuote records sorted by total amount in ascending order.

Rule: Calculate the delivery cost, create the quote, and sort the results.

# 6. Functional Programming

DeliveryPipeline.Run accepts a Func<decimal, decimal> policy.

The policy is passed from outside the pipeline. The same pipeline is used for Standard and Express delivery.

The transformation stages avoid console output and do not modify the original order collection.

# 7. Sample Dataset

The sample dataset contains five orders.

* Four orders are valid.
* One order has a negative distance and is rejected.
* One valid order has a distance of zero kilometres.

The program runs both delivery policies on the same dataset.

# 8. Automated Tests

Test file: CosmeticsDeliverySystem.Tests/UnitTest1.cs

The tests cover:

1. Valid order passes validation.
2. Negative distance is rejected.
3. Zero distance is accepted.
4. Standard and Express policies produce different prices.
5. Invalid orders are rejected by the pipeline.
6. The original collection remains unchanged.
7. An empty dataset returns an empty result.
8. A missing dataset throws an exception.
   <img width="1227" height="587" alt="image" src="https://github.com/user-attachments/assets/ca20b270-c8b3-467f-b7d0-01b051402f6f" />


Expected result: 8 tests passed.

# 9. Project Structure

* Models: data models.
* Services: validation and delivery calculations.
* Functional: delivery policies and pipeline.
* Data: sample orders.
* Program.cs: console application entry point.
* CosmeticsDeliverySystem.Tests: automated tests.


## 10. Expected Outcome
<img width="1562" height="625" alt="image" src="https://github.com/user-attachments/assets/cb34d793-93da-42cf-b9da-c37e7e01d64d" />


The application prints valid delivery quotes for both Standard and Express policies. Invalid orders are rejected during validation. The automated tests verify the principal calculation, validation, and pipeline behaviours.
