import { AbstractControl, ValidatorFn } from '@angular/forms';

export function ValidadorCustomizadoCRM(formatRegex: RegExp): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    const value = control.value;
    const isValid = formatRegex.test(value);
    return isValid ? null : { invalidFormat: { value } };
  };
}
