using HCMDataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FiltersDataController: ControllerBase
    {
        private readonly IFiltersData _filtersData;
        public FiltersDataController(IFiltersData filtersData)
        {
            _filtersData = filtersData;
        }

        [HttpGet]
        [Route("GetGates")]
        public object GetGates()
        {
            //return _filtersData.GetGates().AsQueryable();
            return null;
        }

        //[HttpGet]
        //[Route("GetProfiles")]
        //public object GetProfiles()
        //{
        //    return _filtersData.GetProfiles().AsQueryable();                        
        //}
        //[HttpGet]
        //[Route("GetCategories")]
        //public object GetCategories()
        //{
        //    return _filtersData.GetCategories().AsQueryable();
        //}
        [HttpGet]
        [Route("GetStatuses")]
        public object GetStatuses()
        {
            return _filtersData.GetStatuses().AsQueryable();
        }
    }
}
