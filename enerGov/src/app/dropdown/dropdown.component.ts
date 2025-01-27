import { Component, Input, Output, EventEmitter  } from '@angular/core';
import { CommonModule } from '@angular/common'; // Import CommonModule
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-dropdown',
  standalone: true,
  templateUrl: './dropdown.component.html',
  styleUrls: ['./dropdown.component.scss'],
  imports: [CommonModule], 
})
export class DropdownComponent {
  @Input() items: any[] = []; 
  @Output() selectedItem = new EventEmitter<any>(); 
  selectedOption: string = ''; 

  constructor(private http: HttpClient) {}
 
  onSelectChange(event: Event) {
    const target = event.target as HTMLSelectElement;
    this.selectedOption = target.value;
    console.log('Selected Option:', this.selectedOption);
    this.selectedItem.emit(this.selectedOption); 
  }
}
