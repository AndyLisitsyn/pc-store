import { Component } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryService } from '../category/category.service';
import { ProductService } from './product.service';
import { AccountService } from '../account.service';
import { Category } from '../category/category'
import { Product } from './product'
import { Image } from './image'
import $ from 'jquery';

@Component({
  templateUrl: './add-product.component.html',
  providers: [CategoryService, ProductService, AccountService]
})
export class AddProductComponent {
  product: Product;
  categories: Category[];
  imagesInputs: string[];
  imageInputCount: number = 0;
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
      this.product.categoryId = Number.parseInt(this.activeRoute.snapshot.paramMap.get('categoryId'));
    });
    this.categoryService.getAllCategories()
      .subscribe((data: Category[]) => this.categories = data);
    this.product.images = [];
    for (let i = 0; i < 5; i++) {
      this.product.images.push(new Image());
    }
    this.imagesInputs = ["#imagesInput1", "#imagesInput2", "#imagesInput3", "#imagesInput4"];
  }

  ngAfterViewInit() {
    for (let i = 0; i < this.imagesInputs.length; i++) {
      $(this.imagesInputs[i]).hide();
    }
  }

  addImage() {
    $(this.imagesInputs[this.imageInputCount]).show();
    if (++this.imageInputCount == this.imagesInputs.length) {
      $("#addImageBtn").hide();
    }
  }

  submit(product: Product) {
    this.productService.addProduct(product).subscribe(
      data => {
        this.router.navigate(['/category', product.categoryId]);
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
