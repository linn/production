﻿namespace Linn.Production.Domain.LinnApps.RemoteServices
{
    using Resources;

    public interface ISernosRenumPack
    {
        string ReissueSerialNumber(
            string sernosGroup,
            int serialNumber,
            int? newSerialNumber,
            string articleNumber,
            string newArticleNumber,
            string comments,
            int? createdBy);
    }
}