
using ComicAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComicAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SeriesController: ControllerBase
{
    private readonly DatabaseContext _dataContext;
    public SeriesController(DatabaseContext dataContext)
    {
        _dataContext = dataContext;
    }
    private static List<Series> _series = new List<Series>()
    {
        new Series()
        {
            Id = Guid.NewGuid(),
            Name = "Action Comics",
            Year = 2016
        }

    };

    [HttpGet]
    public async Task<List<Series>> Get()
    {
        return await _dataContext.Series.ToListAsync();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<List<Series>>> Get(Guid id)
    {
        var series = await _dataContext.Series.FindAsync(id);
        if (series == null)
        {
            return new NotFoundResult();
        }
        return Ok(series);
    }

    [HttpPost]
    public async Task<ActionResult<List<Series>>> Add(Series series)
    {
        series.Id = Guid.NewGuid();
        _dataContext.Series.Add(series);
        await _dataContext.SaveChangesAsync();
        return Ok(await _dataContext.Series.ToListAsync());
    }

    [HttpPut]
    public async Task<ActionResult<List<Series>>> Update(Series series)
    {
        var serie = await _dataContext.Series.FindAsync(series.Id);
        if (serie == null)
        {
            return new NotFoundResult();
        }

        serie.Name = series.Name;
        serie.Year = series.Year;
        await _dataContext.SaveChangesAsync();
        return Ok(await _dataContext.Series.ToListAsync());
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<List<Series>>> Delete(Guid id)
    {
        var series = await _dataContext.Series.FindAsync(id);
        if (series == null)
        {
            return new NotFoundResult();
        }

        _dataContext.Series.Remove(series);
        await _dataContext.SaveChangesAsync();
        return Ok(await _dataContext.Series.ToListAsync());
    }

}