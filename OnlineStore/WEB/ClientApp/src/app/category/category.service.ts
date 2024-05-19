import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Category } from './category';

@Injectable()
export class CategoryService {
  private url = "/category";

  constructor(private http: HttpClient) { }

  private getHeadersWithAccessToken() {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + sessionStorage.getItem('accessToken'),
    });
    return headers;
  }

  getCategoriesByParentId(id: number) {
    return this.http.get(this.url + '/index/' + id);
  }

  getAllCategories() {
    return this.http.get(this.url + '/getallcategories');
  }

  getAllRootCategories() {
    return this.http.get(this.url + '/getallrootcategories');
  }

  getDeletedCategories() {
    return this.http.get(this.url + '/getdeletedcategories');
  }

  getCategoryById(id: number) {
    return this.http.get(this.url + '/getcategorybyid/' + id);
  }

  addCategory(category: Category) {
    const headers = this.getHeadersWithAccessToken();
    return this.http.post(this.url + '/add', category, { headers });
  }

  editCategory(category: Category) {
    const headers = this.getHeadersWithAccessToken();
    return this.http.post(this.url + '/edit', category, { headers });
  }

  hideCategory(id: number) {
    const headers = this.getHeadersWithAccessToken();
    return this.http.post(this.url + '/hide/' + id, null, { headers });
  }

  deleteCategory(id: number) {
    const headers = this.getHeadersWithAccessToken();
    return this.http.post(this.url + '/delete/' + id, null, { headers });
  }

  restoreCategory(id: number) {
    const headers = this.getHeadersWithAccessToken();
    return this.http.post(this.url + '/restore/' + id, null, { headers });
  }
}
