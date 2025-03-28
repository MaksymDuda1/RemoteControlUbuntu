export class PaginatedList<T> {
  items: T[] = [];
  pageIndex: number = 0;
  totalPages: number = 0;
  totalCount: number = 0;
  hasPreviousPage: boolean = false;
  hasNextPage: boolean = false;
}
