using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RemoteControlUbuntu.Domain.Entities;
using RemoteControlUbuntu.Infrastructure.Entities;

namespace RemoteControlUbuntu.Infrastructure;

public class RemoteDbContext(DbContextOptions<RemoteDbContext> options)
    : IdentityDbContext<User, Role, Guid>(options)
{
    public DbSet<Connection> Connections { get; set; }

    public DbSet<Command> Commands { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Role>()
            .HasData(
                new Role()
                {
                    Id = Guid.NewGuid(),
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new Role()
                {
                    Id = Guid.NewGuid(),
                    Name = "User",
                    NormalizedName = "USER"
                });

        modelBuilder.Entity<Command>()
            .HasData(
                new Command()
                {
                    Id = Guid.NewGuid(),
                    Name = "Off",
                    TerminalCommand = "shutdown"
                },
                new Command()
                {
                    Id = Guid.NewGuid(),
                    Name = "Telegram",
                    TerminalCommand = "telegram-desktop"
                },
                new Command()
                {
                    Id = Guid.NewGuid(),
                    Name = "Firefox",
                    TerminalCommand = "firefox"
                },
                new Command()
                {
                    Id = Guid.NewGuid(),
                    Name = "List",
                    TerminalCommand = "ls -l"
                }
            );

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);

            // entity.HasMany(u => u.Connections)
            //     .WithOne(c => c.User)
            //     .HasForeignKey(c => c.User)
            //     .OnDelete(DeleteBehavior.Cascade);
            //
            // entity.HasMany(u => u.UsersCommands)
            //     .WithOne(c => c.User)
            //     .HasForeignKey(c => c.User)
            //     .OnDelete(DeleteBehavior.Cascade);
        });
        
    }
}