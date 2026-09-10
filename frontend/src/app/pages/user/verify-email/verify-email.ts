import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faArrowLeft } from '@fortawesome/free-solid-svg-icons';
import { Alert } from '../../../components/alert.old/alert';
import { AuthService } from '../../../services/auth';
import { UserService } from '../../../services/user';

@Component({
  imports: [ReactiveFormsModule, FontAwesomeModule, Alert],
  selector: 'app-verify-email',
  styleUrl: './verify-email.css',
  templateUrl: './verify-email.html',
})

export class VerifyEmail {
  private authService = inject(AuthService);
  private userService = inject(UserService);
  private formBuilder = inject(FormBuilder);
  private router = inject(Router);
  public emailAddress = signal<string>('');
  public faArrowLeft = faArrowLeft;

  ngOnInit(): void {
    this.userService.getEmail().subscribe({
      next: (response: any) => {
        this.emailAddress.set(response.emailAddress);
      }
    })
  }

  generateCode() {
    this.authService.generateEmailVerificationCode().subscribe({
      next: () => {
        console.log("Code generated successfully.");
      },
      error: () => {
        console.log("Unable to generate new code.");
      }
    });
  }

  changeEmail() {
    console.log("Change Email clicked...")
    // this.userService.changeEmail().subscribe({
    //   next: () => {
    //     console.log("Email updated successfully.");
    //   },
    //   error: () => {
    //     console.log("Unable to change email.");
    //   }
    // });
  }

  // onSubmit() {
  //   this.authService.verifyEmail().subscribe({
  //     next: () => {
  //       console.log("Email verified successfully.");
  //     },
  //     error: () => {
  //       console.log("Unable to verify email.");
  //     }
  //   });
  // }
  
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
