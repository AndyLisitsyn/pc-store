import { Component } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryService } from '../category/category.service';
import { ProductService } from './product.service';
import { AccountService } from '../account.service';
import { Category } from '../category/category'
import { Product } from './product'

@Component({
  templateUrl: './edit-product.component.html',
  providers: [CategoryService, ProductService, AccountService]
})
export class EditProductComponent {
  product: Product;
  categories: Category[];
  error: string;

  constructor(private categoryService: CategoryService,
    private productService: ProductService,
    private accountService: AccountService,
    private activeRoute: ActivatedRoute,
    private router: Router,
    private location: Location) { }

  ngOnInit() {
    this.accountService.isInAdminRole().subscribe(
      data => console.log(data), error => this.router.navigate(['/404']));
    this.product = new Product();
    this.activeRoute.paramMap.subscribe(params => {
      const id = Number.parseInt(this.activeRoute.snapshot.paramMap.get('id'));
      this.productService.getProductById(id)
        .subscribe((data: Product) => this.product = data);
    });
    this.categoryService.getAllCategories()
      .subscribe((data: Category[]) => this.categories = data);
  }

  submit(product: Product) {
    this.productService.editProduct(product).subscribe(
      data => {
        this.location.back();
      },
      error => {
        this.error = error['error'];
        console.log(error);
      });
  }

  cancel() {
    this.location.back();
  }
}
