import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { LoginMenuComponent } from './login-menu/login-menu.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import { CategoryComponent } from './category/category.component';
import { AddCategoryComponent } from './category/add-category.component';
import { EditCategoryComponent } from './category/edit-category.component';
import { RestoreCategoryComponent } from './category/restore-category.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { ProductComponent } from './product/product.component';
import { AddProductComponent } from './product/add-product.component';
import { EditProductComponent } from './product/edit-product.component';
import { RestoreProductComponent } from './product/restore-product.component';
import { FooterAbsoluteComponent } from './footer/footer-absolute.component';
import { FooterRelativeComponent } from './footer/footer-relative.component';
import { EmailConfirmationComponent } from './email-confirmation/email-confirmation.component';
import { NotFoundComponent } from './not-found/not-found.component'

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LoginMenuComponent,
    LoginComponent,
    RegisterComponent,
    EmailConfirmationComponent,
    FooterAbsoluteComponent,
    FooterRelativeComponent,
    HomeComponent,
    CategoryComponent,
    AddCategoryComponent,
    EditCategoryComponent,
    RestoreCategoryComponent,
    ProductComponent,
    AddProductComponent,
    EditProductComponent,
    RestoreProductComponent,
    ProductDetailsComponent,
    NotFoundComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'category/:id', component: CategoryComponent },
      { path: 'addCategory/:parentCategoryId', component: AddCategoryComponent },
      { path: 'addCategory', component: AddCategoryComponent },
      { path: 'editCategory/:id', component: EditCategoryComponent },
      { path: 'restoreCategories', component: RestoreCategoryComponent },
      { path: 'products/:categoryId/:page', component: ProductComponent },
      { path: 'product-details/:id', component: ProductDetailsComponent },
      { path: 'addProduct/:categoryId', component: AddProductComponent },
      { path: 'editProduct/:id', component: EditProductComponent },
      { path: 'restoreProducts', component: RestoreProductComponent },
      { path: 'email-confirmation/:isSuccess', component: EmailConfirmationComponent },
      { path: '404', component: NotFoundComponent },
      { path: '**', redirectTo: '/404' }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
