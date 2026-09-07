import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';
import { Alert } from '../../../components/alert/alert';
import { AuthService } from '../../../services/auth';
import { LoginRequest } from '../../../services/models/login-request';

@Component({
  imports: [ReactiveFormsModule, FontAwesomeModule, Alert],
  selector: 'app-login-form',
  styleUrl: './login-form.css',
  templateUrl: './login-form.html',
})

// -- Plan out Dashboard component structure. This is meant to be a hub for all actions and will grow substantially

export class LoginForm {
  private authService = inject(AuthService);
  private formBuilder = inject(FormBuilder);
  private router = inject(Router);
  public alertType: 'error' | 'warning' | 'success' = 'error';
  public alertMessage: string | null = null;
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
      ]
    ]
  })

  public forgotPassword() {
    this.router.navigate(['/user/forgot-password']);
  }

  onSubmit(): void {
    this.isSubmitting = true;
    // Create new LoginRequest object and POST /api/auth/login
    this.loginRequestForm.markAllAsTouched();
    if (this.loginRequestForm.invalid) {
      this.isSubmitting = false;
      return;
    }

    const loginRequest: LoginRequest = {
      email: this.loginRequestForm.controls.email.value!.trim().toLowerCase(),
      password: this.loginRequestForm.controls.password.value!
    }

    this.authService.login(loginRequest).subscribe({
      next: () => {
        this.isSubmitting = false;

        this.router.navigate(['/']);
      },
      error: (e: HttpErrorResponse) => {
        console.log("Error submitting form.");
        if (e.status === 401) {
          this.alertType = "error";
          this.alertMessage = "Email or Password is incorrect.";
          // show error alert
        }
        if (e.status === 403) {
          this.alertType = "error";
          this.alertMessage = "Error: User is forbidden.";
        }
        this.isSubmitting = false;
      }
    });
  }
}
