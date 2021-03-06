//-----------------------------------------------------------------------
// <copyright file="Windows7Grbits.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Windows7
{
    using System;

#if !MANAGEDESENT_ON_WSA // Not exposed in MSDK
    /// <summary>
    /// Options for <see cref="Windows7Api.JetConfigureProcessForCrashDump"/>.
    /// </summary>
    [Flags]
    public enum CrashDumpGrbit
    {
        /// <summary>
        /// Default options.
        /// </summary>
        None = 0,

        /// <summary>
        /// Dump minimum includes <see cref="CacheMinimum"/>.
        /// </summary>
        Minimum = 0x1,

        /// <summary>
        /// Dump maximum includes <see cref="CacheMaximum"/>.
        /// </summary>
        Maximum = 0x2,

        /// <summary>
        /// CacheMinimum includes pages that are latched.
        /// CacheMinimum includes pages that are used for memory.
        /// CacheMinimum includes pages that are flagged with errors.
        /// </summary>
        CacheMinimum = 0x4,

        /// <summary>
        /// Cache maximum includes cache minimum.
        /// Cache maximum includes the entire cache image.
        /// </summary>
        CacheMaximum = 0x8,

        /// <summary>
        /// Dump includes pages that are modified.
        /// </summary>
        CacheIncludeDirtyPages = 0x10,

        /// <summary>
        /// Dump includes pages that contain valid data.
        /// </summary>
        CacheIncludeCachedPages = 0x20,

        /// <summary>
        /// Dump includes pages that are corrupted (expensive to compute).
        /// </summary>
        CacheIncludeCorruptedPages = 0x40,
    }
#endif // !MANAGEDESENT_ON_WSA

    /// <summary>
    /// Options for <see cref="Windows7Api.JetPrereadKeys(JET_SESID, JET_TABLEID, byte[][], int[], int, int, out int, PrereadKeysGrbit)"/>.
    /// </summary>
    [Flags]
    public enum PrereadKeysGrbit
    {
        /// <summary>
        /// Preread forward.
        /// </summary>
        Forward = 0x1,

        /// <summary>
        /// Preread backwards.
        /// </summary>
        Backwards = 0x2,
    }

    /// <summary>
    /// Grbits that have been added to the Windows 7 version of ESENT.
    /// </summary>
    public static class Windows7Grbits
    {
        /// <summary>
        /// Compress data in the column, if possible.
        /// </summary>
        public const ColumndefGrbit ColumnCompressed = (ColumndefGrbit)0x80000;

        /// <summary>
        /// Try to compress the data when storing it.
        /// </summary>
        public const SetColumnGrbit Compressed = (SetColumnGrbit)0x20000;

        /// <summary>
        /// Don't compress the data when storing it.
        /// </summary>
        public const SetColumnGrbit Uncompressed = (SetColumnGrbit)0x10000;

        /// <summary>
        /// Recover without error even if uncommitted logs have been lost. Set 
        /// the recovery waypoint with Windows7Param.WaypointLatency to enable
        /// this type of recovery.
        /// </summary>
        public const InitGrbit ReplayIgnoreLostLogs = (InitGrbit)0x80;

        /// <summary>
        /// Terminate without flushing the database cache.
        /// </summary>
        public const TermGrbit Dirty = (TermGrbit)0x8;

        /// <summary>
        /// Permit only intrinsic LV's (so materialisation is not required simply
        /// because a TT has an LV column).
        /// </summary>
        public const TempTableGrbit IntrinsicLVsOnly = (TempTableGrbit)0x80;

        /// <summary>
        /// When enumerating column values only retrieve data that is present in
        /// the record. This means that BLOB columns will not always be retrieved.
        /// </summary>
        public const EnumerateColumnsGrbit EnumerateInRecordOnly = (EnumerateColumnsGrbit)0x00200000;

        /// <summary>
        /// Force a new logfile to be created. This option may be used even if
        /// the session is not currently in a transaction. This option cannot
        /// be used in combination with any other option.
        /// </summary>
        public const CommitTransactionGrbit ForceNewLog = (CommitTransactionGrbit)0x10;

#if !MANAGEDESENT_ON_WSA
        /// <summary>
        /// No instances will be prepared by default. Instances must be added explicitly.
        /// </summary>
        public const SnapshotPrepareGrbit ExplicitPrepare = (SnapshotPrepareGrbit)0x8;
#endif // !MANAGEDESENT_ON_WSA

        /// <summary>
        /// Hint that the sequential traversal will be in the forward direction.
        /// </summary>
        public const SetTableSequentialGrbit Forward = (SetTableSequentialGrbit)0x1;

        /// <summary>
        /// While running Online Defragmentation, do not perform partial merges of pages.
        /// </summary>
        public const DefragGrbit NoPartialMerges = (DefragGrbit)0x80;

        /// <summary>
        /// Defragment a single BTree.
        /// </summary>
        public const DefragGrbit DefragmentBTree = (DefragGrbit)0x100;

        /// <summary>
        /// Hint that the sequential traversal will be in the backward direction.
        /// </summary>
        public const SetTableSequentialGrbit Backward = (SetTableSequentialGrbit)0x2;

        /// <summary>
        /// The database engine will initiate automatic background database maintenance upon database attachment.
        /// </summary>
        public const AttachDatabaseGrbit EnableAttachDbBackgroundMaintenance = (AttachDatabaseGrbit)0x800;

        /// <summary>
        /// The database engine will initiate automatic background database maintenance upon database creation.
        /// </summary>
        public const CreateDatabaseGrbit EnableCreateDbBackgroundMaintenance = (CreateDatabaseGrbit)0x800;
    }
}
