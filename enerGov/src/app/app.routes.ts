import { Routes } from '@angular/router';
import { AddEmployeeComponent } from './add-employee/add-employee.component'; 
import { HomeComponent } from './home/home.component';

export const routes: Routes = [
  {
    path: '',
    component: HomeComponent, 
  },
  {
    path: 'add-employer',
    component: AddEmployeeComponent, 
  },
];
