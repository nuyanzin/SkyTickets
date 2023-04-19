import { NgModule } from "@angular/core";
import { AppComponent } from "./app.component";
import { AppRoutingModule } from './app-routing.module';
import { CommonModule } from "@angular/common";
import { HeaderComponent } from "./components/header/header.component";
import { BrowserModule } from "@angular/platform-browser";

@NgModule({
  	declarations: [
    	AppComponent,
        HeaderComponent,
  	],
  	imports: [
        BrowserModule,
		CommonModule,
    	AppRoutingModule
  	],
  	providers: [],
  	bootstrap: [AppComponent]
})
export class AppModule { }
