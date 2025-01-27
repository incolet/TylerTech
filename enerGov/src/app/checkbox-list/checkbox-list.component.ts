import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

interface CheckboxItem {
  name: string;
  checked: boolean;
  id: number;
}

@Component({
  selector: 'app-checkbox-list',
  templateUrl: './checkbox-list.component.html',
  styleUrls: ['./checkbox-list.component.css'],
  standalone: true,
  imports: [CommonModule]
})
export class CheckboxListComponent {
  @Input() items: CheckboxItem[] = [];
  
  @Output() selectedItems = new EventEmitter<number[]>();
  
  toggleCheckbox(item: CheckboxItem) {
    item.checked = !item.checked;
    this.emitSelectedItems();
  }

  private emitSelectedItems() {
    const selected = this.items
      .filter(item => item.checked)
      .map(item => item.id);
    this.selectedItems.emit(selected);
  }
}