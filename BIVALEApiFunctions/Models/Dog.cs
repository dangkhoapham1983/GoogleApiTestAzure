using BIVALE.ApiFunctions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIVALE.ApiFunctions.Models
{
    public class Dog : IAnimal
    {
        public string MakeNoise()
        {
            return "Bark!";
        }
    }
}
