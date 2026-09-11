import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgClass } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faEye, faEyeSlash, faUser, faEnvelope, faLock, faCircleXmark, faArrowRight } from '@fortawesome/free-solid-svg-icons';
import { AuthService } from '../../../services/auth';
import { passwordMatchValidator } from '../../../services/custom-validators/passwordMatchValidator';
import { SignupRequest } from '../../../services/models/signup-request';

@Component({
  imports: [ReactiveFormsModule, NgClass, FontAwesomeModule],
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
  public faUser = faUser;
  public faEnvelope = faEnvelope;
  public faLock = faLock;
  public faCircleXmark = faCircleXmark;
  public faArrowRight = faArrowRight;
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
      ]
    ]
  },
  {
    validators: passwordMatchValidator('password', 'confirmPassword')
  })

  testSubmit(): void {
    const signupRequest: SignupRequest = {
      firstName: this.signupRequestForm.controls.firstName.value!.trim(),
      lastName: this.signupRequestForm.controls.lastName.value!.trim(),
      email: this.signupRequestForm.controls.email.value!.trim().toLowerCase(),
      password: this.signupRequestForm.controls.password.value!
    }
    console.log(signupRequest);
  }

  onSubmit(): void {
    this.isSubmitting = true;
    this.signupRequestForm.markAllAsTouched();
    if (this.signupRequestForm.invalid) {
      this.isSubmitting = false;
      return;
    }

    const signupRequest: SignupRequest = {
      firstName: this.signupRequestForm.controls.firstName.value!.trim(),
      lastName: this.signupRequestForm.controls.lastName.value!.trim(),
      email: this.signupRequestForm.controls.email.value!.trim().toLowerCase(),
      password: this.signupRequestForm.controls.password.value!
    }

    this.authService.signup(signupRequest).subscribe({
      next: (response: any) => {
        this.isSubmitting = false;
        this.router.navigate(['/session-check', response])
      },
      error: (e: HttpErrorResponse) => {
        console.log("Error submitting form.")
        if (e.status === 409) {
          // this.alertType = "error";
          // this.alertMessage = "This email address already exists. Please login to continue.";
          // show error alert
        }
        this.isSubmitting = false;
      }
    })
  }

}

