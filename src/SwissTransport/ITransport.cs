namespace SwissTransport
{
    public interface ITransport
    {
        Stations GetStations(string query);
        StationBoardRoot GetStationBoard(string station, string id);
        Connections GetConnections(string fromStation, string toStation, string date, string time, bool isArrivalTime);
    }
}