// Copyright (c) Microsoft Corporation. All Rights Reserved.

using Microsoft.AspNetCore.Mvc;

namespace TracingHttp.NetFramework.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index() => Ok("Learn about R9: http://aka.ms/r9");
    }
}