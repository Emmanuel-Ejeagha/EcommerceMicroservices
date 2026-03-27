using BuildingBlocks.Pagination;

namespace Ordering.Application.Orders.Queries.GetQueries;

public record GetOrdersQuery(PaginationRequest PaginationRequest)
    : IQuery<GetOrdersResult>;

public record GetOrdersResult(PaginatedResult<OrderDto> Orders);