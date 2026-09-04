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

// -- Plan out Dashboard component structure. This is meant to be a hub for all actions and will grow substantially

export class LoginForm {
  private authService = inject(AuthService);
  private formBuilder = inject(FormBuilder);
  private router = inject(Router);
  // move password validators (other than required) to signup
  // public passwordMinLength = 8;
  // public passwordMaxLength = 128;
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
        // Validators.minLength(this.passwordMinLength),
        // Validators.maxLength(this.passwordMaxLength),
      ]
    ]
  })

  onSubmit(): void {
    this.isSubmitting = true;
    // Create new LoginRequest object and POST /api/auth/login
    this.loginRequestForm.markAllAsTouched();
    if (this.loginRequestForm.invalid) {
      this.isSubmitting = false;
      return;
    }

    const loginRequest = this.loginRequestForm.getRawValue() as LoginRequest;
    loginRequest.email = loginRequest.email.toLowerCase();

    this.authService.login(loginRequest).subscribe({
      next: () => {
        this.isSubmitting = false;
        this.router.navigate(['/dashboard']);
      },
      error: () => {
        console.log("Error submitting form.");
        this.isSubmitting = false;
      }
    });
  }
}
