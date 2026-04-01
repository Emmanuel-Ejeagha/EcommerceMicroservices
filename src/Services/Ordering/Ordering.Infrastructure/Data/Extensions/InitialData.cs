namespace Ordering.Infrastructure.Data.Extensions;

internal class InitialData
{
    public static IEnumerable<Customer> Customers =>
    new List<Customer>
    {
        Customer.Create(CustomerId.Of(new Guid("01eaa5cb-daa5-4458-8918-22d261dff344")), "John Doe", "JohnDoe@email.com"),
        Customer.Create(CustomerId.Of(new Guid("7073c293-e143-46f9-9602-60506e0a5510")), "Jane Doe", "JaneDoe@email.com")        
    };

    public static IEnumerable<Product> Products =>
    new List<Product>
    {
        Product.Create(ProductId.Of(new Guid("38889291-3f69-40dc-8d6a-809c70c3472b")), "IPhone 14 Pro", 999.99m),
        Product.Create(ProductId.Of(new Guid("4e171760-4e4a-49b5-88fd-a5038f27576e")), "Infinix Hot 40i", 149.99m),
        Product.Create(ProductId.Of(new Guid("e5d958a8-13e0-4968-9ee1-198a077bb405")), "Tecno Spark 10 Pro", 129.99m),
        Product.Create(ProductId.Of(new Guid("ed9f1dfb-41fe-4d8f-a693-0f6265bed652")), "Samsung Galaxy S23 Ultra", 1199.99m)
    };

    public static IEnumerable<Order> OrderWithItems
    {
        get
        {
            var address1 = Address.Of("John", "Doe", "JohnDoe@email.com", "Zik Avenue", "Nigeria", "Enugu", "New Layout", "11032");
            var address2 = Address.Of("Jane", "Doe", "JaneDoe@email.com", "Medford", "USA", "Texas", "Medford", "20032");

            var payment1 = Payment.Of("John Doe", "1234567890123456", "12/25", "123", 1);
            var payment2 = Payment.Of("Jane Doe", "8901234561234567", "12/26", "345", 2);

            var order1 = Order.Create(
                            OrderId.Of(Guid.NewGuid()),
                            CustomerId.Of(new Guid("01eaa5cb-daa5-4458-8918-22d261dff344")),
                            OrderName.Of("ORD_1"),
                            shippingAddress: address1,
                            billingAddress: address1,
                            payment1);
            order1.Add(ProductId.Of(new Guid("38889291-3f69-40dc-8d6a-809c70c3472b")), 2, 4332.4m);
            order1.Add(ProductId.Of(new Guid("4e171760-4e4a-49b5-88fd-a5038f27576e")), 1, 500m);

            var order2 = Order.Create(
                            OrderId.Of(Guid.NewGuid()),
                            CustomerId.Of(new Guid("7073c293-e143-46f9-9602-60506e0a5510")),
                            OrderName.Of("ORD_2"),
                            shippingAddress: address2,
                            billingAddress: address2,
                            payment2);
            order2.Add(ProductId.Of(new Guid("e5d958a8-13e0-4968-9ee1-198a077bb405")), 2, 332.4m);
            order2.Add(ProductId.Of(new Guid("ed9f1dfb-41fe-4d8f-a693-0f6265bed652")), 1, 600m);

            return new List<Order> { order1, order2 };
        }
    }
}
