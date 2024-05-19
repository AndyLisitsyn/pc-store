import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { RegisterModel } from './register/register-model';
import { LoginModel } from './login/login-model';

@Injectable()
export class AccountService {
  private url = "/account";

  constructor(private http: HttpClient) { }

  private getHeadersWithAccessToken() {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + sessionStorage.getItem('accessToken'),
    });
    return headers;
  }

  getUserName() {
    const headers = this.getHeadersWithAccessToken();
    return this.http.get(this.url + '/getusername', { headers, responseType: 'text' });
  }

  isInAdminRole() {
    const headers = this.getHeadersWithAccessToken();
    return this.http.get(this.url + '/isinadminrole', { headers });
  }

  postLoginModel(model: LoginModel) {
    return this.http.post(this.url + '/login', model);
  }

  postRegisterModel(model: RegisterModel) {
    return this.http.post(this.url + '/register', model);
  }

  confirmEmail(userId: string, code: string) {
    return this.http.get(this.url + '/confirmEmail' + '/' + userId + '/' + code);
  }

  logout() {
    return this.http.post(this.url + '/logout', null);
  }
}
