import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { BaseComponent } from './components/base.component';
import { MainPageComponent } from './components/main-page/main-page.component';


const routes: Routes = [
	{ 
		path: '', 
		component: BaseComponent, 
		children: [
			{ path: 'main', component: MainPageComponent },
			{ path: '**', redirectTo: 'main' }
		]
	},
	{ path: '**', redirectTo: '' }
]
@NgModule({
  	imports: [RouterModule.forRoot(routes, { enableTracing: false, useHash: true })],
	exports: [RouterModule]
})
export class AppRoutingModule { }
