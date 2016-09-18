using System;
using System.Collections.Generic;
using System.Text;

namespace UniGuy.Core.Data
{
    public class OleDbDatabaseTableAdjuster: DatabaseTableAdjusterBase
    {
#region Fields
        /// <summary>
        /// 内部调整器
        /// </summary>
        private IDatabaseTableAdjuster _adjuster;
#endregion

#region Constructors
        public OleDbDatabaseTableAdjuster(IDatabaseTableAdjuster adjuster) 
        {
            if(adjuster == null)
                throw new ArgumentNullException("adjuster");
            this._adjuster = adjuster;
        }
#endregion

        public override bool ExistsTable(string tableName)
        {
            System.Diagnostics.Debug.Assert(_adjuster != null, "_adjuster should not be NULL");
            return _adjuster.ExistsTable(tableName);
        }

        public override void CreateTable(string tableName, IEnumerable<DataColumnDefinition> columnDefinitions)
        {
            System.Diagnostics.Debug.Assert(_adjuster != null, "_adjuster should not be NULL");
            _adjuster.CreateTable(tableName, columnDefinitions);
        }

        public override void ModifyTable(string tableName, IEnumerable<DataColumnDefinition> columnDefinitions)
        {
            System.Diagnostics.Debug.Assert(_adjuster != null, "_adjuster should not be NULL");
            _adjuster.ModifyTable(tableName, columnDefinitions);
        }

        public override void AppendModifyTable(string tableName, IEnumerable<DataColumnDefinition> columnDefinitions)
        {
            System.Diagnostics.Debug.Assert(_adjuster != null, "_adjuster should not be NULL");
            _adjuster.AppendModifyTable(tableName, columnDefinitions);
        }
    }
}
