import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoginRequest } from './models/login-request';
import { SignupRequest } from './models/signup-request';
import { environment } from '../environments/environment';

@Injectable({
    providedIn: 'root',
})

export class AuthService {
    private http = inject(HttpClient);

    checkSession() {
        return this.http.get(
            `${environment.apiUrl}/api/auth/session`, 
            { withCredentials: true }
        );
    }

    login(loginRequest: LoginRequest) {
        return this.http.post(
            `${environment.apiUrl}/api/auth/login`,
            loginRequest,
            { withCredentials: true }
        );
    }

    signup(signupRequest: SignupRequest) {
        return this.http.post(
            `${environment.apiUrl}/api/auth/signup`,
            signupRequest,
            { withCredentials: true }
        );
    }

    logout() {
        return this.http.post(
            `${environment.apiUrl}/api/auth/logout`,
            { withCredentials: true }
        );
    }
}