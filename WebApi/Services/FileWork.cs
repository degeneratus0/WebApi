using System.Collections.Generic;
using System.IO;
using System.Text;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Services
{
    public class FileWork : IFile<DataModel, DataModelDTO>
    {
        private static string path = Directory.GetCurrentDirectory() + "\\storage\\";
        private static int currentId = 1;

        public void Set(List<DataModelDTO> datas)
        {
            Clear();
            Directory.CreateDirectory(path);
            foreach (DataModelDTO data in datas)
            {
                Add(data);
            }
        }

        public void Clear()
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
                currentId = 1;
            }
            else
            {
                return;
            }
        }

        public string Read(string id)
        {
            if (!Directory.Exists(path) || !File.Exists(path + id + ".txt"))
            {
                return null;
            }
            StreamReader sr = new StreamReader(path + id + ".txt");
            string s = sr.ReadToEnd();
            sr.Close();
            return s;
        }

        public IEnumerable<DataModel> ReadAll()
        {
            if (!Directory.Exists(path))
            {
                return null;
            }
            List<DataModel> datas = new List<DataModel>();
            foreach (string s in Directory.GetFiles(path))
            {
                datas.Add(new DataModel()
                {
                    Id = Path.GetFileNameWithoutExtension(s),
                    Content = Read(Path.GetFileNameWithoutExtension(s))
                });
            }
            return datas;
        }

        public void Add(DataModelDTO data)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            StreamWriter sw = new StreamWriter(path + currentId++ + ".txt", false, Encoding.Default);
            sw.Write(data.Content);
            sw.Close();
        }

        public void Edit(string id, DataModelDTO data)
        {
            StreamWriter sw = new StreamWriter(path + id + ".txt", false, Encoding.Default);
            sw.Write(data.Content);
            sw.Close();
        }

        public void Delete(string id)
        {
            FileInfo f = new FileInfo(path + id + ".txt");
            f.Delete();
        }
    }
}
