namespace LightInject.MicrosoftDI.SampleAspNetCore.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class PropertiesController : BasePropertiesController
    {
        private AppStarter.Class1 class1;

        public PropertiesController(AppStarter.Class1 class1) : base()
        {
            this.class1 = class1;
        }

        public AppStarter.Class2 Class2 { get; set; }

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