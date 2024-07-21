using Microsoft.Extensions.DependencyInjection;

namespace RemoteControlUbuntu.Application.Models;

public class LazyInstance<T>(IServiceProvider serviceProvider) : Lazy<T>(serviceProvider.GetRequiredService<T>());