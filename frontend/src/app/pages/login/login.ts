import { Component, signal } from '@angular/core';
import { LoginForm } from "./login-form/login-form";
import { SignupForm } from "./signup-form/signup-form";

@Component({
  imports: [LoginForm, SignupForm],
  selector: 'app-login',
  styleUrl: './login.css',
  templateUrl: './login.html',
})

export class Login {
  public displayForm = signal("Login");
  showLogin(): void {
    this.displayForm.set("Login");
  }
  showSignup(): void {
    this.displayForm.set("Signup");
  }
}
