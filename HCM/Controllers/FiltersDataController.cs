using HCMDataAccess;
using HCMDataAccess.Models;
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
            return _filtersData.GetGates().AsQueryable();

            //IQueryable<FiltersModels.FilterGateModel> data = _filtersData.GetGates().AsQueryable();
            //var count = data.Count();
            //var queryString = Request.Query;
            //if (queryString.Keys.Contains("$inlinecount"))
            //{
            //    StringValues Skip;
            //    StringValues Take;
            //    int skip = (queryString.TryGetValue("$skip", out Skip)) ? Convert.ToInt32(Skip[0]) : 0;
            //    int top = (queryString.TryGetValue("$top", out Take)) ? Convert.ToInt32(Take[0]) : data.Count();
            //    return new { Items = data.Skip(skip).Take(top), Count = count };
            //}
            //else
            //{
            //    return data;
            //}
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
