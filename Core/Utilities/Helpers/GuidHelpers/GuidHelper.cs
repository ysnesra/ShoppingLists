using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helpers.GuidHelpers
{
    public class GuidHelper
    {
        public static string CreateGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
//CreateGuid() metotu ile resimleri wwwroot'un içindeki Image klasörüne yüklerken GUID formatında yüklemek istiyorum.
//*Guid ile eşsiz bir isimi olsun ve yüklediğim resmi bir daha yüklemeyim istiyorum
//*Resimleri Guid formatında yükleyeceğiz--> 0d34-56hk-kdl5.jpg şeklinde

//* Guid.NewGuid() => bu metot her seferinde yeni bir Guid oluşturur 
//* .ToString() => ilede string hale getiririz 

//Guid.NewGuid().ToString(); => Yani bana eşsiz bir değer oluşturdu ve bu değerimi 16 lık sayı tabanına(stringe) çevirdi
