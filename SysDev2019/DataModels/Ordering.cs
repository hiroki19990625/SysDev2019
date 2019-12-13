using ObjectDatabase;

namespace SysDev2019.DataModels
{
    public class Ordering : DataModel
    {
        [SerializeProperty("発注ID", IsKey = true, RelationKey = true)]
        public string OrderingId { get; set; }

        [SerializeProperty("商品ID")] public string ProductId { get; set; }
        [SerializeProperty("社員ID")] public string EmployeeId { get; set; }
        [SerializeProperty("発注量")] public int OrderingVolume { get; set; }
        [SerializeProperty("発注日")] public string OrderingDate { get; set; }
        [SerializeProperty("発注完了")] public bool OrderingCompleted { get; set; }
        [SerializeProperty("受け取り完了")] public bool ReceiptComplete { get; set; }

        [IgnoreProperty]
        [UnionTarget("ProductId")]
        public Product Product { get; set; }

        [IgnoreProperty]
        [UnionTarget("EmployeeId")]
        public Employee Employee { get; set; }
    }
}