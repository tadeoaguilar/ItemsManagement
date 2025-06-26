using Microsoft.AspNetCore.Mvc;
using MediatR;
using FluentValidation;
using eItems.Catalog.Application.Commands.Assets;
using eItems.Catalog.Application.Queries.Assets;
using eItems.Catalog.Data.Model.Assets.Dto;

namespace eItems.ApiService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssetsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AssetsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Create a new asset
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAsset([FromBody] AssetDto assetDto)
    {
        try
        {
            var command = new CreateAssetCommand(
                assetDto.LocationID,
                assetDto.ManufacturerID,
                assetDto.CostCenterID,
                assetDto.AssetCD,
                assetDto.AssetImage,
                assetDto.SubNumber,
                assetDto.Description,
                assetDto.Active
            );

            var assetId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAssetById), new { id = assetId }, assetId);
        }
        catch (ValidationException ex)
        {
            var errors = ex.Errors.Select(e => new { Property = e.PropertyName, Error = e.ErrorMessage });
            return BadRequest(new { Message = "Validation failed", Errors = errors });
        }
    }

    /// <summary>
    /// Get assets with pagination
    /// </summary>
    [HttpGet]
    public async Task<ActionResult> GetAssets([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
    {
        var query = new GetAssetsPaginatedQuery(pageSize, pageIndex);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Get asset by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult> GetAssetById(Guid id)
    {
        var query = new GetAssetByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result.IsNotFound)
        {
            return NotFound();
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Update an existing asset
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsset(Guid id, [FromBody] AssetDto assetDto)
    {
        try
        {
            var command = new UpdateAssetCommand(
                id,
                assetDto.LocationID,
                assetDto.ManufacturerID,
                assetDto.CostCenterID,
                assetDto.AssetCD,
                assetDto.AssetImage,
                assetDto.SubNumber,
                assetDto.Description,
                assetDto.Active
            );

            await _mediator.Send(command);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
        catch (ValidationException ex)
        {
            var errors = ex.Errors.Select(e => new { Property = e.PropertyName, Error = e.ErrorMessage });
            return BadRequest(new { Message = "Validation failed", Errors = errors });
        }
    }

    /// <summary>
    /// Delete an asset
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsset(Guid id)
    {
        try
        {
            var command = new DeleteAssetCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
}