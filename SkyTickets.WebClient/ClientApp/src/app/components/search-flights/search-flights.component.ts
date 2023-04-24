import { Component } from "@angular/core";
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { filter } from "rxjs";
import { AirportModel } from "src/app/models/airports.model";
import { AirportNode, FlightPath } from "src/app/models/flight.model";
import { SimplePathQuery } from "src/app/models/simple-path-query.model";
import { AirportsService } from "src/app/services/airports.service";
import { GraphService } from "src/app/services/graph.service";

@Component({
    selector: 'app-search-flights',
    templateUrl: 'search-flights.component.html',
    styleUrls: ['./search-flights.component.css']
})
export class SearchFlightsComponent {

    public searchForm?: FormGroup;
    public fromFoundAirports: AirportModel[] = [];
    public toFoundAirports: AirportModel[] = [];
    public fromAirportId?: string;
    public toAirportId?: string;
    public flightPaths: FlightPath[] = [];

    constructor(
        private fb: FormBuilder,
        private readonly airportsService: AirportsService,
        private readonly graphService: GraphService
    ) {
        this.initializeSearchForm();
    }

    public changeFromControl(id: string) {
        this.fromAirportId = id;
        const airportName = this.fromFoundAirports.find(ai => ai.id === id)?.name;
        (this.searchForm?.get('from') as FormControl).setValue(airportName);
        this.closeFromDropdown();
    }

    public closeFromDropdown() {
        this.fromFoundAirports = [];
    }

    public closeToDropdown() {
        this.toFoundAirports = [];
    }

    public changeToControl(id: string) {
        this.toAirportId = id;
        const airportName = this.toFoundAirports.find(ai => ai.id === id)?.name;
        (this.searchForm?.get('to') as FormControl).setValue(airportName);
        this.closeToDropdown();
    }

    public get fromControl(): FormControl {
        return this.searchForm?.get('from') as FormControl;
    }

    public get toControl(): FormControl {
        return this.searchForm?.get('to') as FormControl;
    }

    public submit() {
        //remove ! after tests
        if (!this.searchForm?.valid) {
            let query: SimplePathQuery = {
                flight: {
                    departureAirportEntityId: "29530ad7-d59e-4699-ace5-0a2978b16bcc",
                    arrivalAirportEntityId: "29530ad7-d59e-4699-ace5-0a2978b16bcc",
                    departureDateTime: "2017-11-07", //this.searchForm?.get('departureTime')?.value,
                    arrivalDateTime: "2017-11-08", //this.searchForm?.get('arrivalTime')?.value,
                },
                numberOfTransfers: 1
            };
            this.graphService.getPathsBetweenAirports(query).subscribe(result => {
                this.flightPaths = result;
            });
        }
    }

    private initializeSearchForm() {
        this.searchForm = this.fb.group({
            from: new FormControl(null, { validators: [ Validators.required ]}),
            to: new FormControl('', { validators: [ Validators.required ]}),
            departureTime: new FormControl('', { validators: [ Validators.required ]}),
            arrivalTime: new FormControl('', { validators: [ Validators.required ]})
        });

        this.searchForm?.get('from')?.valueChanges.subscribe((value: string) => {
            this.airportsService.getBySearchTerm(value)
                .pipe(filter(airports => airports && airports.length > 0))
                .subscribe(airports => {
                    this.fromFoundAirports = airports;
                })
        });

        this.searchForm?.get('to')?.valueChanges.subscribe((value: string) => {
            this.airportsService.getBySearchTerm(value)
                .pipe(filter(airports => airports && airports.length > 0))
                .subscribe(airports => {
                    this.toFoundAirports = airports;
                })
        });
    }

    public getFlightTooltip(flightPathIndex: number, airportIndex: number): string {
        const endOfFlightTime = this.flightPaths[flightPathIndex].relationships[airportIndex + 1].date;
        const beginOfFlightTime = this.flightPaths[flightPathIndex].relationships[airportIndex].date;

        const timeBetween = new Date(endOfFlightTime).getTime() - new Date(beginOfFlightTime).getTime();
        const a = new Date(timeBetween);
        return `In flight ${a.getHours()} h, ${a.getMinutes()} m`;
    }

    public getPointInfoTime(dateString: string): string {
        const date = new Date(dateString);
        const minutes = date.getMinutes() === 0 ? `${date.getMinutes()}0` : `${date.getMinutes()}`;
        return `${date.getHours()}:${minutes}`;
    }

    public getPointInfoDay(dateString: string): string {
        const date = new Date(dateString);
        return `${date.getDay()}`;
    }

    public getPointInfoMonth(dateString: string): string {
        const date = new Date(dateString);
        return `${this.getMonthName(date.getMonth())}`;
    }

    private getMonthName(index: number): string {
        switch(index)
        {
            case 1: return 'January';
            case 2: return 'February';
            case 3: return 'March';
            case 4: return 'April';
            case 5: return 'May';
            case 6: return 'June';
            case 7: return 'July';
            case 8: return 'August';
            case 9: return 'September';
            case 10: return 'October';
            case 11: return 'November';
            case 12: return 'December';
            default: return '';
        }
    } 
}