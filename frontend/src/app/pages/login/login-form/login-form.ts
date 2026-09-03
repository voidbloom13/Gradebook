import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';
import { AuthService } from '../../../services/auth';
import { LoginRequest } from '../../../services/models/login-request';

@Component({
  imports: [ReactiveFormsModule, FontAwesomeModule],
  selector: 'app-login-form',
  styleUrl: './login-form.css',
  templateUrl: './login-form.html',
})

// Add state for password input type="password"/"text" to toggle visibility
// Add styling for invalid form fields
// Add logout button to Dashboard and handle /api/auth/logout route
// -- Plan out Dashboard component structure. This is meant to be a hub for all actions and will grow substantially

export class LoginForm {
  private authService = inject(AuthService);
  private formBuilder = inject(FormBuilder);
  private router = inject(Router);
  public showPassword = false;
  public faEye = faEye;
  public faEyeSlash = faEyeSlash;
  public isSubmitting = false;

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

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }

  onSubmit(): void {
    this.isSubmitting  = true;
    // Create new LoginRequest object and POST /api/auth/login
    this.loginRequestForm.markAllAsTouched;
    if (this.loginRequestForm.invalid) {
      return;
    }

    const loginRequest = this.loginRequestForm.getRawValue() as LoginRequest;
    loginRequest.email = loginRequest.email.toLowerCase();

    this.authService.login(loginRequest).subscribe({
      next: () => {
        this.isSubmitting = false;
        this.router.navigate(['/dashboard']);
      },
      error: (e: HttpErrorResponse) => {
        switch (e.status) {
          case 0:
            console.log(e.message);
            break;
          case 400:
            console.log(e.message);
            break;
          case 401:
            console.log(e.message);
            break;
          case 403:
            console.log(e.message);
            break;
          case 429:
            console.log(e.message);
            break;
          case 500:
            console.log(e.message);
            break;
          default:
            console.log(e.message);
            break;
        }
        this.isSubmitting = false;
      }
    });
  }
}
