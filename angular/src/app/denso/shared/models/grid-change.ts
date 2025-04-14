export class GridChange<T> {
    type: 'insert' | 'update' | 'remove';
    key: any;
    data: Partial<T>;
}
