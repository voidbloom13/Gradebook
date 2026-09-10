import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faArrowLeft, faArrowRight } from '@fortawesome/free-solid-svg-icons';
import { Alert } from '../../../components/alert.old/alert';
import { UserService } from '../../../services/user';
import { passwordMatchValidator } from '../../../services/custom-validators/passwordMatchValidator';
import { ResetPasswordRequest } from '../../../services/models/reset-password-request';

@Component({
  imports: [ReactiveFormsModule, FontAwesomeModule, Alert],
  selector: 'app-reset-password',
  styleUrl: './reset-password.css',
  templateUrl: './reset-password.html',
})

export class ResetPassword {
  private userService = inject(UserService);
  private formBuilder = inject(FormBuilder);
  private router = inject(Router);
  public alertType: 'error' | 'warning' | 'success' = 'error';
  public alertMessage: string | null = null
  public showOldPassword = false;
  public showNewPassword = false;
  public showConfirmPassword = false;
  public passwordMinLength = 8;
  public passwordMaxLength = 128;
  public faArrowLeft = faArrowLeft;
  public faArrowRight = faArrowRight;
  public isSubmitting = false;

  resetPasswordForm = this.formBuilder.group({
    oldPassword: [
      '',
      [
        Validators.required,
        Validators.minLength(this.passwordMinLength),
        Validators.maxLength(this.passwordMaxLength)
      ]
    ],
    newPassword: [
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
        Validators.maxLength(this.passwordMaxLength)
      ]
    ]
  },
{
  validators: passwordMatchValidator('newPassword', 'confirmPassword')
})

  public backToDashboard() {
    this.router.navigate(['/dashboard']);
  }

  public forgotPassword() {
    this.router.navigate(['/user/forgot-password']);
  }

  onSubmit(): void {
    this.isSubmitting = true;
    this.resetPasswordForm.markAllAsTouched();
    if (this.resetPasswordForm.invalid) {
      this.isSubmitting = false;
      return;
    }

    const resetPasswordRequest: ResetPasswordRequest = {
      oldPassword: this.resetPasswordForm.controls.oldPassword.value!,
      newPassword: this.resetPasswordForm.controls.newPassword.value!
    }

    this.userService.resetPassword(resetPasswordRequest).subscribe({
      next: (response: any) => {
        this.isSubmitting = false;
        this.alertType = 'success';
        this.alertMessage = 'Password updated successfully!';
        this.router.navigate(['/dashboard']);
      }
    })
  }
}
