import { Component } from '@angular/core';
import { CategoryService } from './category.service';
import { Category } from './category'
import { Router } from '@angular/router';
import { AccountService } from '../account.service';


@Component({
  templateUrl: './restore-category.component.html',
  providers: [CategoryService, AccountService]
})
export class RestoreCategoryComponent {
  categories: Category[];

  constructor(private categoryService: CategoryService,
              private accountService: AccountService,
              private router: Router) { }

  ngOnInit() {
    this.accountService.isInAdminRole().subscribe(
      data => console.log(data), error => this.router.navigate(['/404']));
    this.categoryService.getDeletedCategories()
      .subscribe((data: Category[]) => this.categories = data);
  }

  restoreCategory(id: number) {
    this.categoryService.restoreCategory(id).subscribe(
      data => window.location.reload(),
      error => console.log(error));
  }
}
