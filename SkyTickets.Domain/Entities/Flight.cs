using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyTickets.Domain.Entities
{
    public class FlightModel
    {
        public AirportNode? Start { get; set; }
        public AirportNode? End { get; set; }
        public List<Segment>? Segments { get; set; }
    }

    public class AirportNode : Node
    {
        public AirportProperties? Properties { get; set; }
    }

    public class FlightNode : Node
    {
        public FlightProperties? Properties { get; set; }
    }

    public class AirportProperties : NodeProperties
    {
        public string? Iata { get; set; }
        public string? City { get; set; }
        public string? Name { get; set; }
        public string? Icao { get; set; }
    }

    public class FlightProperties : NodeProperties
    {
        public int Number { get; set; }
    }

    public class Relationship
    {
        public int Identity { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public string? Type { get; set; }
        public RelationshipProperties? Properties { get; set; }
    }

    public class RelationshipProperties
    {
        public DateTime? Date { get; set; }
        public string? Timestamp { get; set; }
    }

    public class Segment
    {
        public Node Start { get; set; }
        public Node End { get; set; }
        public Relationship Relationship { get; set; }
    }

    public abstract class Node
    {
        public int Identity { get; set; }
        public List<string>? Labels { get; set; }
    }

    public abstract class NodeProperties
    {
    }
}
