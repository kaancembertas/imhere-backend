// Author: Kaan Çembertaş 
// No: 200001684

using System;
using System.Collections.Generic;
using System.Text;

namespace ImHere.Models
{
    public class ApiResponse
    {
        public ApiResponse(string errorMessage)
        {
            this.errorMessage = errorMessage;
        }
        public string errorMessage { get; }
    }
}
