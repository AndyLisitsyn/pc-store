import { Product } from './product';

export class ProductModel {
  categoryId?: number;
  products?: Product[];
  pageNumber?: number;
  pagesCount?: number;
}
