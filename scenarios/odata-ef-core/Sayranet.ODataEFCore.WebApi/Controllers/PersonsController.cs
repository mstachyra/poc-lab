using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Sayranet.ODataEFCore.WebApi.Models;

namespace Sayranet.ODataEFCore.WebApi.Controllers
{
    public class PersonsController : ODataController
    {
        private readonly AdventureWorks2022Context _context;

        public PersonsController(AdventureWorks2022Context context)
        {
            _context = context;
        }

        [EnableQuery]
        public ActionResult<IQueryable<Person>> Get()
        {
            return _context.Person;
        }
    }
}
