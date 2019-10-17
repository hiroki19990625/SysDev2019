using ObjectDatabase;

namespace SysDev2019.DataModels
{
    public class Product : DataModel
    {
        [SerializeProperty("商品ID", IsKey = true, RelationKey = true)]
        public string ProductId { get; set; }

        [SerializeProperty("商品名")] public string ProductName { get; set; }
        [SerializeProperty("単価")] public int UnitPrice { get; set; }
        [SerializeProperty("メーカーID")] public string ManufacturerId { get; set; }

        [IgnoreProperty, UnionTarget("ManufacturerId")]
        public Manufacturer Manufacturer { get; set; }
    }
}