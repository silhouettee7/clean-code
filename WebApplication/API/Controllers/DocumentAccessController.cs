using API.Filters;
using API.Utils;
using Application.Abstract.Services;
using Application.ResponseResult;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ServiceFilter(typeof(DocumentExistFilter))]
[ServiceFilter(typeof(DocumentAccessFilter))]
[ApiController]
[Route("document/access[controller]")]
public class DocumentAccessController(
    IDocumentService documentService,
    ResponseResultCreator creator): ControllerBase
{
    [ServiceFilter(typeof(DocumentReadFilter))]
    [HttpGet("/{documentId:guid}")]
    public async Task<IActionResult> GetDocumentContent(Guid documentId)
    {
        var result = await documentService.GetDocumentContentById(documentId);
        if (!result.IsOk)
        {
            return creator.CreateAction(result);
        }
        var content = ((Result<string>)result).Value;
        if (content == null)
        {
            return Problem();
        }
        return Ok(content);
    }
    
    [ServiceFilter(typeof(DocumentEditFilter))]
    [HttpPatch("public/update/{documentId:guid}")]
    public async Task<IActionResult> UpdateDocumentContent([FromBody] string content, Guid documentId)
    {
        var result = await documentService.UpdateDocumentContent(documentId, content);
        if (!result.IsOk)
        {
            return creator.CreateAction(result);
        }

        return Ok();
    }
    
    
}