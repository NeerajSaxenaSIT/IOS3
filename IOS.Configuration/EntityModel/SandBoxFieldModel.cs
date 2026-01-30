using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOS.Configuration.EntityModel
{
   public class SandBoxFieldModel
    {

        public string SQL_KPI_ID
        {
            get { return _SQL_KPI_ID; }
            set { _SQL_KPI_ID = value; }
        }

        private string _SQL_KPI_ID;
        public string SQL_KPIFormula
        {
            get { return _SQL_KPIFormula; }
            set { _SQL_KPIFormula = value; }
        }

        private string _SQL_KPIFormula;
        public List<string> SQL_SourceTable
        {
            get { return _SQL_SourceTable; }
            set { _SQL_SourceTable = value; }
        }

        private List<string> _SQL_SourceTable;
        public string ObjectAggregation
        {
            get { return _ObjectAggregation; }
            set { _ObjectAggregation = value; }
        }

        private string _ObjectAggregation;
        public string TimeAggregation
        {
            get { return _TimeAggregation; }
            set { _TimeAggregation = value; }
        }

        private string _TimeAggregation;
        public int VSandBoxType
        {
            get { return m_VSandBoxType; }
            set { m_VSandBoxType = value; }
        }

        private int m_VSandBoxType;
        public int SourceObjectID
        {
            get { return m_SourceObjectID; }
            set { m_SourceObjectID = value; }
        }
        private int m_SourceObjectID;
    }
}
