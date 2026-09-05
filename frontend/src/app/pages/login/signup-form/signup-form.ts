import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';
import { AuthService } from '../../../services/auth';
import { passwordMatchValidator } from '../../../services/custom-validators/passwordMatchValidator';
import { SignupRequest } from '../../../services/models/signup-request';

@Component({
  imports: [ReactiveFormsModule, FontAwesomeModule],
  selector: 'app-signup-form',
  styleUrl: './signup-form.css',
  templateUrl: './signup-form.html',
})
export class SignupForm {
  private authService = inject(AuthService);
  private formBuilder = inject(FormBuilder);
  private router = inject(Router);
  public nameMinLength = 2;
  public nameMaxLength = 50;
  public showPassword = false;
  public showConfirmPassword = false;
  public passwordMinLength = 8;
  public passwordMaxLength = 128;
  public faEye = faEye;
  public faEyeSlash = faEyeSlash;
  public isSubmitting = false

  signupRequestForm = this.formBuilder.group({
    firstName: [
      '',
      [
        Validators.required,
        Validators.minLength(this.nameMinLength),
        Validators.maxLength(this.nameMaxLength),
        Validators.pattern(/^[A-Za-z]+(?=:[ '-]+)*$/)
      ]
    ],
    lastName: [
      '',
      [
        Validators.required,
        Validators.minLength(this.nameMinLength),
        Validators.maxLength(this.nameMaxLength),
        Validators.pattern(/^[A-Za-z]+(?=:[ '-]+)*$/)
      ]
    ],
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
        Validators.minLength(this.passwordMinLength),
        Validators.maxLength(this.passwordMaxLength)
      ]
    ],
    confirmPassword: [
      '',
      [
        Validators.required,
        Validators.minLength(this.passwordMinLength),
        Validators.maxLength(this.passwordMaxLength),
      ]
    ]
  },
  {
    validators: passwordMatchValidator()
  })

  onSubmit(): void {
    this.isSubmitting = true;
    this.signupRequestForm.markAllAsTouched();
    if (this.signupRequestForm.invalid) {
      this.isSubmitting = false;
      return;
    }

    const signupRequest = this.signupRequestForm.getRawValue() as SignupRequest;
    signupRequest.email = signupRequest.email.toLowerCase();

    this.authService.signup(signupRequest).subscribe({
      next: () => {
        this.isSubmitting = false;
        this.router.navigate(['/dashboard'])
      },
      error: (e: HttpErrorResponse) => {
        console.log("Error submitting form.")
        // Handle duplicate email entries, any other errors
        if (e.status === 409) {
          console.log("Email already exists.");
        }
        this.isSubmitting = false;
      }
    })
  }

}

