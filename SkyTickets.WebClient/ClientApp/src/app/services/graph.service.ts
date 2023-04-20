import { Injectable } from "@angular/core";
import { HttpService } from "./http.service";

@Injectable()
export class GraphService {
    constructor(
        private readonly httpService: HttpService
    ) {}

    //public getPathsBetweenAirports()

}