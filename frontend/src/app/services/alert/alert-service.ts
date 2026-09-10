import { Injectable, signal, computed } from '@angular/core';
import { Alert } from '../models/alert';

@Injectable({
    providedIn: 'root'
})
export class AlertService {
    public alerts = signal<Alert[]>([]);

    createAlert(message: string, type: 'error' | 'warning' | 'success', duration: number = 3000) {
        let alert = {
            "id": crypto.randomUUID(),
            "message": message,
            "type": type,
            "duration": duration
        } as Alert;
        this.alerts.update(current => [
            ...current,
            alert
        ])
    }

    currentAlert = computed(() => {
        return this.alerts()[0];
    })

    currentAlertArray = computed(() => {
        const alert = this.currentAlert();
        return alert ? [alert] : [];
    })

    dismissCurrent() {
        this.alerts.update(current => current.slice(1));
    }
}
