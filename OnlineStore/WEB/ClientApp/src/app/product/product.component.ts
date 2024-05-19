import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../account.service';
import { Category } from '../category/category';
import { Product } from './product';
import { ProductService } from './product.service';

@Component({
  templateUrl: './product.component.html',
  providers: [ProductService, AccountService]
})

export class ProductComponent implements OnInit {
  categoryId: number;
  category: Category;
  products: Product[];
  pageNumber: number;
  pagesCount: number;
  pagesArray: Array<number>;
  isAdmin: boolean = false;

  constructor(private productService: ProductService,
              private accountService: AccountService,
              private activeRoute: ActivatedRoute,
              private router: Router) { }

  ngOnInit() {
    this.activeRoute.paramMap.subscribe(params => {
      this.categoryId = Number.parseInt(this.activeRoute.snapshot.paramMap.get("categoryId"));
      this.pageNumber = Number.parseInt(this.activeRoute.snapshot.paramMap.get("page"));
      this.productService.getProducts(this.categoryId, this.pageNumber).subscribe((data: JSON) => {
        this.category = data['category'];
        if (this.category.isDeleted) {
          this.router.navigate(['/404']);
        }
        this.products = data['products'];
        this.pagesCount = data['pagesCount'];
        this.pagesArray = new Array(this.pagesCount);
      });
    });
    this.accountService.isInAdminRole().subscribe(data => this.isAdmin = true, error => console.log(error));
  }

  hideProduct(id: number) {
    this.productService.hideProduct(id).subscribe(
      data => window.location.reload(),
      error => console.log(error));
  }

  deleteProduct(id: number) {
    this.productService.deleteProduct(id).subscribe(
      data => window.location.reload(),
      error => console.log(error));
  }
}
