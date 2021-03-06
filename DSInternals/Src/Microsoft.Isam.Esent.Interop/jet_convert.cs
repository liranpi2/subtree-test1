//-----------------------------------------------------------------------
// <copyright file="jet_convert.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

#if !MANAGEDESENT_ON_WSA // Not exposed in MSDK
namespace Microsoft.Isam.Esent.Interop
{
    using System;

    /// <summary>
    /// Conversion options for <see cref="Api.JetCompact"/>. This feature
    /// was discontinued in Windows Server 2003.
    /// </summary>
    [Obsolete("Not available in Windows Server 2003 and up.")]
    public abstract class JET_CONVERT
    {
    }
}
#endif // !MANAGEDESENT_ON_WSA