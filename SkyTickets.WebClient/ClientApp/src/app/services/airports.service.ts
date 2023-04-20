import { Injectable } from "@angular/core";
import { HttpService } from "./http.service";
import { Observable } from "rxjs";
import { AirportModel } from "src/app/models/airports.model";

@Injectable()
export class AirportsService {
    constructor(
        private readonly httpService: HttpService
    ) {}

    public getBySearchTerm(searchTerm: string): Observable<AirportModel[]> {
        const url = `airports/by-search-term?searchTerm=${searchTerm}`;
        return this.httpService.get<AirportModel[]>(url);
    }
}