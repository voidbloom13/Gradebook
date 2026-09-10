import { Injectable, signal, computed } from '@angular/core';
import { Alert } from '../models/alert';

@Injectable({
    providedIn: 'root'
})
export class AlertService {
    public alerts = signal<Alert[]>([]);

    currentAlert = computed(() => {
        return this.alerts()[0];
    })

    addAlert(alert: Alert) {
        this.alerts.update(current => [
            ...current,
            alert
        ])
    }

    dismissCurrent() {
        this.alerts.update(current => current.slice(1));
    }

    showAlert(
        message: string,
        type: 'error' | 'warning' | 'success',
        duration: number = 3000
    ) {
        this.addAlert({message, type, duration});
    }
}
