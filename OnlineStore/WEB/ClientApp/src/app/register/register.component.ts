import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { RegisterModel } from './register-model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  providers: [AccountService]
})
export class RegisterComponent {
  isPosted: boolean = false;
  myForm: FormGroup;
  error: string;

  constructor(private accountService: AccountService) {
    this.myForm = new FormGroup({
      "email": new FormControl("", [
        Validators.required,
        Validators.pattern("[a-zA-Z_.]+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}")
      ]),
      "password": new FormControl("", [
        Validators.required,
        Validators.pattern("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")
      ]),
      "confirmPassword": new FormControl("", [
      ])
    }, { validators: this.confirmPasswordValidator });
  }
  
  submit() {
    const model = new RegisterModel();
    model.email = this.myForm.controls['email'].value;
    model.password = this.myForm.controls['password'].value;
    model.confirmPassword = this.myForm.controls['confirmPassword'].value;
    this.accountService.postRegisterModel(model).subscribe(
      data => this.isPosted = true,
      error => {
        console.log(error);
        this.error = error['error'];
      });
  }
  
  confirmPasswordValidator(group: FormGroup): { [s: string]: boolean } {
    const password = group.get('password').value;
    const confirmPassword = group.get('confirmPassword').value;

    return password === confirmPassword ? null : { notSame: true }
  }
}
