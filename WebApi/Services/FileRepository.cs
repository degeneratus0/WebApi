using System.Collections.Generic;
using System.IO;
using System.Text;
using WebApi.Models;
using WebApi.Models.DTOs;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    /// <summary>
    /// Implementation of FileRepository
    /// </summary>
    internal class FileRepository : IFileRepository<DataModel, DataModelDTO>
    {
        private static readonly string path = Directory.GetCurrentDirectory() + "\\storage\\";
        private static int currentId = 0;

        /// <inheritdoc cref="IFileRepository{DataModel, DataModelDTO}.Set(List{DataModelDTO})"/>
        public void Set(List<DataModelDTO> datas)
        {
            Clear();
            Directory.CreateDirectory(path);
            foreach (DataModelDTO data in datas)
            {
                Add(data);
            }
        }

        /// <inheritdoc cref="IFileRepository{DataModel, DataModelDTO}.Clear()"/>
        public void Clear()
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
                currentId = 0;
            }
            else
            {
                return;
            }
        }

        /// <inheritdoc cref="IFileRepository{DataModel, DataModelDTO}.Read(string)"/>
        public DataModelDTO Read(string id)
        {
            if (!Directory.Exists(path) || !File.Exists(path + id + ".txt"))
            {
                return null;
            }
            StreamReader sr = new StreamReader(path + id + ".txt");
            string s = sr.ReadToEnd();
            sr.Close();
            return new DataModelDTO { Content = s };
        }

        /// <inheritdoc cref="IFileRepository{DataModel, DataModelDTO}.ReadAll()"/>
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
                    Content = Read(Path.GetFileNameWithoutExtension(s)).Content
                });
            }
            return datas;
        }

        /// <inheritdoc cref="IFileRepository{DataModel, DataModelDTO}.Add(DataModelDTO)"/>
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

        /// <inheritdoc cref="IFileRepository{DataModel, DataModelDTO}.Edit(string, DataModelDTO)"/>
        public void Edit(string id, DataModelDTO data)
        {
            StreamWriter sw = new StreamWriter(path + id + ".txt", false, Encoding.Default);
            sw.Write(data.Content);
            sw.Close();
        }

        /// <inheritdoc cref="IFileRepository{DataModel, DataModelDTO}.Delete(string)"/>
        public void Delete(string id)
        {
            FileInfo f = new FileInfo(path + id + ".txt");
            f.Delete();
        }
    }
}
