namespace Sayranet.ODataEFCore.WebApi.Controllers;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

public class CustomersController : ODataController
{
    private static Random random = new();
    private static List<Customer> customers = new(
        Enumerable.Range(1, 10).Select(idx => new Customer(idx, $"Customer {idx}", new List<Order>(
                Enumerable.Range(1, 2).Select(dx => new Order((idx - 1) * 2 + dx, random.Next(1, 9) * 10))))));

    [EnableQuery]
    public ActionResult<IEnumerable<Customer>> Get()
    {
        return Ok(customers);
    }

    [EnableQuery]
    public ActionResult<Customer> Get([FromRoute] int key)
    {
        var item = customers.SingleOrDefault(d => d.Id.Equals(key));

        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }
}

public record Order(int Id, decimal Amount);

public record Customer(int Id, string Name, List<Order> Orders);
