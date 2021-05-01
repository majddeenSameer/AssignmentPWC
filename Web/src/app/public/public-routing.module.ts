import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { marker } from '@biesbjerg/ngx-translate-extract-marker';
import { Shell } from '../shell/shell.service';
import { RegisterComponent } from '../user/register/register.component';

const routes: Routes = [
  Shell.childRoutes([
    { path: '', component: RegisterComponent, data: { title: marker('Registeration') } },
    { path: 'register', component: RegisterComponent, data: { title: marker('Registeration') } },
  ], "public")
];


@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PublicRoutingModule { }
