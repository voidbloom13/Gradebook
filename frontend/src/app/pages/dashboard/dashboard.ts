import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { AuthService } from '../../services/auth';
import { UserService } from '../../services/user';

@Component({
  imports: [],
  selector: 'app-dashboard',
  styleUrl: './dashboard.css',
  templateUrl: './dashboard.html',
})

export class Dashboard {
  private authService = inject(AuthService);
  private userService = inject(UserService);
  private router = inject(Router);

  ngOnInit(): void {
    this.userService.initDashboard().subscribe({
      next: (response: any) => {
        if (!response.isEmailVerified) {
          this.router.navigate(['/user/verify-email']);
        }
        if (response.requirePasswordChange) {
          this.router.navigate(['/user/reset-password']);
        }
      },
      error: () => {
        return;
      }
    });
    // route user if verify-email or reset-password is needed
  }

  logout(): void {
    this.authService.logout().subscribe({
      next: () => {
        this.router.navigate(['/']);
      },
      error: (e: HttpErrorResponse) => {
        console.log(`Error: Unable to logout.\nMessage: ${e.message}`);
      }
    })
  }
}
