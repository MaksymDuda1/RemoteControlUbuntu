using AutoMapper;
using RemoteControlUbuntu.Application.Abstractions;
using RemoteControlUbuntu.Domain.Abstractions;
using RemoteControlUbuntu.Domain.Dtos;
using RemoteControlUbuntu.Domain.Entities;

namespace RemoteControlUbuntu.Application.Services;

public class CommandsManagementService(IUnitOfWork unitOfWork, IMapper mapper) : ICommandsManagementService
{
    public async Task<List<BlackListCommandDto>> GetBlackListCommands()
    {
        var commands =  await unitOfWork.CommandsBlackList.GetAllAsync();
        
        return mapper.Map<List<BlackListCommandDto>>(commands);
    }

    public async Task AddCommandToBlackList(AddCommandToBlackListDto dto)
    {
        await unitOfWork.CommandsBlackList.InsertAsync(mapper.Map<CommandsBlackList>(dto));
        
        await unitOfWork.SaveAsync();
    }

    public async Task UpdateBlackListCommand(Guid id, BlackListCommandDto dto)
    {
        var command = await unitOfWork.CommandsBlackList.GetSingleByConditionAsync(x => x.Id == id);
        
        mapper.Map(dto, command);
        
        await unitOfWork.SaveAsync();
    }

    public async Task DeleteBlackListCommand(Guid id)
    {
        await unitOfWork.CommandsBlackList.Delete(id);
        
        await unitOfWork.SaveAsync();
    }

    public async Task<List<WhiteListCommandDto>> GetWhiteListCommands()
    {
        var commands = await unitOfWork.CommandsWhiteList.GetAllAsync();
        
        return mapper.Map<List<WhiteListCommandDto>>(commands);
    }

    public async Task AddCommandToWhiteList(AddCommandToBlackListDto dto)
    {
        await unitOfWork.CommandsWhiteList.InsertAsync(mapper.Map<CommandsWhiteList>(dto));
        
        await unitOfWork.SaveAsync();
    }

    public async Task UpdateWhiteListCommands(Guid id, WhiteListCommandDto dto)
    {
        var command = await unitOfWork.CommandsWhiteList.GetSingleByConditionAsync(x => x.Id == id);
        
        mapper.Map(dto, command);
        
        await unitOfWork.SaveAsync();
    }

    public async Task DeleteWhiteListCommand(Guid id)
    {
        await unitOfWork.CommandsWhiteList.Delete(id);
        
        await unitOfWork.SaveAsync();
    }
}