using ObjectDatabase;

namespace SysDev2019.DataModels
{
    public class Department : DataModel
    {
        [SerializeProperty("部署ID", IsKey = true, RelationKey = true)]
        public string DepartmentId { get; set; }

        [SerializeProperty("部署名")] public string DepartmentName { get; set; }
    }
}