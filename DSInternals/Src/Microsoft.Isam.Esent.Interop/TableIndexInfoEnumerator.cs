//-----------------------------------------------------------------------
// <copyright file="TableIndexInfoEnumerator.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop
{
    /// <summary>
    /// Enumerate columns in a table specified by dbid and name.
    /// </summary>
    internal sealed class TableIndexInfoEnumerator : IndexInfoEnumerator
    {
        /// <summary>
        /// The database containing the table.
        /// </summary>
        private readonly JET_DBID dbid;

        /// <summary>
        /// The name of the table.
        /// </summary>
        private readonly string tablename;

        /// <summary>
        /// Initializes a new instance of the <see cref="TableIndexInfoEnumerator"/> class.
        /// </summary>
        /// <param name="sesid">
        /// The session to use.
        /// </param>
        /// <param name="dbid">
        /// The database containing the table.
        /// </param>
        /// <param name="tablename">
        /// The name of the table.
        /// </param>
        public TableIndexInfoEnumerator(JET_SESID sesid, JET_DBID dbid, string tablename)
            : base(sesid)
        {
            this.dbid = dbid;
            this.tablename = tablename;
        }

        /// <summary>
        /// Open the table to be enumerated. This should set <see cref="TableEnumerator{T}.TableidToEnumerate"/>.
        /// </summary>
        protected override void OpenTable()
        {
            JET_INDEXLIST indexlist;
            Api.JetGetIndexInfo(this.Sesid, this.dbid, this.tablename, string.Empty, out indexlist, JET_IdxInfo.List);
            this.Indexlist = indexlist;
            this.TableidToEnumerate = this.Indexlist.tableid;
        }

        /// <summary>
        /// Retrieves information about indexes on a table.
        /// </summary>
        /// <param name="sesid">The session to use.</param>
        /// <param name="indexname">The name of the index.</param>
        /// <param name="result">Filled in with information about indexes on the table.</param>
        /// <param name="infoLevel">The type of information to retrieve.</param>
        protected override void GetIndexInfo(
                JET_SESID sesid,
                string indexname,
                out string result,
                JET_IdxInfo infoLevel)
        {
            Api.JetGetIndexInfo(sesid, this.dbid, this.tablename, indexname, out result, infoLevel);
        }
    }
}