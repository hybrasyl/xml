//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Hybrasyl.Xml.Manager;
//using Hybrasyl.Xml.Objects;

using Hybrasyl.Xml.Manager;
using Hybrasyl.Xml.Objects;
using Microsoft.VisualBasic.CompilerServices;

namespace Hybrasyl.Xml.Interfaces;

public interface ILoadOnStart<T> where T : HybrasylEntity<T>
{
    public static abstract XmlLoadResult<T> LoadAll(string path);
}

public interface IPostProcessable<T> where T : HybrasylEntity<T>
{
    public static abstract XmlLoadResult<T> Process(IWorldDataManager manager, XmlLoadResult<T> result);
}

public interface IAdditionalValidationRequired<T> where T : HybrasylEntity<T>
{
    public static abstract XmlLoadResult<T> Validate(IWorldDataManager manager, XmlLoadResult<T> result);
}

//namespace Hybrasyl.Xml.Interfaces
//{
//    public interface ILoadOnStart<out T> where T : HybrasylEntity<T>
//    {
//        public T Load(string path);

//        public static XmlLoadResult<T> LoadAll(string directory)
//        {
//            var ret = new XmlLoadResult<T>();
//            foreach (var xml in HybrasylEntity<T>.GetXmlFiles(directory))
//            {
//                try
//                {
//                    var entity = HybrasylEntity<T>.LoadFromFile(xml);
//                    ret.Results.Add(entity);
//                }
//                catch (Exception ex)
//                {
//                    ret.Errors.Add(xml,ex.ToString());
//                }
//            }
//            return ret;
//        }

//        public static XmlLoadResult<T> PostProcess(XmlLoadResult<T> result)
//        {
//        }
//        public static XmlLoadResult<T> AdditionalValidation(XmlLoadResult<T> result) => result;
//    }
//}

//public interface IExample<T>
//{
//    public static virtual void Foo()
//    {
//        Console.WriteLine("This is a virtual static function.");
//    }
//}

//public class Example : IExample<string>
//{
//}

//public class Program
//{
//    static void Main(string[] args)
//    {
//        Example.Foo(); // Output: This is a virtual static function.
//    }
//}
