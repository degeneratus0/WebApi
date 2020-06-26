using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Interfaces
{
    public interface IData
    {
        static string path = Directory.GetCurrentDirectory() + "\\storage\\";
        public class Data
        {
            public string id { get; set; }
            public string content { get; set; }
        }
        void Set();
        string Read(string id);
        List<Data> ReadAll();
        void Add(string content);
        void Edit(string id, string content);
        void Delete(string id);
    }
    public class FileWork : IData
    {
        public void Set()
        {
            Directory.CreateDirectory(IData.path);
            if (Directory.GetFiles(IData.path).Length == 0)
            {
                Add("text 1");
                Add("text 2");
                Add("text 3");
            }
        }
        public string Read(string id)
        {
            try
            {
                StreamReader sr = new StreamReader(IData.path + id + ".txt");
                string s = sr.ReadToEnd();
                sr.Close();
                return s;
            }
            catch
            {
                return null;
            }
        }
        public List<IData.Data> ReadAll()
        {
            List<IData.Data> datas = new List<IData.Data>();
            foreach (string s in Directory.GetFiles(IData.path))
            {
                datas.Add(new IData.Data()
                {
                    id = Path.GetFileNameWithoutExtension(s),
                    content = Read(Path.GetFileNameWithoutExtension(s))
                });
            }
            return datas;
        }
        public void Add(string content)
        {
            StreamWriter sw = new StreamWriter(IData.path + (Directory.GetFiles(IData.path).Length + 1) + ".txt", false, System.Text.Encoding.Default);
            sw.Write(content);
            sw.Close();
        }
        public void Edit(string id, string content)
        {
            StreamWriter sw = new StreamWriter(IData.path + id + ".txt", false, System.Text.Encoding.Default);
            sw.Write(content);
            sw.Close();
        }
        public void Delete(string id)
        {
            FileInfo f = new FileInfo(IData.path + id + ".txt");
            f.Delete();
        }
    }
}
