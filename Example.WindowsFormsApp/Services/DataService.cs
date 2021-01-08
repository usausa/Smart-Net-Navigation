namespace Example.WindowsFormsApp.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Example.WindowsFormsApp.Models;

    public class DataService
    {
        private readonly List<DataEntity> entities = new()
        {
            new DataEntity { Id = 1, Name = "Data-1" },
            new DataEntity { Id = 2, Name = "Data-2" },
            new DataEntity { Id = 3, Name = "Data-3" },
            new DataEntity { Id = 4, Name = "Data-4" }
        };

        public IEnumerable<DataEntity> QueryDataList()
        {
            return entities.Select(x => new DataEntity { Id = x.Id, Name = x.Name });
        }

        public void InsertData(string name)
        {
            entities.Add(new DataEntity { Id = entities.Count + 1, Name = name });
        }

        public void UpdateData(DataEntity entity)
        {
            var current = entities.FirstOrDefault(x => x.Id == entity.Id);
            if (current is null)
            {
                return;
            }

            current.Name = entity.Name;
        }
    }
}
