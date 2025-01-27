import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { DropdownComponent } from './dropdown/dropdown.component';
import { TableComponent } from './table/table.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'enerGov';

  constructor(private router: Router) {}

  goToAddEmployer() {
    this.router.navigate(['/add-employer']);
  }
}
