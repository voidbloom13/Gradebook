import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function passwordMatchValidator(): ValidatorFn {
  return (group: AbstractControl): ValidationErrors | null => {
    const password = group.get('password');
    const confirmPassword = group.get('confirmPassword');

    if (!password || !confirmPassword) {
      return null;
    }

    if (password.value !== confirmPassword.value) {
      confirmPassword.setErrors({
        ...(confirmPassword.errors || {}),
        mismatch: true
      });
    } else if (confirmPassword.hasError('mismatch')) {
      const errors = { ...(confirmPassword.errors || {}) };
      delete errors['mismatch'];

      confirmPassword.setErrors(
        Object.keys(errors).length ? errors : null
      );
    }

    return null;
  };
}
