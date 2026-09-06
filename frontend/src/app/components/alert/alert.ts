import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NgClass } from '@angular/common';

@Component({
  imports: [NgClass],
  selector: 'app-alert',
  styleUrl: './alert.css',
  templateUrl: './alert.html',
})

export class Alert {
  @Input() message = '';
  @Input() duration = 5000;
  @Input() type: 'error' | 'warning' | 'success' = 'error';
  @Output() dismissed = new EventEmitter<void>();

  isLeaving = false;

  private leaveTimer?: ReturnType<typeof setTimeout>;
  private removeTimer?: ReturnType<typeof setTimeout>;

  ngOnInit(): void {
    this.startTimers();
  }

  private startTimers() {
    const exitAnimDuration = 300;

    this.leaveTimer = setTimeout(() => {
      this.isLeaving = true;
    }, this.duration - exitAnimDuration)

    this.removeTimer = setTimeout(() => {
      this.dismissed.emit();
    }, this.duration)
  }

  ngOnDestroy() {
    if (this.leaveTimer) {
      clearTimeout(this.leaveTimer);
    }

    if (this.removeTimer) {
      clearTimeout(this.removeTimer);
    }
  }
}
