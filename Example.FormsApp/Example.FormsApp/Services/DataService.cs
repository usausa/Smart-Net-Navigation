namespace Example.FormsApp.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Example.FormsApp.Models;

    public class DataService
    {
        private readonly List<DataEntity> entities = new List<DataEntity>
        {
            new DataEntity { Id = 1, Name = "Data-1" },
            new DataEntity { Id = 2, Name = "Data-2" },
            new DataEntity { Id = 3, Name = "Data-3" },
            new DataEntity { Id = 4, Name = "Data-4" }
        };

        public IEnumerable<DataEntity> QueryDataList()
        {
            foreach (var entity in entities)
            {
                yield return new DataEntity { Id = entity.Id, Name = entity.Name };
            }
        }

        public void InsertData(string name)
        {
            entities.Add(new DataEntity { Id = entities.Count + 1, Name = name } );
        }

        public void UpdateData(DataEntity entity)
        {
            var current = entities.FirstOrDefault(x => x.Id == entity.Id);
            if (current != null)
            {
                current.Name = entity.Name;
            }
        }
    }
}
