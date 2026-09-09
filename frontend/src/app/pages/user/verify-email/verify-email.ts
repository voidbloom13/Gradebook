import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faArrowLeft } from '@fortawesome/free-solid-svg-icons';
import { Alert } from '../../../components/alert/alert';
import { AuthService } from '../../../services/auth';

@Component({
  imports: [ReactiveFormsModule, FontAwesomeModule, Alert],
  selector: 'app-verify-email',
  styleUrl: './verify-email.css',
  templateUrl: './verify-email.html',
})

export class VerifyEmail {
  private authService = inject(AuthService);
  private formBuilder = inject(FormBuilder);
  private router = inject(Router);
  public userEmail: string = "placeholder@place.holder";
  public faArrowLeft = faArrowLeft;

  generateCode() {
    alert("Calling /api/auth/generate-email-verification-code");
  }

  logout(): void {
    this.authService.logout().subscribe({
      next: () => {
        this.router.navigate(['/']);
      },
      error: () => {
        console.log("Error logging out.");
      }
    });
  }
}
