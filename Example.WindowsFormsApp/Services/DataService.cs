namespace Example.WindowsFormsApp.Services
{
    using System.Collections.Generic;

    using Example.WindowsFormsApp.Models;

    public class DataService
    {
        public IEnumerable<DataEntity> QueryDataList()
        {
            yield return new DataEntity { Id = 1, Name = "Data-1" };
            yield return new DataEntity { Id = 2, Name = "Data-2" };
            yield return new DataEntity { Id = 3, Name = "Data-3" };
            yield return new DataEntity { Id = 4, Name = "Data-4" };
            yield return new DataEntity { Id = 5, Name = "Data-5" };
        }
    }
}
