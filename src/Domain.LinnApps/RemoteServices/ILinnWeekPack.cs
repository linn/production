namespace Linn.Production.Domain.LinnApps.RemoteServices
{
    using System;

    public interface ILinnWeekPack
    {
        string Wwsyy(DateTime date);

        DateTime LinnWeekStartDate(int linnWeekNumber);

        int LinnWeekNumber(DateTime date);
    }
}