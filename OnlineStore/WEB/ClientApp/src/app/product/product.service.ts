import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Review } from '../product/review';
import { Product } from './product';

@Injectable()
export class ProductService {
  private url = "/product";

  constructor(private http: HttpClient) { }

  private getHeadersWithAccessToken() {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + sessionStorage.getItem('accessToken'),
    });
    return headers;
  }

  getProducts(categoryId: number, page: number) {
    return this.http.get(this.url + '/index/' + categoryId + '/' + page);
  }

  getDeletedProducts() {
    return this.http.get(this.url + '/getdeletedproducts');
  }

  getProductById(id: number) {
    return this.http.get(this.url + '/getProductById/' + id);
  }

  getDetails(id: number) {
    return this.http.get(this.url + '/details/' + id);
  }

  getMoreReviews(id: number, page: number) {
    return this.http.get(this.url + '/loadMoreReviews/' + id + '/' + page, { responseType: 'text' });
  }

  postReview(review: Review) {
    return this.http.post(this.url + '/postReview', review);
  }

  addProduct(product: Product) {
    const headers = this.getHeadersWithAccessToken();
    return this.http.post(this.url + '/add', product, { headers });
  }

  editProduct(product: Product) {
    const headers = this.getHeadersWithAccessToken();
    return this.http.post(this.url + '/edit', product, { headers });
  }

  hideProduct(id: number) {
    const headers = this.getHeadersWithAccessToken();
    return this.http.post(this.url + '/hide/' + id, null, { headers });
  }

  restoreProduct(id: number) {
    const headers = this.getHeadersWithAccessToken();
    return this.http.post(this.url + '/restore/' + id, null, { headers });
  }

  deleteProduct(id: number) {
    const headers = this.getHeadersWithAccessToken();
    return this.http.post(this.url + '/delete/' + id, null, { headers });
  }
}
