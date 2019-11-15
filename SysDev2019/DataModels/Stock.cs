using ObjectDatabase;

namespace SysDev2019.DataModels
{
    public class Stock : DataModel
    {
        [SerializeProperty("在庫ID", IsKey = true, RelationKey = true)]
        public string StockId { get; set; }

        [SerializeProperty("商品ID")] public string ProductId { get; set; }
        [SerializeProperty("在庫数")] public int StockQuantity { get; set; }
        [SerializeProperty("発注点")] public int ReorderPoint { get; set; }
        [SerializeProperty("発注点量")] public int OrderQuantity { get; set; }

        [IgnoreProperty, UnionTarget("ProductId")]
        public Product Product { get; set; }
    }
}