using UnitOfWorkExample.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkExample.Models
{
    public class Cat : IAnimal
    {
        public string MakeNoise()
        {
            return "Meow!";
        }
    }
}
