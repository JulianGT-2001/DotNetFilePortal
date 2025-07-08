using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.controllers;

[ApiController]
[Route("/api/[controller]")]
public class FileController : ControllerBase
{
    #region Attributes
    private readonly IFileService _service;
    #endregion
    #region Constructor
    public FileController(IFileService service)
    {
        _service = service;
    }
    #endregion
    #region Methods
    [HttpPost]
    public async Task<IActionResult> Register([FromForm] IFormFileCollection files, [FromForm] string userId)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        throw new NotImplementedException();
    }
    #endregion
}