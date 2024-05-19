import { Component } from '@angular/core';
import { Location } from '@angular/common';
import { AccountService } from '../account.service';
import { LoginModel } from './login-model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  providers: [AccountService]
})
export class LoginComponent {
  model: LoginModel = new LoginModel();
  error: string;

  constructor(private accountService: AccountService,
              private location: Location) { }

  submit(model: LoginModel) {
    this.accountService.postLoginModel(model).subscribe(
      data => {
        sessionStorage.setItem('accessToken', data['access_token']);
        sessionStorage.setItem('userName', data['username']);
        this.location.back();
      },
      error => {
        console.log(error);
        this.error = error['error'];
      });
  }
}
