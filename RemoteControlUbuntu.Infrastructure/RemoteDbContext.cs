using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RemoteControlUbuntu.Domain.Entities;

namespace RemoteControlUbuntu.Infrastructure;

public class RemoteDbContext(DbContextOptions<RemoteDbContext> options) : IdentityDbContext<User, Role, Guid>(options)
{
    public DbSet<Connection> Connections { get; set; }

    public DbSet<Command> Commands { get; set; }
    
    public DbSet<CommandSet> CommandSets { get; set; }

    public DbSet<CommandsWhiteList> commandsWhiteLists { get; set; }
    
    public DbSet<CommandsBlackList> commandsBlackLists { get; set; }
    
    public DbSet<UserCommandsWhiteList> UserCommandsWhiteLists { get; set; }

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
                    Type = "Off",
                    TerminalCommand = "shutdown"
                },
                new Command()
                {
                    Id = Guid.NewGuid(),
                    Name = "Telegram",
                    Type = "OpenApp",
                    TerminalCommand = "telegram-desktop"
                },
                new Command()
                {
                    Id = Guid.NewGuid(),
                    Name = "Firefox",
                    Type = "OpenApp",
                    TerminalCommand = "firefox"
                },
                new Command()
                {
                    Id = Guid.NewGuid(),
                    Name = "List",
                    Type = "List",
                    TerminalCommand = "ls -l"
                }
            );

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
        });

        modelBuilder.Entity<CommandSet>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.HasMany(c => c.Commands)
                .WithMany(c => c.CommandSets)
                .UsingEntity(j => j.ToTable("CommandCommandSet"));
        });
    }
}