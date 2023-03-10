using System;
using Newtonsoft.Json;

namespace WorkerAtualizacaoImagem.Models.Magento
{
    public class StockItem
    {

        [JsonProperty("product_id")]
        public decimal ProductId { get; set; }

        [JsonProperty("qty")]
        public double Qty { get; set; }

        [JsonProperty("is_in_stock")]
        public bool IsInStock { get; set; }

        [JsonProperty("is_qty_decimal")]
        public bool IsQtyDecimal { get; set; }

        [JsonProperty("show_default_notification_message")]
        public bool ShowDefaultNotificationMessage { get; set; }

        [JsonProperty("use_config_min_qty")]
        public bool UseConfigMinQty { get; set; }

        [JsonProperty("min_qty")]
        public decimal MinQty { get; set; }

        [JsonProperty("use_config_min_sale_qty")]
        public decimal UseConfigMinSaleQty { get; set; }

        [JsonProperty("min_sale_qty")]
        public decimal MinSaleQty { get; set; }

        [JsonProperty("use_config_max_sale_qty")]
        public bool UseConfigMaxSaleQty { get; set; }

        [JsonProperty("max_sale_qty")]
        public decimal MaxSaleQty { get; set; }

        [JsonProperty("use_config_backorders")]
        public bool UseConfigBackorders { get; set; }

        [JsonProperty("backorders")]
        public decimal Backorders { get; set; }

        [JsonProperty("use_config_notify_stock_qty")]
        public bool UseConfigNotifyStockQty { get; set; }

        [JsonProperty("notify_stock_qty")]
        public decimal NotifyStockQty { get; set; }

        [JsonProperty("use_config_qty_increments")]
        public bool UseConfigQtyIncrements { get; set; }

        [JsonProperty("qty_increments")]
        public decimal QtyIncrements { get; set; }

        [JsonProperty("use_config_enable_qty_inc")]
        public bool UseConfigEnableQtyInc { get; set; }

        [JsonProperty("enable_qty_increments")]
        public bool EnableQtyIncrements { get; set; }

        [JsonProperty("use_config_manage_stock")]
        public bool UseConfigManageStock { get; set; }

        [JsonProperty("manage_stock")]
        public bool ManageStock { get; set; }

        [JsonProperty("low_stock_date")]
        public DateTime? LowStockDate { get; set; }

        [JsonProperty("is_decimal_divided")]
        public bool IsDecimalDivided { get; set; }

        [JsonProperty("stock_status_changed_auto")]
        public int StockStatusChangedAuto { get; set; }
    }
}