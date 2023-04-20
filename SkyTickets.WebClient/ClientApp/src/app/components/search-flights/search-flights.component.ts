import { Component } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { filter } from "rxjs";
import { AirportModel } from "src/app/models/airports.model";
import { AirportsService } from "src/app/services/airports.service";

@Component({
    selector: 'app-search-flights',
    templateUrl: 'search-flights.component.html',
    styleUrls: ['./search-flights.component.css']
})
export class SearchFlightsComponent {

    public searchForm: FormGroup = new FormGroup('');
    public foundAirports: AirportModel[] = [];

    constructor(
        private readonly airportsService: AirportsService
    ) {
        this.initializeSearchForm();
        this.searchForm.get('from')?.valueChanges.subscribe((value: string) => {
            this.airportsService.getBySearchTerm(value).subscribe(airports => {
                if (airports && airports.length > 0)
                {
                    this.foundAirports = airports;
                    console.log(airports);
                }
            })
        });
    }

    private initializeSearchForm() {
        this.searchForm = new FormGroup({
            from: new FormControl('', { validators: [ Validators.required ]}),
            to: new FormControl('', { validators: [ Validators.required ]}),
            departureTime: new FormControl('', { validators: [ Validators.required ]}),
            arrivalTime: new FormControl('', { validators: [ Validators.required ]})
        });
    }
}