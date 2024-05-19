import { Component } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryService } from './category.service';
import { Category } from './category'
import { AccountService } from '../account.service';

@Component({
  templateUrl: './add-category.component.html',
  providers: [CategoryService, AccountService]
})
export class AddCategoryComponent {
  category: Category;
  categories: Category[];
  error: string;

  constructor(private categoryService: CategoryService,
              private accountService: AccountService,
              private activeRoute: ActivatedRoute,
              private router: Router,
              private location: Location ) { }

  ngOnInit() {
    this.accountService.isInAdminRole().subscribe(
      data => console.log(data), error => this.router.navigate(['/404']));
    this.category = new Category();
    this.activeRoute.paramMap.subscribe(params => {
      const parentCategoryId = Number.parseInt(this.activeRoute.snapshot.paramMap.get('parentCategoryId'));
      if (isNaN(parentCategoryId)) {
        this.category.parentCategoryId = null;
      } else {
        this.category.parentCategoryId = parentCategoryId;
      }
    });
    this.categoryService.getAllCategories()
      .subscribe((data: Category[]) => this.categories = data);
  }

  submit(category: Category) {
    if (category.parentCategoryId == 0) {
      category.parentCategoryId = null;
    }
    this.categoryService.addCategory(category).subscribe(
      data => {
        if (category.parentCategoryId == null) {
          this.router.navigate(['/']);
        } else {
          this.router.navigate(['/category', category.parentCategoryId]);
        }
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
