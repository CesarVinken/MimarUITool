using System.Collections.Generic;

public static class TempConfiguration
{
    public static int TravellingGoldCost = -1;
    public static List<IAccumulativePlayerStat> MakeSacrificeTransaction = new List<IAccumulativePlayerStat>() { new Gold(-4), new Reputation(5) };

    public static List<IAccumulativePlayerStat> WorkerWages = new List<IAccumulativePlayerStat>() { new Gold(-2) };
    
    public static List<IAccumulativePlayerStat> HireWorkingFee = new List<IAccumulativePlayerStat>() { new Gold(-4) };
    public static List<IAccumulativePlayerStat> ExtendWorkerContractFee = new List<IAccumulativePlayerStat>() { new Gold(-2) };
    public static List<IAccumulativePlayerStat> BribeWorkerFee = new List<IAccumulativePlayerStat>() { new Gold(-6), new Reputation(-2) };
}
