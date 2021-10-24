using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SakhCuba.Models
{
    public class NewsEditViewModel
    {
        public string Id { get; set; }
        public string Header { get; set; }
        public string FirstColumn { get; set; }
        public string SecondColumn { get; set; }
        public string ThirdColumn { get; set; }
        public IFormFile NewPicture { get; set; }
        public byte[] Picture { get; set; }
    }
}
