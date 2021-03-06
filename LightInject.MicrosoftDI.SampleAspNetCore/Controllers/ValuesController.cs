﻿namespace LightInject.MicrosoftDI.SampleAspNetCore.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private AppStarter.Class1 class1;

        public ValuesController(AppStarter.Class1 class1) : base()
        {
            this.class1 = class1;
        }

        public AppStarter.Class2 Class2 { get; set; }

        // GET api/values
        [HttpGet]
        public object Get()
        {
            return new
            {
                Constructor = this.class1,
                Property = this.Class2
            };
        }
    }
}