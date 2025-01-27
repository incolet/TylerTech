import { Component, Input } from '@angular/core';
import { DropdownComponent } from '../dropdown/dropdown.component';
import { TableComponent } from '../table/table.component';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { ConfigService } from '../core/services/config.service'

@Component({
  selector: 'app-home',
  standalone: true, 
  imports: [DropdownComponent, TableComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent {

  constructor(
    private router: Router,
    private http: HttpClient,
    private config : ConfigService
  ) {}

  employeesList: any[] = []; 

  employeeListUnderManager: any[] = [];

  

  ngOnInit() {
    this.fetchOptions();
  }

  fetchOptions() {
    this.http.get<any[]>(this.config.employeesEndpoint).subscribe({
      next: (data: any[]) => {
        this.employeesList = data;
      },
      error: (err: any) => {
        console.error('Error fetching dropdown options:', err);
      },
    });
  }

  fetchEmployees() {
    this.http.get<any[]>(this.config.employeesEndpoint).subscribe({
      next: (data: any[]) => {
        this.employeesList = data;
      },
      error: (err: any) => {
        console.error('Error fetching dropdown options:', err);
      },
    });
  }

  selectedEmployee: any; 

  fetchEmployeesUnderManager(managerId: string) {
    const url = `${this.config.employeesEndpoint}?managerId=${managerId}`;
  
    this.http.get<any[]>(url).subscribe({
      next: (data: any[]) => {
        this.employeeListUnderManager = data;
        console.log('Employees under manager:', data);
      },
      error: (err: any) => {
        console.error('Error fetching employees:', err);
      },
    });
  }

  handleSelectedItem(selectedItem: any) {
    this.selectedEmployee = selectedItem;
    if (this.selectedEmployee !== null) {
      this.fetchEmployeesUnderManager(this.selectedEmployee); 
    } else {
      console.warn('No employee selected or missing ID');
    }
  }

  goToAddEmployer() {
    this.router.navigate(['/add-employer']);
  }
}