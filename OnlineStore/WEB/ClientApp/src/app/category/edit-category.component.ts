import { Component } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryService } from './category.service';
import { Category } from './category'
import { AccountService } from '../account.service';


@Component({
  templateUrl: './edit-category.component.html',
  providers: [CategoryService, AccountService]
})
export class EditCategoryComponent {
  category: Category;
  categories: Category[];
  error: string;

  constructor(private categoryService: CategoryService,
    private accountService: AccountService,
    private activeRoute: ActivatedRoute,
    private router: Router,
    private location: Location) { }

  ngOnInit() {
    this.accountService.isInAdminRole().subscribe(
      data => console.log(data), error => this.router.navigate(['/404']));
    this.category = new Category();
    this.activeRoute.paramMap.subscribe(params => {
      const id = Number.parseInt(this.activeRoute.snapshot.paramMap.get('id'));
      this.categoryService.getCategoryById(id)
        .subscribe((data: Category) => this.category = data);
    });
    this.categoryService.getAllCategories()
      .subscribe((data: Category[]) => this.categories = data);
  }

  submit(category: Category) {
    if (category.parentCategoryId == 0) {
      category.parentCategoryId = null;
    }
    this.categoryService.editCategory(category).subscribe(
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
