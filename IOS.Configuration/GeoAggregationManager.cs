using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IOS.Configuration
{
    public enum GeoDataType
    {
        HeatMap,
        GeoAggregation
    }
    public class GeoAggregationData
    {
        public GeoAggregationData(string tableName, string KPI, string aggregationFunction)
        {
            this.TableName = tableName;
            this.KPI = KPI;
            this.AggregationFunction= aggregationFunction;
            this.ZoomRange = new Dictionary<string, MapInfo.Mapping.VisibleRange>();
        }
        public Dictionary<string, MapInfo.Mapping.VisibleRange> ZoomRange { set; get; }
        public String TableName { get; set; }
        public String KPI { get; set; }
        public String AggregationFunction { get; set; }
        public GeoDataType GeoDataType { get; set; }
    }
    public class GeoAggregationManager
    {
        public delegate void DelgateGeoAggregation(GeoAggregationData data);
        static GeoAggregationManager()
        {
            DataContainer = new List<GeoAggregationData>();
            ToRemoveItemCollection = new List<GeoAggregationData>();
        }
        public static List<GeoAggregationData> DataContainer { set; get; }
        static List<GeoAggregationData> ToRemoveItemCollection { set; get; }
        public static bool IsExist(GeoAggregationData values)
        {
            return (DataContainer.FirstOrDefault(w => w.GeoDataType == values.GeoDataType && w.TableName == values.TableName && w.KPI == values.KPI) != null);
        }
        public static void Add(GeoAggregationData values)
        {
            if (IsExist(values))
            {
                return;
            }
            DataContainer.Add(values);
        }
        public static void AddToRemoveCollection(GeoAggregationData values)
        {
            if (IsExist(values))
            {
                ToRemoveItemCollection.Add(values);
               // DataContainer.Remove(values);
            }
        }
        public static void RemoveByTableNameAndType(GeoAggregationData values)
        {
            if (IsExist(values))
            {
                DataContainer.RemoveAll(w => w.GeoDataType == values.GeoDataType && w.TableName == values.TableName && w.KPI == values.KPI);
            }
        }
        
        static void ValidateDataCollection()
        {
            foreach (GeoAggregationData item in ToRemoveItemCollection)
            {
                if (IsExist(item))
                {
                    DataContainer.RemoveAll(w => w.GeoDataType == item.GeoDataType && w.TableName == item.TableName && w.KPI == item.KPI);
                }
            }
        }
        public static void GenerateGeoAggregation(DelgateGeoAggregation data, GeoDataType GeoDataType)
        {
            foreach (var item in DataContainer)
            {
                if (item.GeoDataType == GeoDataType)
                {
                    data(item);
                }
            }
            ValidateDataCollection();
        }
    }
}
