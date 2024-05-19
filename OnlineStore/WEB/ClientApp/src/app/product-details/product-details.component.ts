import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Image } from '../product/image';
import { Product } from '../product/product';
import { ProductService } from '../product/product.service';
import { Review } from '../product/review';
import $ from 'jquery';

@Component({
  templateUrl: './product-details.component.html',
  providers: [ProductService],
  styleUrls: ['./product-details.component.css']
})

export class ProductDetailsComponent implements OnInit {
  id: number;
  product: Product;
  images: Image[];
  reviews: Review[];
  review: Review = new Review();
  isReviewPostedByUser: boolean;
  isReviewSuccessfullyPosted: boolean = false;
  isUserSignedIn: boolean = false;
  averageRating: number;
  reviewsCount: number;
  reviewsLoadCount = 1;
  reviewsLoadingPageSize = 5;

  constructor(private productService: ProductService,
              private activeRoute: ActivatedRoute,
              private router: Router) {
    this.id = Number.parseInt(this.activeRoute.snapshot.params['id']);
  }

  ngOnInit() {
    const name = sessionStorage.getItem('userName');
    if (name != null) {
      this.isUserSignedIn = true;
    }
    if (this.id) {
      this.productService.getDetails(this.id).subscribe((data: JSON) => {
        this.product = data['product'];
        if (this.product.isDeleted) {
          this.router.navigate(['/404']);
        }
        this.images = this.product.images;
        this.reviews = data['reviews'];
        this.review = data['review'];
        this.review.rating = '5';
        this.averageRating = data['averageRating'];
        this.reviewsCount = data['reviewsCount'];
        this.isReviewPostedByUser = data['isReviewPostedByUser'];});
    }
  }

  loadMoreReviews() {
    this.productService.getMoreReviews(this.id, this.reviewsLoadCount + 1).subscribe((data) => {
      $('<div></div>').append(data).appendTo('#results');
      this.reviewsLoadCount++;
      if (this.reviewsLoadCount * this.reviewsLoadingPageSize >= this.reviewsCount) {
        $('#load').hide();
      }
    });
  }

  postReview(review: Review) {
    this.productService.postReview(review).subscribe(
      data => this.isReviewSuccessfullyPosted = true,
      error => console.log('Post review error', error));
  }

  createRange(number) {
    var items: number[] = [];
    for (var i = 1; i <= number; i++) {
      items.push(i);
    }
    return items;
  }

  formatDate(date) {
    var d = new Date(date),
      month = '' + (d.getMonth() + 1),
      day = '' + d.getDate(),
      year = d.getFullYear();

    if (month.length < 2)
      month = '0' + month;
    if (day.length < 2)
      day = '0' + day;

    return [day, month, year].join('.');
  }
}
