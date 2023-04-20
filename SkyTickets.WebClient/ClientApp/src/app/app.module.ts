import { NgModule } from "@angular/core";
import { AppComponent } from "./app.component";
import { AppRoutingModule } from './app-routing.module';
import { CommonModule } from "@angular/common";
import { HeaderComponent } from "./components/header/header.component";
import { BrowserModule } from "@angular/platform-browser";
import { MainPageComponent } from "./components/main-page/main-page.component";
import { BaseComponent } from "./components/base.component";
import { SearchFlightsComponent } from "./components/search-flights/search-flights.component";
import { AppConfig } from "./app.config";
import { AirportsService } from "./services/airports.service";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpService } from "./services/http.service";
import { HttpClientModule } from "@angular/common/http";
import { NgSelectModule } from "@ng-select/ng-select";

@NgModule({
  	declarations: [
    	AppComponent,
        HeaderComponent,
		BaseComponent,
		MainPageComponent,
		SearchFlightsComponent
  	],
  	imports: [
        BrowserModule,
		CommonModule,
    	AppRoutingModule,
        ReactiveFormsModule,
        HttpClientModule,
        NgSelectModule,
        FormsModule
  	],
  	providers: [
        AppConfig,
        HttpService,
        AirportsService
    ],
  	bootstrap: [AppComponent]
})
export class AppModule { }
