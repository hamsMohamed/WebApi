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
  public class ProductController : ControllerBase
  {
    private readonly Ecommerce context;
    public ProductController(Ecommerce context)
    {
      this.context = context;
    }
    [HttpGet]
    public IActionResult getAll()
    {
      List<Product> prodlist = context.products.ToList();
      if (prodlist == null)
      {
        return BadRequest("Empty product");
      }
      return Ok(prodlist);
    }
    [HttpGet("Catid")]
    public IActionResult getproductsByCatID([FromQuery] int catID)
    {
      List<Product> ProductList = context.products.Where(P => P.CategoryId == catID).ToList();

      if (ProductList == null)
      {
        return BadRequest("NO Matches");
      }
      return Ok(ProductList);
    }
    [HttpGet("{id:int}", Name = "getRoute")]
    public IActionResult getbyId(int id)
    {
      Product prod = context.products.FirstOrDefault(p => p.Id == id);
      if (prod == null)
      {
        return BadRequest("product is Empty");
      }
      return Ok(prod);
    }
    [HttpGet("{name:alpha}")]
    public IActionResult getByNAme(string name)
    {
      Product deptList =
          context.products.FirstOrDefault(d => d.Name.Contains(name));
      if (deptList == null)
      {
        return BadRequest("Empty Product");
      }
      return Ok(deptList);

    }
    [HttpPost]
    public IActionResult New(Product prod)
    {
      if (ModelState.IsValid)
      {
        try
        {
          context.products.Add(prod);
          context.SaveChanges();
          string url = Url.Link("getRoute", new { id = prod.Id });
          return Created(url, prod);
        }
        catch (Exception ex)
        {
          return BadRequest(ex.Message);
        }
      }
      return BadRequest(ModelState);
    }
    [HttpPut("{id:int}")]
    public IActionResult Edit(int id, Product prod)
    {
      if (ModelState.IsValid)
      {
        try
        {
          Product prodModel =
              context.products.FirstOrDefault(d => d.Id == id);
          prodModel.Name = prod.Name;
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
      Product oldprod = context.products.FirstOrDefault(c => c.Id == id);
      if (oldprod == null)
      {
        return BadRequest("category is Empty");
      }
      context.products.Remove(oldprod);
      context.SaveChanges();
      return StatusCode(204, "Data Deleted");


    }

  }

}

