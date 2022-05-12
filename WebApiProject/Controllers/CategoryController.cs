using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApiProject.Model;

namespace WebApiProject.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CategoryController : ControllerBase
  {
    private readonly Ecommerce context;
    public CategoryController(Ecommerce context)
    {
      this.context = context;
    }
    [HttpGet]
    public IActionResult GetAll()
    {
      List<Category> catlist = context.category.ToList();
      if(catlist==null)
      {
        return BadRequest("Empty category");
      }
      return Ok(catlist);
    }
    [HttpGet("{id:int}", Name = "getOneRoute")]
    public IActionResult getbyId(int id)
    {
      Category cat = context.category.FirstOrDefault(c => c.Id == id);
      if (cat == null)
      {
        return BadRequest("category is Empty");
      }
      return Ok(cat);
    }
    [HttpGet("{Name:alpha}")]
    public IActionResult getByNAme(string Name)
    {
      Category catList =
          context.category.FirstOrDefault(d => d.Name.Contains(Name));
      if (catList == null)
      {
        return BadRequest("Empty Category");
      }
      return Ok(catList);

    }
    [HttpPost]
    public IActionResult New(Category cat)
    {
      if (ModelState.IsValid)
      {
        try
        {
          context.category.Add(cat);
          context.SaveChanges();
          string url = Url.Link("getOneRoute", new { id = cat.Id });
          return Created(url, cat);
        }
        catch (Exception ex)
        {
          return BadRequest(ex.Message);
        }
      }
      return BadRequest(ModelState);
    }
    [HttpPut("{id:int}")]
    public IActionResult Edit(int id, Category cate)
    {
      if (ModelState.IsValid)
      {
        try
        {
          Category cat =
              context.category.FirstOrDefault(d => d.Id == id);
          cat.Name = cate.Name;
          context.SaveChanges();

          return StatusCode(204, "Data Saved");// Created(url, dep);
        }
        catch (Exception ex)
        {
          return BadRequest(ex.Message);
        }
      }
      return BadRequest(ModelState);

    }
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
      Category oldCat= context.category.FirstOrDefault(c => c.Id == id);
      if (oldCat == null)
      {
        return BadRequest("category is Empty");
      }
      context.category.Remove(oldCat);
      context.SaveChanges();
      return StatusCode(204, "Data Deleted");


    }

  }
}
