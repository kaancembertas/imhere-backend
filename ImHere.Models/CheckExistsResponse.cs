using System;
using System.Collections.Generic;
using System.Text;

namespace ImHere.Models
{
    public class CheckExistsResponse
    {
        public bool isExists { get; set; }

        public CheckExistsResponse(bool isExists)
        {
            this.isExists = isExists;
        }
    }
}
