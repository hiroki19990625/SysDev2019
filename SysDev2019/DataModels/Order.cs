﻿using System;
using ObjectDatabase;

namespace SysDev2019.DataModels
{
    public class Order : DataModel
    {
        [SerializeProperty("受注ID", IsKey = true, RelationKey = true)]
        public string OrderId { get; set; }

        [SerializeProperty("社員ID")] public string EmployeeId { get; set; }
        [SerializeProperty("商品ID")] public string ProductId { get; set; }
        [SerializeProperty("受注量")] public int OrderVolume { get; set; }
        [SerializeProperty("受注日")] public string OrderDate { get; set; }
        [SerializeProperty("受注完了")] public bool OrderCompletion { get; set; }
        [SerializeProperty("注文キャンセル")] public bool CancelOrder { get; set; }
        [SerializeProperty("出荷完了")] public bool ShipmentCompleted { get; set; }

        [IgnoreProperty, UnionTarget("EmployeeId")]
        public Employee Employee { get; set; }

        [IgnoreProperty, UnionTarget("ProductId")]
        public Product Product { get; set; }
    }
}