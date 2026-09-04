import { Component, signal } from '@angular/core';
import { NgClass } from '@angular/common';
import { LoginForm } from "./login-form/login-form";
import { SignupForm } from "./signup-form/signup-form";

@Component({
  imports: [LoginForm, SignupForm, NgClass],
  selector: 'app-login',
  styleUrl: './login.css',
  templateUrl: './login.html',
})

export class Login {
  public displayForm = signal<string>("Login");
  showLogin(): void {
    this.displayForm.set("Login");
  }
  showSignup(): void {
    this.displayForm.set("Signup");
  }
}
