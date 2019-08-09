using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SunmathiTech.HRMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HMSApiControllerBase : ControllerBase
    {
        protected int _offset = 0;
        protected int _limit = 20;
        protected string _filter = string.Empty;
    }
}