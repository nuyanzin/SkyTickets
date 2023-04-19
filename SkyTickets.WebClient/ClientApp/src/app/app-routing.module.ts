import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';


const routes: Routes = [

]
@NgModule({
  	imports: [RouterModule.forRoot(routes, { enableTracing: false, useHash: true })],
	exports: [RouterModule]
})
export class AppRoutingModule { }
