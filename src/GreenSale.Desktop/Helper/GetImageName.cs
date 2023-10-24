using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSale.Desktop.Helper
{
    public class GetImageName
    {
       public static string  GetName(string imageName)
        {
            FileInfo fileInfo = new FileInfo(imageName);
            return "IMG_" + Guid.NewGuid().ToString() + fileInfo.Extension;
        }
    }
}
