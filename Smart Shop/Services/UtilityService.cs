using Smart_Shop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Shop.Services
{
    public class UtilityService : IUtility
    {
        public string GenerateBuildVersion()
        {
            //var rnd = new Random();
            //var strBuilder = new StringBuilder();
            //for (int i = 0; i < 24; i+= 4)
            //{
            //    for (int x = 0; x < 4; x++)
            //    {
            //        var randomNumber = rnd.Next(0, 9);
            //        strBuilder.Append(randomNumber);
            //        if (x == 3)
            //            strBuilder.Append('.');

            //    }
                
            //}



            string version = string.Join(".", Enumerable.Range(0, 6)
                .Select(__ => new Random().Next(1, 1000)
                .ToString("D4")));


            //var index = strBuilder.ToString().LastIndexOf(".");
           // var result = strBuilder.ToString().Substring(0, index);
            return version;
        }
    }
}
