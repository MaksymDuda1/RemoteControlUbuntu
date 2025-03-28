namespace RemoteControlUbuntu.Domain.Models;

public class ConnectionFilterModel(
    Guid userId,
    string? name,
    string? host,
    string? username,
    int pageIndex,
    int pageSize)
{
    public Guid UserId { get; } = userId;
    
    public string? Name { get; } = name;
    
    public string? Host { get; } = host;
    
    public string? Username { get; } = username;
    
    public int PageIndex { get; } = pageIndex;
    
    public int PageSize { get; } = pageSize;
}