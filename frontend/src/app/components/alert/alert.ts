import { Component, inject, signal, effect } from '@angular/core';
import { NgClass } from '@angular/common';
import { AlertService } from '../../services/alert/alert-service';

@Component({
  imports: [NgClass],
  selector: 'app-alert',
  styleUrl: './alert.css',
  templateUrl: './alert.html',
})

export class Alert {
  public alertService = inject(AlertService);
  public alertState = signal< 'entering' | 'exiting' >('entering');

  constructor() {
    effect((onCleanup: any) => {
      const alert = this.alertService.currentAlert();

      if (!alert) {
        return;
      }

      this.alertState.set('entering');

      let exitTimer: ReturnType<typeof setTimeout> | undefined;
      const displayTimer = setTimeout(() => {
        this.alertState.set('exiting');
        exitTimer = setTimeout(() => {
          this.alertService.dismissCurrent();
        }, 400);
      }, alert.duration)

      onCleanup(() => {
        clearTimeout(displayTimer);
        if (exitTimer) {
          clearTimeout(exitTimer);
        }
      });
    });
  }
}
