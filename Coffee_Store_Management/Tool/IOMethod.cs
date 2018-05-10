using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Coffee_Store_Management
{
    class IOMethod
    {
        private static IOMethod instance;      

        private IOMethod()
        {

        }

        public static IOMethod Instance
        {
            get
            {
                if (instance == null)
                    instance = new IOMethod();
                return instance;
            }
            private set { }
        }
        

        public void WriteData<T>(string path, T obj)
        {
            using (StreamWriter writer = new StreamWriter(path, true, Encoding.UTF8))
            {
                writer.WriteLine(JsonConvert.SerializeObject(obj));
            }            
        }

        public void ReadData<T>(string path, ref List<T> list)
        {
            if (File.Exists(path))
            {
                using (StreamReader reader = File.OpenText(path))
                {
                    string input;
                    while ((input = reader.ReadLine()) != null)
                    {
                        list.Add(JsonConvert.DeserializeObject<T>(input));
                    }
                }
            }
        }
      
        public void EditData<T>(string path, List<T> list)
        {
            string tempfile = Path.GetTempFileName();
            using (var sw = new StreamWriter(tempfile))
            {
                foreach (T a in list)
                {
                    sw.WriteLine(JsonConvert.SerializeObject(a));
                }
            }
            File.Delete(path);
            File.Move(tempfile, path);
        }        
    }
}
