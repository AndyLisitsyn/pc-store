import { Image } from "../product/image";

export class Product {
    constructor(
        public id?: number,
        public isDeleted?: boolean,
        public name?: string,
        public description?: string,
        public price?: number,
        public amount?: number,
        public code?: number,
        public categoryId?: number,
        public images?: Image[]
    ) { }
}
