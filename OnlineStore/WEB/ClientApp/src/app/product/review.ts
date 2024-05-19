export class Review {
  constructor(
    public id?: number,
    public date?: Date,
    public desiredDisplayName?: string,
    public text?: string,
    public rating?: string,
    public productId?: number,
    public userId?: number) { }
}
