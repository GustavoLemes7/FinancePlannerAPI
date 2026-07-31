import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-modal',
  standalone: true,
  templateUrl: './modal.html',
  styleUrl: './modal.css'
})
export class Modal {

  @Input() title = '';

  @Input() subtitle = '';

  @Input() isOpen = false;

  @Output() closed = new EventEmitter<void>();

  close(): void {

    this.closed.emit();

  }

}