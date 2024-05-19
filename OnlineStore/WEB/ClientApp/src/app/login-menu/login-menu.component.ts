import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { error } from 'selenium-webdriver';
import { AccountService } from 'src/app/account.service';

@Component({
  selector: 'app-login-menu',
  templateUrl: './login-menu.component.html',
  providers: [AccountService]
})
export class LoginMenuComponent implements OnInit {
  userName: string;
  isUserSignedIn: boolean = false;
  constructor(private accountService: AccountService, private router: Router) { }

  ngOnInit() {
    this.userName = sessionStorage.getItem('userName');
    if (this.userName) {
      this.isUserSignedIn = true;
    }
  }

  logout() {
    this.accountService.logout().subscribe(
      data => {
        sessionStorage.removeItem('accessToken');
        sessionStorage.removeItem('userName');
        sessionStorage.removeItem('userRole');
        window.location.reload()
      },
      error => console.log(error) );
  }
}
