
using ComicAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ComicAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SeriesController: ControllerBase
{
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
    public List<Series> Get()
    {
        return _series;
    }

    [HttpGet("{id:guid}")]
    public ActionResult<List<Series>> Get(Guid id)
    {
        var series = _series.Find(s => s.Id.Equals(id));
        if (series == null)
        {
            return new NotFoundResult();
        }
        return Ok(series);
    }

    [HttpPost]
    public List<Series> Add(Series series)
    {
        series.Id = Guid.NewGuid();
        _series.Add(series);
        return _series;
    }

    [HttpPut]
    public ActionResult<List<Series>> Update(Series series)
    {
        var serie = _series.Find(s => s.Id.Equals(series.Id));
        if (serie == null)
        {
            return new NotFoundResult();
        }

        serie.Name = series.Name;
        serie.Year = series.Year;
        return Ok(_series);
    }

    [HttpDelete("{id:guid}")]
    public ActionResult<List<Series>> Delete(Guid id)
    {
        var series = _series.Find(s => s.Id.Equals(id));
        if (series == null)
        {
            return new NotFoundResult();
        }

        _series.Remove(series);
        return Ok(_series);
    }

}