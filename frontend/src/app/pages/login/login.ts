import { Component, signal } from '@angular/core';
import { NgClass } from '@angular/common';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faBookOpen } from '@fortawesome/free-solid-svg-icons';
import { LoginForm } from "./login-form/login-form";
import { SignupForm } from "./signup-form/signup-form";

@Component({
  imports: [LoginForm, SignupForm, NgClass, FontAwesomeModule],
  selector: 'app-login',
  styleUrl: './login.css',
  templateUrl: './login.html',
})

export class Login {
  public displayForm = signal<"Login" | "Signup">("Signup"); // switch back to "Login" once Signup is styled
  public faBookOpen = faBookOpen;

  showLogin(): void {
    this.displayForm.set("Login");
  }
  showSignup(): void {
    this.displayForm.set("Signup");
  }
}
