using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeImageFiles_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            MergeImageFiles.MergeImage.MergeImageFiles(@"C:\Users\Romain.Alexandre\Documents\__To Clean\Image1.png;C:\Users\Romain.Alexandre\Documents\__To Clean\Image2.png", 1, @"C:\Users\Romain.Alexandre\Documents\__To Clean\Test3.png");
        }
    }
}
