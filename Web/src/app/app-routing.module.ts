import { NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';
import { Shell } from '@app/shell/shell.service';
import { AuthenticationGuard } from './auth/authentication.guard';

const routes: Routes = [
  
    { path: 'internal', loadChildren: () => import('./internal/internal.module').then(m => m.InternalModule) },
  
  
    { path: 'public', loadChildren: () => import('./public/public.module').then(m => m.PublicModule) }   
  ,
  // Fallback when no prior route is matched
  { path: '**', redirectTo: '/login', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })],
  exports: [RouterModule],
  providers: []
})
export class AppRoutingModule { }
