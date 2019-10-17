using ObjectDatabase;

namespace SysDev2019.DataModels
{
    public class Manufacturer : DataModel
    {
        [SerializeProperty("メーカーID", IsKey = true, RelationKey = true)]
        public string ManufacturerId { get; set; }

        [SerializeProperty("メーカー名")] public string ManufacturerName { get; set; }
    }
}