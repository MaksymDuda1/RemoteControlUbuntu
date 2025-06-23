using AutoMapper;
using RemoteControlUbuntu.Application.Exceptions;
using RemoteControlUbuntu.Domain.Abstractions;
using RemoteControlUbuntu.Domain.Dtos;
using RemoteControlUbuntu.Domain.Entities;

namespace RemoteControlUbuntu.Application.Services;

public class CommandSetService(IUnitOfWork unitOfWork, IMapper mapper) : ICommandSetService
{
    public async Task<List<CommandSetDto>> GetCommandSetsAsync(Guid userId)
    {
        var userCommandSets = await unitOfWork.CommandSets.GetByConditionsAsync(x => x.UserId == userId);

        return mapper.Map<List<CommandSetDto>>(userCommandSets);
    }
    
    public async Task<CommandSetDto> GetCommandSetByIdAsync(Guid id)
    {
        var commandSet = await unitOfWork.CommandSets.GetSingleByConditionAsync(
            x => x.Id == id, 
            x => x.Commands);

        if (commandSet == null)
        {
            throw new EntityNotFoundException($"CommandSet with Id {id} not found");
        }

        return mapper.Map<CommandSetDto>(commandSet);
    }

    public async Task CreateCommandSetAsync(CommandSet commandSet)
    {
        var command = mapper.Map<CommandSet>(commandSet);

        await unitOfWork.CommandSets.InsertAsync(command);

        await unitOfWork.SaveAsync();
    }

    public async Task UpdateCommandSetAsync(CommandSet commandSet)
    {
        if (commandSet.Id == Guid.Empty)
        {
            throw new ArgumentException("CommandSet Id cannot be empty");
        }

        var existingCommandSet = await unitOfWork.CommandSets.GetSingleByConditionAsync(
            x => x.Id == commandSet.Id, 
            x => x.Commands);

        if (existingCommandSet == null)
        {
            throw new EntityNotFoundException($"CommandSet with Id {commandSet.Id} not found");
        }
    
        var existingCommandIds = existingCommandSet.Commands?.Select(c => c.Id).ToHashSet() ?? new HashSet<Guid>();
        var newCommandIds = commandSet.Commands?.Select(c => c.Id).ToHashSet() ?? new HashSet<Guid>();

        var commandsToAdd = commandSet.Commands?.Where(c => !existingCommandIds.Contains(c.Id)).ToList() ?? new List<Command>();

        var commandsToRemove = existingCommandSet.Commands?.Where(c => !newCommandIds.Contains(c.Id)).ToList() ?? new List<Command>();

        foreach (var command in commandsToAdd)
        {
            existingCommandSet.Commands.Add(command);
        }

        foreach (var command in commandsToRemove)
        {
            existingCommandSet.Commands.Remove(command);
        }

        existingCommandSet.Name = commandSet.Name;

        unitOfWork.CommandSets.Update(existingCommandSet);
        await unitOfWork.SaveAsync();
    }

    public async Task DeleteCommandSetAsync(Guid commandSetId)
    {
        await unitOfWork.CommandSets.Delete(commandSetId);
        
        await unitOfWork.SaveAsync();
    }
}