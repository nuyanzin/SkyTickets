import { Injectable } from "@angular/core";
import { HttpService } from "./http.service";
import { SimplePathQuery } from "../models/simple-path-query.model";
import { FlightPath } from "../models/flight.model";
import { Observable } from "rxjs";

@Injectable()
export class GraphService {
    constructor(
        private readonly httpService: HttpService
    ) {}

    public getPathsBetweenAirports(query: SimplePathQuery): Observable<FlightPath[]> {
        let url = `graph/paths-between-airports`;
        return this.httpService.post<FlightPath[]>(url, query);
    }
}