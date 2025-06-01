using Microsoft.EntityFrameworkCore;

namespace RemoteControlUbuntu.Infrastructure.Model;

public class PaginatedList<T>(List<T> items, int count, int pageIndex, int pageSize)
{
    public List<T> Items { get; } = items;

    public int PageIndex { get; } = pageIndex;

    public int TotalPages { get; } = (int)Math.Ceiling(count / (double)pageSize);

    public int TotalCount { get; } = count;

    public bool HasPreviousPage => this.PageIndex > 1;

    public bool HasNextPage => this.PageIndex < this.TotalPages;

    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PaginatedList<T>(items, count, pageIndex, pageSize);
    }

    public static PaginatedList<T> CreateFromIEnumerable(IEnumerable<T> source, int pageIndex, int pageSize)
    {
        var count = source.Count();
        var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        return new PaginatedList<T>(items, count, pageIndex, pageSize);
    }

    public static PaginatedList<T> Create(List<T> source, int pageIndex, int pageSize)
    {
        var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        return new PaginatedList<T>(items, source.Count, pageIndex, pageSize);
    }
}