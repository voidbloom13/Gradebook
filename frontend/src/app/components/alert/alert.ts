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
  alertService = inject(AlertService);
  alertState = signal< 'entering' | 'exiting' >('entering');

  constructor() {
    effect((onCleanup: any) => {
      const alert = this.alertService.currentAlert();

      if (!alert) {
        return;
      }

      console.log('entering');

      const timer = setTimeout(() => {
        this.alertState.set('exiting');
        console.log('exiting');
      }, alert.duration - 300)


      onCleanup(() => {
        clearTimeout(timer);
      });
    });
  }
}
