﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using filejob_service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;

namespace filejob_service.Controllers
{
    [Route("api/filejob-service/[controller]")]
    [ApiController]
    public class CurLinksController : ControllerBase
    {
        /*
         public IEnumerable<Links> Get()
        {
            List<Links> links = Startup.links;
            return links.AsEnumerable();
        }
         */

        [HttpGet]
        public string Get(string token)
        {
            List<SourceLinks> source_Links = Startup.sourceCurLinks;
            foreach (SourceLinks item in source_Links)
            {
                if (item.Token == token)
                {
                    var elements = item.links.AsEnumerable();
                    string json = new JavaScriptSerializer().Serialize(elements);
                    return json;
                }
            }
            return "Not found";
        }

        // POST api/elements
        [HttpPost]
        public async Task<IActionResult> Post(string afe1, string afe2, string afe3, string type, string token)
        {
            var checkToken = false;
            Links inputLink = new Links(afe1, afe2, afe3, type);
            if (token != null && token != "")
            {
                foreach (SourceLinks item in Startup.sourceCurLinks)
                {
                    if (item.Token == token)
                    {
                        item.links.Add(inputLink);
                        checkToken = true;
                        return Ok();
                    }
                }
                if (checkToken == false)
                {
                    SourceLinks s_links = new SourceLinks();
                    s_links.links.Add(inputLink);
                    s_links.Token = token;
                    Startup.sourceCurLinks.Add(s_links);
                    return Ok();
                }
            }
            return BadRequest();


            /*

            //var lastindex = Startup.elements.Count;
            Links inputLink = new Links(afe1, afe2, afe3, type);
            Startup.links.Add(inputLink);
            return Ok();

            */
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string token)
        {
            if (token != null && token != "")
            {
                foreach (SourceLinks item in Startup.sourceCurLinks)
                {
                    if (item.Token == token)
                    {
                        item.links.Clear();
                        return Ok();
                    }
                }
                return Ok();
            }
            return BadRequest();
        }

    }
}