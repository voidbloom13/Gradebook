import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EmailResponse } from './models/email-response';
import { UpdateEmailRequest } from './models/update-email-request';
import { ResetPasswordRequest } from './models/reset-password-request';
import { ForgotPasswordRequest } from './models/forgot-password-request';
import { environment } from '../environments/environment';


@Injectable({
    providedIn: 'root',
})

export class UserService {
    private http = inject(HttpClient);

    getEmail() {
        return this.http.get<EmailResponse>(
            `${environment.apiUrl}/api/user/get-email`,
            { withCredentials: true }
        );
    }

    updateEmail(updateEmailRequest: UpdateEmailRequest) {
        return this.http.post(
            `${environment.apiUrl}/api/user/update-email`,
            updateEmailRequest,
            { withCredentials: true }
        );
    }

    resetPassword(resetPasswordRequest: ResetPasswordRequest) {
        return this.http.post(
            `${environment.apiUrl}/api/user/reset-password`,
            resetPasswordRequest,
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