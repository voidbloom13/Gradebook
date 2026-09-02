import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { AuthService } from '../../../services/auth';
import { LoginRequest } from '../../../services/models/login-request';

@Component({
  imports: [ReactiveFormsModule],
  selector: 'app-login-form',
  styleUrl: './login-form.css',
  templateUrl: './login-form.html',
})

// Add state for password input type="password"/"text" to toggle visibility

export class LoginForm {
  private authService = inject(AuthService);
  private formBuilder = inject(FormBuilder);
  private isSubmitting = true;
  loginRequestForm = this.formBuilder.group({
    email: [
      '',
      [
        Validators.required,
        Validators.email
      ]
    ],
    password: [
      '',
      [
        Validators.required,
        Validators.minLength(8),
        Validators.maxLength(128),
      ]
    ]
  })

  onSubmit(): void {
    // Create new LoginRequest object and POST /api/auth/login
    if (this.loginRequestForm.invalid) {
      this.loginRequestForm.markAllAstouched();
      return;
    }

    const loginRequest: LoginRequest = {
      email: this.loginRequestForm.value.email,
      password: this.loginRequestForm.value.password
    }

    this.authService.login(loginRequest).subscribe({
      next: () => {
        this.isSubmitting = false;
      },
      error: (e: HttpErrorResponse) => {
        switch (e.status) {
          case 0:
            break;
          case 400:
            break;
          case 401:
            break;
          case 403:
            break;
          case 429:
            break;
          case 500:
            break;
          default:
            break;
        }
        this.isSubmitting = false;
      }
    });
  }
}
