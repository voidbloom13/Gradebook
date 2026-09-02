import { Component } from '@angular/core';
import { FormGroup, FormControl, ReactiveFormsModule } from '@angular/forms';
import { LoginRequest } from '../../../services/models/login-request';

@Component({
  imports: [ReactiveFormsModule],
  selector: 'app-login-form',
  styleUrl: './login-form.css',
  templateUrl: './login-form.html',
})

// Add state for password input type="password"/"text" to toggle visibility

// Take User Input for Email and Password
// Validate Inputs (email format, password.length > 8)
export class LoginForm {
  loginRequestForm = new FormGroup({
    email: new FormControl(''),
    password: new FormControl('')
  })

  onSubmit() {
    // Create new LoginRequest object and POST /api/auth/login
  }
}
