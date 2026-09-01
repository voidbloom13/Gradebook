import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoginRequest } from './models/login-request';
import { SignupRequest } from './models/signup-request';

@Injectable({
    providedIn: 'root',
})

export class AuthService {
    private http = inject(HttpClient);

    checkSession() {
        return this.http.get('/api/auth/session');
    }

    login(loginRequest: LoginRequest) {
        return this.http.post('/api/auth/login', loginRequest);
    }

    signup(signupRequest: SignupRequest) {
        return this.http.post('/api/auth/signup', signupRequest);
    }
}