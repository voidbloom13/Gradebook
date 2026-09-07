import { Component, OnInit, inject } from '@angular/core';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { AuthService } from '../../services/auth';

@Component({
  imports: [],
  selector: 'app-session-check',
  styleUrl: './session-check.css',
  templateUrl: './session-check.html',
})

export class SessionCheck {
  private authService = inject(AuthService);
  private router = inject(Router);

  ngOnInit(): void {
    this.authService.checkSession().subscribe({
      next: (response: any) => {
        console.log(response);
        if (!response.isEmailVerified) {
          this.router.navigate(['/user/verify-email']);
        } else if (response.requirePasswordChange) {
          this.router.navigate(['/user/change-password']);
        } else this.router.navigate(['/dashboard']);
      },
      error: (e: HttpErrorResponse) => {
        if (e.status === 401) {
          this.router.navigate(['/login']);
        }
        console.log(e.message);
        this.router.navigate(['/login']);
      }
    })
  }
}
