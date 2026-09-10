import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';
import { AlertService } from '../../../services/alert/alert-service';
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
  private alert = inject(AlertService);
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
          this.alert.createAlert("Message 1", "error", 3000)
          this.alert.createAlert("Message 2", "warning", 3000)
          this.alert.createAlert("Message 3","success", 3000)
          // show error alert
        }
        if (e.status === 403) {
          this.alert.createAlert("User is forbidden.", "error", 3000)
        }
        this.isSubmitting = false;
      }
    });
  }
}
