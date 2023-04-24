namespace SkyTickets.Domain.Entities
{
    public class FlightPath
    {
        public AirportNode? Start { get; set; }
        public AirportNode? End { get; set; }
        public List<AirportNode>? AirportNodes { get; set; }
        public List<FlightNode>? FlightNodes { get; set; }
        public List<FlightsRelationship>? Relationships { get; set; }
    }

    public class FlightsRelationship
    {
        public string? ElementId { get; set; }
        public string? StartNodeElementId { get; set; }
        public string? EndNodeElementId { get; set; }
        public string? Type { get; set; }
        public DateTime Date { get; set; }
        public string? Timestamp { get; set; }
    }

    public abstract class Node
    {
        public string? ElementId { get; set; }
        public abstract NodeType Label { get; }
    }

    public class AirportNode : Node
    {
        public string? Name { get; set; }
        public string? City { get; set; }
        public string? Iata { get; set; }
        public string? Icao { get; set; }
        public override NodeType Label => NodeType.Airport;
    }

    public class FlightNode : Node
    {
        public long Number { get; set; }
        public override NodeType Label => NodeType.Flight;
    }

    public enum NodeType
    {
        Airport = 0,
        Flight = 1
    }
}
