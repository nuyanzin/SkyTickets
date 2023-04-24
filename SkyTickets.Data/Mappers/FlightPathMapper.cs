using Neo4j.Driver;
using SkyTickets.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyTickets.Data.Mappers
{
    public static class FlightPathMapper
    {
        public static FlightPath Map(IPath path)
        {
            return path == null ? throw new NullReferenceException() : new FlightPath()
            {
                Start = MapAirportNode(path.As<IPath>().Start),
                End = MapAirportNode(path.As<IPath>().End),
                AirportNodes = path.As<IPath>().Nodes.Where(node => node.Labels[0] == "Airport").Select(MapAirportNode).ToList(),
                FlightNodes = path.As<IPath>().Nodes.Where(node => node.Labels[0] == "Flight").Select(MapFlightNode).ToList(),
                Relationships = path.As<IPath>().Relationships.Select(MapRelationship).ToList()
            };
        }

        public static AirportNode MapAirportNode(INode node)
        {
            return new AirportNode()
            {
                ElementId = node.ElementId,
                Name = (string)node.Properties["name"],
                City = (string)node.Properties["city"],
                Iata = (string)node.Properties["iata"],
                Icao = (string)node.Properties["icao"]
            };
        }

        public static FlightNode MapFlightNode(INode node)
        {
            return new FlightNode()
            { 
                ElementId = node.ElementId,
                Number = long.Parse((string)node.Properties["number"])
            };
        }

        public static FlightsRelationship MapRelationship(IRelationship relationship)
        {
            return new FlightsRelationship()
            {
                ElementId = relationship.ElementId,
                StartNodeElementId = relationship.StartNodeElementId,
                EndNodeElementId = relationship.EndNodeElementId,
                Type = relationship.Type,
                Date = DateTime.Parse((string)relationship.Properties["date"]),
                Timestamp = (string)relationship.Properties["timestamp"]
            };
        }
    }
}
