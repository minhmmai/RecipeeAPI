using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeeAPI.DTOs.Method
{
    public class GetMethodDTO
    {
        public int Id { get; set; }
        public short Index { get; set; }
        public string Detail { get; set; }
    }
}
