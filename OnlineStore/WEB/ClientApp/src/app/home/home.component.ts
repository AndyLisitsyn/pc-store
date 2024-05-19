import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../category/category.service';
import { Category } from '../category/category';
import { AccountService } from '../account.service';

@Component({
  templateUrl: './home.component.html',
  providers: [CategoryService, AccountService]
})

export class HomeComponent implements OnInit {
  categories: Category[];
  isAdmin: boolean = false;

  constructor(private categoryService: CategoryService,
              private accountService: AccountService) { }

  ngOnInit() {
    this.accountService.getUserName().subscribe(data => console.log(data), error => console.log(error));
    this.accountService.isInAdminRole().subscribe(data => this.isAdmin = true, error => console.log(error));
    this.categoryService.getAllRootCategories().subscribe((data: Category[]) => this.categories = data);
  }

  hideCategory(id: number) {
    console.log(id);
    this.categoryService.hideCategory(id).subscribe(
      data => window.location.reload(),
      error => console.log(error));
  }

  deleteCategory(id: number) {
    console.log(id);
    this.categoryService.deleteCategory(id).subscribe(
      data => window.location.reload(),
      error => console.log(error));
  }
}
