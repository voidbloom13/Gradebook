import { Component, ElementRef, ViewChildren, QueryList, inject, signal } from '@angular/core';
import { FormBuilder, FormArray, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgClass } from '@angular/common';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faArrowLeft } from '@fortawesome/free-solid-svg-icons';
import { AuthService } from '../../../services/auth';
import { UserService } from '../../../services/user';
import { AlertService } from '../../../services/alert/alert-service';
import { VerificationCode } from '../../../services/models/verification-code';

@Component({
  imports: [ReactiveFormsModule, NgClass, FontAwesomeModule],
  selector: 'app-verify-email',
  styleUrl: './verify-email.css',
  templateUrl: './verify-email.html',
})

export class VerifyEmail {
  readonly CODE_LENGTH: number = 6;
  readonly MIN_INDEX: number = 0;
  readonly MAX_INDEX: number = this.CODE_LENGTH - 1;
  private authService = inject(AuthService);
  private userService = inject(UserService);
  private alertService = inject(AlertService);
  private formBuilder = inject(FormBuilder);
  private router = inject(Router);
  public emailAddress = signal<string>('');
  public faArrowLeft = faArrowLeft;
  public isSubmitting = false;

  emailVerificationCodeForm = this.formBuilder.group({
    code: this.formBuilder.nonNullable.array(
      Array.from({ length: 6 }, () =>
        this.formBuilder.nonNullable.control('',[
          Validators.required,
          Validators.pattern(/^\d$/)
        ])
      )
    )
  });

  get code() {
    return this.emailVerificationCodeForm.controls.code;
  }

  @ViewChildren('codeInput')
  codeInputs!: QueryList<ElementRef<HTMLInputElement>>;

  sanitizeInput(input: string): string {
    return input.replace(/\D/g, "");
  }

  focusInput(index: number): void {
    if (index < this.MIN_INDEX || index > this.MAX_INDEX) {
      return;
    }

    this.codeInputs.get(index)?.nativeElement.focus();
  }

  onInput(event: Event, index: number): void {
    const input = event.target as HTMLInputElement;
    const sanitizedValue = this.sanitizeInput(input.value).slice(-1);
    input.value = sanitizedValue;
    this.code.at(index).setValue(sanitizedValue);

    if (sanitizedValue && index < this.MAX_INDEX) {
      this.focusInput(index + 1);
    }
  }

  onPaste(event: ClipboardEvent): void {
    event.preventDefault();
    const pastedContents = event.clipboardData?.getData('text') ?? '';
    const sanitizedInput = this.sanitizeInput(pastedContents).slice(0, this.CODE_LENGTH);

    this.code.controls.forEach(c => {
      c.setValue("");
    })

    for (let i = 0; i < sanitizedInput.length; i++) {
      this.code.at(i).setValue(sanitizedInput[i]);
    }

    if (sanitizedInput.length > this.MAX_INDEX) {
      this.focusInput(this.MAX_INDEX);
    } else {
      this.focusInput(sanitizedInput.length);
    }
  }

  onKeydown(event: KeyboardEvent, index: number): void {

    if (event.key === "Backspace") {
      event.preventDefault();
      const control = this.code.at(index);
      if (control.value) {
        control.setValue("");
      } else if (!control.value && index > this.MIN_INDEX) {
        this.focusInput(index - 1);
      } else {
        return;
      }
    }
  }

  ngOnInit(): void {
    this.alertService.createAlert("TODO", "Implement route guard before moving on.", "info", 99999999);
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
    this.router.navigate(['/user/update-email']);
  }

  onSubmit() {
    this.isSubmitting = true;
    this.emailVerificationCodeForm.markAllAsTouched();
    if (this.emailVerificationCodeForm.invalid) {
      this.isSubmitting = false;
      return;
    }

    const submittedCode = this.code.value.join("");

    const emailVerificationCode: VerificationCode = {
      code: submittedCode // handle form concatenation and pass to code
    };
    this.authService.verifyEmail(emailVerificationCode).subscribe({
      next: () => {
        // create alert for email verification success
        // .RequireAuthorization() on endpoint?
        console.log("Email verified successfully.");
      },
      error: () => {
        // create alert for email verification errors
        // which errors should I display alerts for? Which get specific messages vs generic "verification failed"?
        console.log("Unable to verify email.");
      }
    });
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
