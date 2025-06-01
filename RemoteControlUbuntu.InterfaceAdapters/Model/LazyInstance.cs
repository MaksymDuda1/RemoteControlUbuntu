using Microsoft.Extensions.DependencyInjection;

namespace RemoteControlUbuntu.Infrastructure.Model;

public class LazyInstance<T>(IServiceProvider serviceProvider) : Lazy<T>(serviceProvider.GetRequiredService<T>());