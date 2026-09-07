import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  imports: [],
  selector: 'app-change-password',
  styleUrl: './change-password.css',
  templateUrl: './change-password.html',
})

export class ChangePassword {
  private router = inject(Router);

  public forgotPassword() {
    this.router.navigate(['/user/forgot-password'])
  }
}
