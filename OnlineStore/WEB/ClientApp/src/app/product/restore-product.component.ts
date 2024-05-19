import { Component } from '@angular/core';
import { ProductService } from './product.service';
import { Product } from './product'
import { Router } from '@angular/router';
import { AccountService } from '../account.service';


@Component({
  templateUrl: './restore-product.component.html',
  providers: [ProductService, AccountService]
})
export class RestoreProductComponent {
  products: Product[];

  constructor(private productService: ProductService,
              private accountService: AccountService,
              private router: Router) { }

  ngOnInit() {
    this.accountService.isInAdminRole().subscribe(
      data => console.log(data), error => this.router.navigate(['/404']));
    this.productService.getDeletedProducts()
      .subscribe((data: Product[]) => this.products = data);
  }

  restoreProduct(id: number) {
    this.productService.restoreProduct(id).subscribe(
      data => window.location.reload(),
      error => console.log(error));
  }
}
