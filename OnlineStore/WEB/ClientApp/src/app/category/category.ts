export class Category {
    constructor(
        public id?: number,
        public isDeleted?: boolean,
        public name?: string,
        public description?: string,
        public imagePath?: string,
        public parentCategoryId?: number) { }
}