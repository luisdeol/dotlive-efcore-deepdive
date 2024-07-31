using EfCoreDeepDive.API.Entities;

namespace EfCoreDeepDive.API.Models;

public class PagedProducts
{
    public List<Product> Products { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }

    public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;

    public PagedProducts(List<Product> products, int pageNumber, int pageSize, int totalRecords)
    {
        Products = products;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalRecords = totalRecords;
    }
}