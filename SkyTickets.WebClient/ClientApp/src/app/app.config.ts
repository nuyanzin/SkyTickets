import { Injectable } from "@angular/core";

@Injectable()
export class AppConfig {
    public get skyTicketsApiUrl(): string {
        return `http://localhost:5216`;
    }
}