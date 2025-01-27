import { Component, Input } from '@angular/core';
import { DropdownComponent } from '../dropdown/dropdown.component';
import { Router } from '@angular/router';
import { CheckboxListComponent } from '../checkbox-list/checkbox-list.component';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms'; 
import { CommonModule } from '@angular/common';
import { ConfigService } from '../core/services/config.service';


@Component({
  selector: 'app-add-employer',
  imports: [DropdownComponent, CheckboxListComponent, FormsModule,CommonModule],
  templateUrl: './add-employee.component.html',
  styleUrl: './add-employee.component.scss',
})
export class AddEmployeeComponent {
  constructor(
    private router: Router,
    private http: HttpClient,
    private config: ConfigService
  ) {}

  selectedRoles: any[] = []; 
  newEmployee = {
    employeeNumber: '',
    firstName: '',
    lastName: ''
  };

  
  selectedEmployee: any; 
  employeesList: any[] = []; 
  roleList: any[] = []; 

  ngOnInit() {
    this.fetchOptions();
    this.fetchEmployees();
    console.log(this.employeesList, 'employee list')
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

  

  fetchOptions() {
    this.http.get<any[]>(this.config.rolesEndpoint).subscribe({
      next: (data: any[]) => {
        this.roleList = data;
      },
      error: (err: any) => {
        console.error('Error fetching dropdown options:', err);
      },
    });
  }

  handleSelectedItem(selectedItem: any) {
    this.selectedEmployee = selectedItem;
    if (this.selectedEmployee !== null) {
      console.log(this.selectedEmployee, 'this.selectedEmployee')
    } else {
      console.warn('No employee selected or missing ID');
    }
  }
  handleSelectedRoleList(selectedItems: any) {
    this.selectedRoles = selectedItems;
    if (this.selectedEmployee !== null) {
      console.log(this.selectedEmployee, 'this.selectedEmployee')
    } else {
      console.warn('No employee selected or missing ID');
    }
  }

  
isSubmitted = false;
apiError: string | null = null;
validationErrors = {
  managerId: false,
  employeeNumber: false,
  firstName: false,
  lastName: false,
  roles: false
};

private readonly empNumberRegex = /^[A-Z]{2}\d{5}$/;

private validateForm(): void {
  this.validationErrors = {
    managerId: !this.selectedEmployee,
    employeeNumber: !this.newEmployee.employeeNumber?.trim(),
    firstName: !this.newEmployee.firstName?.trim(),
    lastName: !this.newEmployee.lastName?.trim(),
    roles: this.selectedRoles.length === 0
  };
}

validateField(field: keyof typeof this.validationErrors): void {
  switch(field) {
    case 'managerId':
      this.validationErrors.managerId = !this.selectedEmployee;
      break;
    case 'employeeNumber':
      const empNum = this.newEmployee.employeeNumber || '';
      this.validationErrors.employeeNumber = 
        !empNum.trim() || 
        !this.empNumberRegex.test(empNum);
      break;
    case 'firstName':
      const firstName = this.newEmployee.firstName || '';
      this.validationErrors.firstName = 
        !firstName.trim() || 
        firstName.trim().length > 50;
      break;
    case 'lastName':
      const lastName = this.newEmployee.lastName || '';
      this.validationErrors.lastName = 
        !lastName.trim() || 
        lastName.trim().length > 50;
      break;
    case 'roles':
      this.validationErrors.roles = this.selectedRoles.length === 0;
      break;
  }
}

onSubmit() {
  this.isSubmitted = true;
  this.apiError = null;
  this.validateForm();

  if (Object.values(this.validationErrors).some(error => error)) {
    return;
  }

  const formData = {
    managerId: this.selectedEmployee,
    lastName: this.newEmployee.lastName.trim(),
    firstName: this.newEmployee.firstName.trim(),
    employeeNumber: this.newEmployee.employeeNumber.trim(),
    roles: this.selectedRoles
  };

  this.http.post(this.config.employeesEndpoint, formData)
  .subscribe({
    next: (response) => {
      this.resetForm();
      this.router.navigate(['/']);
    },
    error: (err) => {
      this.handleApiError(err);
    }
  });
}
onEmployeeNumberInput(event: Event): void {
  const input = event.target as HTMLInputElement;
  let value = input.value.toUpperCase().replace(/[^A-Z0-9]/g, '');
  
  if (value.length > 2) {
    value = value.slice(0, 2) + value.slice(2).replace(/\D/g, '');
  }
  
  if (value.length > 7) value = value.slice(0, 7);
  this.newEmployee.employeeNumber = value;
  this.validateField('employeeNumber');
}

private handleApiError(err: any): void {
  console.error('Error creating employee:', err);
  this.apiError = err.error?.message || 'An unexpected error occurred';

  if (err.status === 400 && err.error?.errors) {
    const apiErrors = err.error.errors;
    
    if (apiErrors.employeeNumber) {
      this.validationErrors.employeeNumber = true;
      this.apiError += `: ${apiErrors.employeeNumber}`;
    }
    
  }
}

private resetForm() {
  this.apiError = null;
  this.newEmployee = {
    employeeNumber: '',
    firstName: '',
    lastName: ''
  };
  this.selectedRoles = [];
  this.selectedEmployee = null;
  this.isSubmitted = false;
  this.validationErrors = {
    managerId: false,
    employeeNumber: false,
    firstName: false,
    lastName: false,
    roles: false
  };
}



  goToHome() {
    this.router.navigate(['/']); 
  }
}
