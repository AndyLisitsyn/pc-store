import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'email-confirmation',
  templateUrl: './email-confirmation.component.html'
})
export class EmailConfirmationComponent {
  isSuccess: boolean;

  constructor(activeRoute: ActivatedRoute) {
    this.isSuccess = activeRoute.snapshot.params["isSuccess"] == "true";
  }
}
