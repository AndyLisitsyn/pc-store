import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { AccountService } from '../account.service';
import { Category } from './category';
import { CategoryService } from './category.service';

@Component({
  templateUrl: './category.component.html',
  providers: [CategoryService, AccountService]
})

export class CategoryComponent implements OnInit {
  id: number;
  category: Category;
  childCategories: Category[];
  isAdmin: boolean = false;

  constructor(private categoryService: CategoryService,
              private accountService: AccountService,
              private activeRoute: ActivatedRoute,
              private router: Router) { }

  ngOnInit() {
    this.activeRoute.paramMap.subscribe(params => {
      this.id = Number.parseInt(this.activeRoute.snapshot.paramMap.get("id"));
      this.categoryService.getCategoriesByParentId(this.id).subscribe((data: JSON) => {
        if (data['isAnyChildren'] == true) {
          this.category = data['category'];
          if (this.category.isDeleted) {
            this.router.navigate(['/404']);
          }
          this.childCategories = data['childCategories'];
        } else if (data['isAnyChildren'] == false) {
          this.router.navigate(['products', this.id, 1]);
        }
      });
    });
    this.accountService.isInAdminRole().subscribe(data => this.isAdmin = true, error => console.log(error));
  }

  hideCategory(id: number) {
    this.categoryService.hideCategory(id).subscribe(
      data => window.location.reload(),
      error => console.log(error));
  }

  deleteCategory(id: number) {
    this.categoryService.deleteCategory(id).subscribe(
      data => window.location.reload(),
      error => console.log(error));
  }
}
