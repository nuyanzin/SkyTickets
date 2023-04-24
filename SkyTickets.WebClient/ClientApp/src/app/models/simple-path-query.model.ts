export interface SimplePathQuery {
    flight: SimpleFlight;
    numberOfTransfers: number;
}

export interface SimpleFlight {
    departureAirportEntityId?: string;
    arrivalAirportEntityId?: string;
    departureDateTime?: string;
    arrivalDateTime?: string;
}