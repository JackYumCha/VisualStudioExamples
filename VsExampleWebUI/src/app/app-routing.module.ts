import { AnyAuthGuard, AdministratorAuthGuard, UserAuthGuard } from './auth/auth-guard';
import { AnimalViewComponent } from './content/animal-view/animal-view.component';
import { AnimalListComponent } from './content/animal-list/animal-list.component';
import { AppComponent } from './app.component';
import { DashboardComponent } from './content/dashboard/dashboard.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { Demo1Component } from './content/demo1/demo1.component';
import { Demo2Component } from './content/demo2/demo2.component';
import { LoginComponent } from './user/login/login.component';
import { InputTypesComponent } from './editor/input-types/input-types.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: AppComponent
  },
  {
    path: 'app',
    component: AppComponent,
    canActivate: [AnyAuthGuard],
    children: [
      {
        path: 'demo1',
        component: Demo1Component,
        canActivate: [AnyAuthGuard]
      },
      {
        path: 'demo2',
        component: Demo2Component,
        canActivate: [AdministratorAuthGuard]
      },
      {
        path: 'input',
        component: InputTypesComponent,
        canActivate: [UserAuthGuard]
      },
      {
        path: 'animals',
        component: AnimalListComponent,
        children:[
          {
            path: 'view/:name', 
            component: AnimalViewComponent
          }
        ]
      }
    ]
  },
  {
    path: 'login',
    component: LoginComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
