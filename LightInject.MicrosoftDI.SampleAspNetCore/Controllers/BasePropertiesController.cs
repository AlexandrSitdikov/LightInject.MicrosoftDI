namespace LightInject.MicrosoftDI.SampleAspNetCore.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    [PropertyInjection]
    public abstract class BasePropertiesController : ControllerBase
    { }
}