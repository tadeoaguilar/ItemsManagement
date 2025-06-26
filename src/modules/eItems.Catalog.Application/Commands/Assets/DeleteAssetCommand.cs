using Shared.Contracts.CQRS;

namespace eItems.Catalog.Application.Commands.Assets;

public record DeleteAssetCommand(Guid Id) : ICommand;