import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { marker } from '@biesbjerg/ngx-translate-extract-marker';
import { Shell } from '../shell/shell.service';
import { RequestSearchComponent } from './request-search/request-search.component';

const routes: Routes = [
  Shell.childRoutes([
    { path: 'request', component: RequestSearchComponent, data: { title: marker('Manage Request')} },

  ], "internal")
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class InternalRoutingModule { }
