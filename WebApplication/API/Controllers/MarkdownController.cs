using API.Utils;
using Application.Abstract.Services;
using Application.ResponseResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("md/[controller]")]
public class MarkdownController(
    IMdService mdService, 
    ResponseResultCreator resultCreator): ControllerBase
{
    [HttpPost("convert")]
    [Authorize]
    public async Task<IActionResult> ConvertMarkdown([FromBody] string markdown)
    {
        var result = await mdService.GetHtml(markdown);

        if (!result.IsOk)
        {
            return resultCreator.CreateAction(result);
        }
        
        var html = ((Result<string>)result).Value;
        return Ok(html);
    }
}