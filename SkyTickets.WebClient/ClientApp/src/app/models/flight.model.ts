export interface FlightPath
{
    start?: AirportNode;
    end?: AirportNode;
    airportNodes: AirportNode[];
    flightNodes: FlightNode[];
    relationships: FligtsRelationship[];
}

export interface FligtsRelationship
{
    elementId: string;
    startNodeElementId: string;
    endNodeElementId: string;
    type: string;
    date: string;
    timestamp: string;
}

export interface Node
{
    elementId: string;
    label: NodeType;
}

export interface AirportNode
{
    name: string;
    city: string;
    iata: string;
    icao: string;
}

export interface FlightNode
{
    number: number;
}

export enum NodeType {
    Airport = 0,
    Flight = 1
}

// export class FlightPath
// {
//     public start?: AirportNode;
//     public end?: AirportNode;
//     public nodes?: Node[];
//     public relationships?: FligtsRelationship;
// }

// export interface FligtsRelationship
// {
//     elementId: string;
//     startNodeElementId: string;
//     endNodeElementId: string;
//     type: string;
//     date: Date;
//     timestamp: string;
// }

// export interface Node
// {
//     elementId: string;
//     label: NodeType;
// }

// export interface AirportNode extends Node
// {
//     name: string;
//     city: string;
//     iata: string;
//     icao: string;
// }

// export interface FlightNode extends Node
// {
//     number: number;
// }

// export enum NodeType {
//     Airport = 0,
//     Flight = 1
// }