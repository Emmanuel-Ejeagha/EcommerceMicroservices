namespace Ordering.Application.Orders.Queries.GetOrderByCustomer;

public record GetOrderByCustomerQuery(Guid CustomerId)
    : IQuery<GetOrdersByCustomerResult>;

public record GetOrdersByCustomerResult(IEnumerable<OrderDto> Orders);