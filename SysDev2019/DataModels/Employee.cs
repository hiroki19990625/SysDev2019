using ObjectDatabase;

namespace SysDev2019.DataModels
{
    public class Employee : DataModel
    {
        [SerializeProperty("社員ID", IsKey = true, RelationKey = true)]
        public string EmployeeId { get; set; }

        [SerializeProperty("名前")] public string Name { get; set; }
        [SerializeProperty("パスワード")] public string Password { get; set; }
        [SerializeProperty("部署ID")] public string DepartmentId { get; set; }

        [IgnoreProperty, UnionTarget("DepartmentId")]
        public Department Department { get; set; }
    }
}