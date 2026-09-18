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

    initDashboard() {
        // get user and return routing fields (isEmailVerified/requirePasswordReset)
        return this.http.post(
            `${environment.apiUrl}/api/user/init-dashboard`,
            {},
            { withCredentials: true }
        )
    }

    initVerifyEmail() {
        return this.http.post<EmailResponse>(
            `${environment.apiUrl}/api/user/init-verify-email`,
            {},
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

    testEmail() {
        return this.http.post(
            `${environment.apiUrl}/api/auth/test-email`,
            {},
            { withCredentials: true}
        );
    }
}