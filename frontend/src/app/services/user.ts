import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/http';
import { ChangeEmailRequest } from './models/change-email-request';
import { ChangePasswordRequest } from './models/change-password-request';
import { ForgotPasswordRequest } from './models/forgot-password-request';
import { environment } from '../environments/environment';


@Injectable({
    providedIn: 'root',
})

export class UserService {
    private http = inject(HttpClient);

    getEmail() {
        return this.http.get(
            `${environment.apiUrl}/api/user/get-email`,
            { withCredentials: true }
        );
    }

    changeEmail(changeEmailRequest: ChangeEmailRequest) {
        return this.http.post(
            `${environment.apiUrl}/api/user/change-email`,
            changeEmailRequest,
            { withCredentials: true }
        );
    }

    changePassword(changePasswordRequest: ChangePasswordRequest) {
        return this.http.post(
            `${environment.apiUrl}/api/user/change-password`,
            changePasswordRequest,
            { withCredentials: true }
        );
    }

    forgotPassword(forgotPasswordRequest: ForgotPasswordRequest) {
        return this.http.post(
            `${environment.apiUrl}/api/user/forgot-password`,
            forgotPasswordRequest,
            { withCredentials: true }
        );
    }
}