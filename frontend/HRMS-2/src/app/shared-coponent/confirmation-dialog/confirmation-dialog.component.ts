import { Component, Input, Output,EventEmitter } from '@angular/core';

@Component({
  selector: 'app-confirmation-dialog',
  imports: [],
  templateUrl: './confirmation-dialog.component.html',
  styleUrl: './confirmation-dialog.component.css'
})
export class ConfirmationDialogComponent {

  @Input() title: string = "";
  @Input() content: string = "";
  @Output() confirm = new EventEmitter<boolean>();

  confrimDelete(confirm: boolean) {
    this.confirm.emit(confirm);
  }
}
