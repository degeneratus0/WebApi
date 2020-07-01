using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Services
{
    public class FCFileWork : IFile<Data, DataDTO>
    {
        static string path = Directory.GetCurrentDirectory() + "\\storage\\";
        public void Set()
        {
            Directory.CreateDirectory(path);
            if (Directory.GetFiles(path).Length == 0)
            {
                Add(new DataDTO { content = "text 1" });
                Add(new DataDTO { content = "text 2" });
                Add(new DataDTO { content = "text 3" });
            }
        }
        public string Read(string id)
        {
            try
            {
                StreamReader sr = new StreamReader(path + id + ".txt");
                string s = sr.ReadToEnd();
                sr.Close();
                return s;
            }
            catch
            {
                return null;
            }
        }
        public IEnumerable<Data> ReadAll()
        {
            List<Data> datas = new List<Data>();
            foreach (string s in Directory.GetFiles(path))
            {
                datas.Add(new Data()
                {
                    id = Path.GetFileNameWithoutExtension(s),
                    content = Read(Path.GetFileNameWithoutExtension(s))
                });
            }
            return datas;
        }
        public void Add(DataDTO data)
        {
            StreamWriter sw = new StreamWriter(path + (Directory.GetFiles(path).Length + 1) + ".txt", false, System.Text.Encoding.Default);
            sw.Write(data.content);
            sw.Close();
        }
        public void Edit(string id, DataDTO data)
        {
            StreamWriter sw = new StreamWriter(path + id + ".txt", false, System.Text.Encoding.Default);
            sw.Write(data.content);
            sw.Close();
        }
        public void Delete(string id)
        {
            FileInfo f = new FileInfo(path + id + ".txt");
            f.Delete();
        }
    }
}
