import { Component, inject, signal, effect } from '@angular/core';
import { NgClass } from '@angular/common';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faX, faInfo } from '@fortawesome/free-solid-svg-icons';
import { AlertService } from '../../services/alert/alert-service';

@Component({
  imports: [NgClass, FontAwesomeModule],
  selector: 'app-alert',
  styleUrl: './alert.css',
  templateUrl: './alert.html',
})

export class Alert {
  public alertService = inject(AlertService);
  public alertState = signal< 'entering' | 'exiting' >('entering');
  public faX = faX;
  public faInfo = faInfo;
  displayTimer: ReturnType<typeof setTimeout> | undefined;
  exitTimer: ReturnType<typeof setTimeout> | undefined;

  dismiss() {
    if (this.alertState() === 'exiting') {
      return;
    }

    if (this.displayTimer) {
      clearTimeout(this.displayTimer);
      this.displayTimer = undefined;
    }

    this.alertState.set('exiting');
    setTimeout(() => {
      this.alertService.dismissCurrent();
    }, 400)
  }

  constructor() {
    effect((onCleanup: any) => {
      const alert = this.alertService.currentAlert();

      if (!alert) {
        return;
      }

      this.alertState.set('entering');

      const displayTimer = setTimeout(() => {
        this.dismiss();
      }, alert.duration)



      onCleanup(() => {
        clearTimeout(displayTimer);
        if (this.exitTimer) {
          clearTimeout(this.exitTimer);
        }
      });
    });
  }
}
