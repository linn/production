namespace Linn.Production.Domain.LinnApps.RemoteServices
{
    using System;

    public interface ILinnWeekPack
    {
        DateTime GetLinnWeekEndDate(DateTime date);
    }
}